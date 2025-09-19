using GraphQL.Types;
using minimal_api_repoDb.Data.Models;

namespace minimal_api_repoDb.GraphQL.Types;

public class ReviewType : ObjectGraphType<Review>
{
    public ReviewType()
    {

        Field(d => d.Id, type: typeof(IdGraphType)).Description("");
        Field(d => d.Rate, type: typeof(IdGraphType)).Description("");
        Field(d => d.Comment, type: typeof(StringGraphType)).Description("");
    }
}
