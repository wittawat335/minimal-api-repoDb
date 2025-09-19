using GraphQL.Types;
using minimal_api_repoDb.GraphQL.Queries;

namespace minimal_api_repoDb.GraphQL;

public class AppSchema : Schema
{
    public AppSchema(EmployeeQuery query)
    {
        this.Query = query;
        //this.Mutation = mutation;
    }
}
