using GraphQL;
using GraphQL.Types;
using minimal_api_repoDb.Data.Respositories;
using minimal_api_repoDb.GraphQL.Types;

namespace minimal_api_repoDb.GraphQL.Queries;

public class EmployeeQuery : ObjectGraphType
{
    public EmployeeQuery(EmployeeRepository _repository)
    {
        Field<ListGraphType<EmployeeType>>(
            "employees",
            "Return all the employees",
            resolve: context => _repository.GetAllEmployees());

        Field<EmployeeType>(
            "employee",
            "Return a single employee by id",
            new QueryArguments(new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id", Description = "Employee Id" }),
            resolve: context => _repository.GetEmployeeById(context.GetArgument("id", int.MinValue)));
    }
}
