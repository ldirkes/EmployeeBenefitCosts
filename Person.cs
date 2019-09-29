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
        private string name { get; set; }
        // determines if this person gets a discount
        private bool discount { get; set; }
        // determines if this person is a dependent (TRUE == dependent, FALSE == employee)
        private bool isDependent { get; set; }
        // Constructor for creating a Person object
        public Person()
        {

        }
        public Person(int employeeID, string name, bool discount, bool isDependent) 
        {
            this.employeeID = employeeID;
            this.name = name;
            this.discount = discount;
            this.isDependent = isDependent;
        }
    }
}
