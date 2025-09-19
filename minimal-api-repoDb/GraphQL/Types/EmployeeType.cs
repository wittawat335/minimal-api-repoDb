using GraphQL.Types;
using minimal_api_repoDb.Data.Models;

namespace minimal_api_repoDb.GraphQL.Types;
public class EmployeeType : ObjectGraphType<Employee>
{
    public EmployeeType()
    {

        Field(d => d.Id, type: typeof(IdGraphType)).Description("");
        Field(d => d.Email, type: typeof(StringGraphType)).Description("");
        Field(d => d.FirstName, type: typeof(StringGraphType)).Description("");
        Field(d => d.LastName, type: typeof(StringGraphType)).Description("");

        //one to many review relationship
        Field(d => d.Reviews, type: typeof(ListGraphType<ReviewType>)).Description("");
    }
}
