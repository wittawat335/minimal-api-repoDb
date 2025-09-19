using GraphQL.Types;
using minimal_api_repoDb.Data.Models;

namespace minimal_api_repoDb.GraphQL.Types;
public class EmployeeType : ObjectGraphType<Employee>
{
    public EmployeeType()
    {

        Field(d => d.Id, type: typeof(IdGraphType)).Description("Id property for Employee object");
        Field(d => d.Email, type: typeof(StringGraphType)).Description("Email property for Employee object");
        Field(d => d.FirstName, type: typeof(StringGraphType)).Description("FirstName property for Employee object");
        Field(d => d.LastName, type: typeof(StringGraphType)).Description("LastName property for Employee object");

        ////one to many review relationship
        //Field(d => d.Reviews, type: typeof(ListGraphType<ReviewType>)).Description("List of reviews for the employee object");
    }
}
