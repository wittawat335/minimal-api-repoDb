using GraphQL;
using GraphQL.Types;
using minimal_api_repoDb.Data.Models;
using minimal_api_repoDb.Data.Respositories;
using minimal_api_repoDb.GraphQL.Types;

namespace minimal_api_repoDb.GraphQL.Mutations;

public class EmployeeMutation : ObjectGraphType
{
    public EmployeeMutation(EmployeeRepository _respository)
    {
        Field<EmployeeType>(
            "addEmployee",
            "Is used to add a new employee to the database",
            arguments: new QueryArguments(
                new QueryArgument<NonNullGraphType<EmployeeInputType>> { Name = "employee", Description = "Employee input parameter." }
                ),
            resolve: context =>
            {
                var employee = context.GetArgument<Employee>("employee");
                if (employee != null)
                {
                    return _respository.AddEmployee(employee);
                }
                return null;
            });

        Field<EmployeeType>(
           "updateEmployee",
           "Is used to update a existing employee to the database",
           arguments: new QueryArguments(
               new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id", Description = "Id of the employee that need to be updated" },
               new QueryArgument<NonNullGraphType<EmployeeInputType>> { Name = "employee", Description = "Employee input parameter." }
               ),
           resolve: context =>
           {
               var id = context.GetArgument<int>("id");
               var employee = context.GetArgument<Employee>("employee");
               if (employee != null)
               {
                   return _respository.UpdateEmployee(id, employee);
               }
               return null;
           });

        Field<EmployeeType>(
          "deleteEmployee",
          "Is used to delete a existing employee to the database",
          arguments: new QueryArguments(
              new QueryArgument<NonNullGraphType<IdGraphType>>
              {
                  Name = "id",
                  Description = "Id of the employee that need to be updated"
              }
              ),
          resolve: context =>
          {
              var id = context.GetArgument<int>("id");
              _respository.DeleteEmployee(id);
              return true;
          });
    }
}
