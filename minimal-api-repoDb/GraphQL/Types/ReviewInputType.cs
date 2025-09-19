using GraphQL.Types;

namespace minimal_api_repoDb.GraphQL.Types;

public class ReviewInputType : InputObjectGraphType
{
    public ReviewInputType()
    {
        Name = "ReviewInputType";
        Field<IntGraphType>("Rate");
        Field<StringGraphType>("Comment");
    }
}
