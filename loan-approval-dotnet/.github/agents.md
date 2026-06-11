# AGENTS.md — loan-approval-dotnet

## Service Overview
ASP.NET Core .NET 8 Minimal API for retail banking loan approvals.
Evaluates loan applications by credit score, income, employment status.
Returns a structured decision record.

## Build Commands
  dotnet build                  # compile
  dotnet test                   # run xUnit tests
  dotnet run                    # start the API (port 5000)
  dotnet publish -c Release     # production build

## Architecture Rules
- Minimal API endpoints in Program.cs — thin, delegate immediately to services.
- Services: registered via DI, constructor injection only.
- Records for all DTOs: immutable, concise, init-only properties.
- Validation: use data annotations ([Required], [Range]) or FluentValidation — not manual if/throw chains.
- Exceptions: typed domain exceptions or built-in BCL exceptions. NEVER throw raw Exception.

## Coding Standards
- Logging: ILogger<T> via constructor injection. NEVER Console.WriteLine.
- Use structured logging: _logger.LogInformation("Processing {ApplicantId}", id)
- Nullable reference types enabled (default in .NET 8). All types must be nullable-aware.
- XML doc comments (///) on all public methods and classes.
- C# naming: _camelCase for private fields, PascalCase for everything public.
- NEVER use `var` when the type is not immediately obvious from the right-hand side.

## Test Standards
- xUnit. Fact for single cases, Theory + InlineData for parameterised cases.
- Test naming: MethodName_GivenCondition_ExpectedResult
- Arrange / Act / Assert comments in every test method.
- Mock with Moq or NSubstitute — never concrete implementations.

## Domain
- LoanRequest: ApplicantId (string), RequestedAmount (decimal), CreditScore (int), AnnualIncome (decimal), EmploymentStatus (string)
- LoanDecision: record with ApplicantId, Status (APPROVED/REJECTED), InterestRate (decimal?), Reason
- Credit Score: 300–900. Below 600 = high risk.