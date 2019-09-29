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
        public int employeeID { get; set; }
        // first name of the person
        public string personName { get; set; }
        // determines if this person gets a discount
        public bool hasDiscount { get; set; }
        // determines if this person is a dependent (TRUE == dependent, FALSE == employee)
        public bool isDependent { get; set; }
        // Constructor for creating a Person object
        public Person()
        {

        }
        public Person(int employeeID, string personName, bool hasDiscount, bool isDependent) 
        {
            this.employeeID = employeeID;
            this.personName = personName;
            this.hasDiscount = hasDiscount;
            this.isDependent = isDependent;
        }
    }
}
