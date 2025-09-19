using GraphQL.Types;

namespace minimal_api_repoDb.GraphQL.Types;

public class EmployeeInputType : InputObjectGraphType
{
    public EmployeeInputType()
    {
        Name = "EmployeeInputType";
        Field<StringGraphType>("FirstName");
        Field<StringGraphType>("LastName");
        Field<StringGraphType>("Email");
        //Adding Reviews for Employee Object
        //Field<ListGraphType<ReviewInputType>>("Reviews");
    }
}