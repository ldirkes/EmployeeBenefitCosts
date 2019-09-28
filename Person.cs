using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeBenefitCosts
{
    class Person
    {
        // ID number of the employee
        private int employeeID { get; set; }
        // first name of the person
        private string firstName { get; set; }
        // last name of the person
        private string lastName { get; set; }
        // determines if this person gets a discount
        private bool discount { get; set; }
        // determines if this person is a dependent (TRUE == dependent, FALSE == employee)
        private bool isDependent { get; set; }
        // Constructor for creating a Person object
        public Person(int employeeID, string firstName, string lastName, bool discount, bool isDependent) 
        {
            this.employeeID = employeeID;
            this.firstName = firstName;
            this.lastName = lastName;
            this.discount = discount;
            this.isDependent = isDependent;
        }
    }
}
