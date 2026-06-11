---
applyTo: "**/*.cs"
---
# C# .NET 8 Standards

- .NET 8 Minimal API patterns. No MVC controllers unless specifically requested.
- Records for all DTOs: public record LoanRequest(string ApplicantId, ...);
- Constructor injection for all dependencies. NEVER property injection.
- ILogger<T> for logging — structured logging with named placeholders {PropertyName}.
- NEVER Console.WriteLine or Debug.WriteLine.
- Nullable reference types: enable. Mark intentionally nullable with ?.
- XML doc comments on all public types and members.
- Use primary constructors where it reduces boilerplate (C# 12).
- NEVER throw raw Exception — use ArgumentException, InvalidOperationException, or a custom domain exception.
- xUnit for tests. Fact and Theory attributes. Arrange/Act/Assert structure.