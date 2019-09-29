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

        ///////////////////////////////// SEARCH FUNCTIONS HERE //////////////////////////////////////////
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

        /////////////////////////////// ADD FUNCTIONS HERE /////////////////////////////////////////////////
        public void AddPerson(Person person)
        {
            connection.Query($"INSERT into [People].[dbo].PeopleTable (employeeID, personName, hasDiscount, isDependent) values ({person.employeeID}, '{person.personName}', '{person.hasDiscount}', '{person.isDependent}')");
        }

        /////////////////////////////////// REMOVE FUNCTIONS HERE //////////////////////////////////////////
        public void RemovePerson(Person person)
        {
            connection.Query($"DELETE FROM [People].[dbo].PeopleTable WHERE employeeID = {person.employeeID} AND personName = '{person.personName}'");
        }

        //////////////////////////////////// EDIT FUNCTIONS HERE ///////////////////////////////////////////
        public void EditPerson(Person originalPerson, Person editedPerson)
        {
            connection.Query($"UPDATE [People].[dbo].PeopleTable SET employeeID = {editedPerson.employeeID}, personName = '{editedPerson.personName}', hasDiscount = '{editedPerson.hasDiscount}', isDependent = '{editedPerson.isDependent}'" +
                $" WHERE employeeID = {originalPerson.employeeID} AND personName = '{originalPerson.personName}'");
        }
}
}
