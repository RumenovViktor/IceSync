namespace IceSync.Controllers;

using System.ComponentModel.DataAnnotations;
using BL.Commands.RunWorkflow;
using BL.Queries.GetWorkflows;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Models;

public class WorkflowsController : Controller
{
    private readonly IMediator _mediator;

    public WorkflowsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var command = new GetWorkflowsQuery();
        var result = await _mediator.Send(command);
        
        var viewModel = result.Workflows.Select(x => new WorkflowViewModel
        {
            WorkflowId = x.WorkflowId,
            WorkflowName = x.WorkflowName,
            IsActive = x.IsActive,
            IsRunning = x.IsRunning,
            MultiExecBehaviour = x.MultiExecBehaviour
        });
        
        return View(viewModel);
    }
    
    [ValidateAntiForgeryToken]
    [HttpPost("/{workflowId}/run")]
    public async Task<IActionResult> Run([Required] [FromRoute] int workflowId)
    {
        TempData["Success"] = "";
        TempData["Error"] = "";
        
        try
        {
            var command = new RunWorkflowCommand(workflowId);
            await _mediator.Send(command);
            TempData["Success"] = "Successfully run the workflow";
        }
        catch (Exception e)
        {
            ModelState.AddModelError(string.Empty, "An error occurred while performing the action.");
            TempData["Error"] = "An error occurred while performing the action.";
        }
        
        return RedirectToAction("Index", "Workflows");
    }
}