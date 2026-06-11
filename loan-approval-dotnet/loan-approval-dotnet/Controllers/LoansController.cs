using LoanApproval.Models;
using LoanApproval.Services;
using Microsoft.AspNetCore.Mvc;

namespace LoanApproval.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoansController : ControllerBase
{
    private readonly LoanApprovalService _service;

    public LoansController(LoanApprovalService service)
    {
        _service = service;
    }

    [HttpPost("evaluate")]
    public ActionResult<LoanDecision> Evaluate([FromBody] LoanRequest request)
    {
        var decision = _service.Evaluate(request);
        return Ok(decision);
    }
}
