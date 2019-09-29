using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace EmployeeBenefitCosts
{
    class DatabaseManagement
    {
        public DatabaseManagement()
        {
            string connectionString = "Data Source=LANESROG\\TEW_SQLEXPRESS;Integrated Security=True";
            using (var connection = new SqlConnection(connectionString))
            {
                var resultsArray = connection.Query<Person>("SELECT * FROM [People].[dbo].PeopleTable").ToArray();

            }
        }
    }
}
