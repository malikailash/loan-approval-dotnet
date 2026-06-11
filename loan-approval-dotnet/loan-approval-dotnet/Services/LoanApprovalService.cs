using LoanApproval.Models;
using Microsoft.Extensions.Logging;

namespace LoanApproval.Services;

// TODO: this service has problems for Lab 1 to fix

public class LoanApprovalService
{
    private readonly ILogger<LoanApprovalService> _logger;

    public LoanApprovalService(ILogger<LoanApprovalService> logger)
    {
        _logger = logger;
    }

    public LoanDecision Evaluate(LoanRequest request)
    {
        _logger.LogInformation("Evaluating loan for: {ApplicantId}", request.ApplicantId);

        if (request.CreditScore <= 0)
            return new LoanDecision(request.ApplicantId, "REJECTED", null, "Credit score missing");

        if (request.CreditScore >= 750)
            return new LoanDecision(request.ApplicantId, "APPROVED", 7.5m, "Excellent profile");

        if (request.CreditScore >= 600)
            return new LoanDecision(request.ApplicantId, "APPROVED", 12.0m, "Standard profile");

        return new LoanDecision(request.ApplicantId, "REJECTED", null, "Credit score too low");
    }
}