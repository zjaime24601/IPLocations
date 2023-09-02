using FluentAssertions;
using FluentAssertions.Primitives;
using Microsoft.AspNetCore.Mvc;

namespace IPLocations.IntegrationTests;

public static class ProblemDetailsAssertionExtensions
{
    public static ProblemDetailsAssertions Should(this ProblemDetails instance)
    {
        return new ProblemDetailsAssertions(instance);
    }
}

public class ProblemDetailsAssertions
    : ReferenceTypeAssertions<ProblemDetails, ProblemDetailsAssertions>
{
    public ProblemDetailsAssertions(ProblemDetails instance)
     : base(instance)
    {

    }

    protected override string Identifier
        => nameof(ProblemDetailsAssertions);

    public AndConstraint<ProblemDetailsAssertions> HaveDetail(string expectedDetail)
    {
        Subject.Detail.Should().Be(expectedDetail);
        return new AndConstraint<ProblemDetailsAssertions>(this);
    }

    public AndConstraint<ProblemDetailsAssertions> HaveValidTraceId()
    {
        Subject.Extensions.Should().ContainKey("traceId");
        Subject.Extensions["traceId"].ToString().Should().NotBeNullOrWhiteSpace();
        return new AndConstraint<ProblemDetailsAssertions>(this);
    }
}
