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
        SqlConnection connection;
        public DatabaseManagement()
        {
            string connectionString = "Data Source=LANESROG\\TEW_SQLEXPRESS;Integrated Security=True";
            this.connection = new SqlConnection(connectionString);
            connection.Open();
            //using (this.connection = new SqlConnection(connectionString))
            //{
            //    //var resultsArray = connection.Query<Person>("SELECT * FROM [People].[dbo].PeopleTable").ToArray();
            //    connection.Open();
            //}
        }
        // Search functions that return an array of person objects that exactly match the search criteria
        public Person[] SearchExact(int employeeID, bool isDependent)
        {
            if (isDependent)
            {
                return connection.Query<Person>($"SELECT * FROM [People].[dbo].PeopleTable WHERE employeeID = {employeeID} AND isDependent = 1").ToArray();
            }
            else
            {
                return connection.Query<Person>($"SELECT * FROM [People].[dbo].PeopleTable WHERE employeeID = {employeeID} AND isDependent = 0").ToArray();
            }
        }
        public Person[] SearchExact(string name, bool isDependent)
        {
            return connection.Query<Person>($"SELECT * FROM [People].[dbo].PeopleTable WHERE personName = '{name}' AND isDependent = '{isDependent}'").ToArray();
        }
        public Person[] SearchExact(int employeeID, string name, bool isDependent)
        {
            return connection.Query<Person>($"SELECT * FROM [People].[dbo].PeopleTable WHERE employeeID = {employeeID} AND personName = '{name}' AND isDependent = '{isDependent}'").ToArray();
        }
        // Search function that returns an array of person objects whose names contains the search parameters
        public Person[] SearchLikeName(string name)
        {
            return connection.Query<Person>($"SELECT * FROM [People].[dbo].PeopleTable WHERE personName LIKE '%{name}%'").ToArray();
        }
    }
}
