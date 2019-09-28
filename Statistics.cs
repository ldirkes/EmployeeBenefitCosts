using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeBenefitCosts
{
    class Statistics
    {
        // Given constants from assignment
        private const int paycheck = 2000; // Each employee is payed $2000 per paycheck
        private const int numPaychecksPerYear = 26; // 26 paychecks in a year
        private const int employeeBenefitsPerYear = 1000; // Each employee's benefits costs $1000 per year
        private const int dependentBenefitsPerYear = 500; // Each dependant's benefits costs $500 per year
        private const double theARate = 0.9; // Anyone whose name starts with 'A' gets a 10% discount -> pay 90% of the rate
        // Statistics to track in the UI
        private double avgDependentsPerEmployee { get; set; } // Average number of dependents that each employee has
        private double avgCostToEmployee { get; set; } // Average cost to employee per paycheck
        private int numEmployees { get; set; } // number of employees in the company (might be unnecessary)
        private int numDependents { get; set; } // number of dependants in the company (might be unnecessary)
        private double totalEmployeeCosts { get; set; } // total costs for all employees
        private double totalDepenedentCosts { get; set; } // total costs for all dependents
        private double totalCompanyCosts { get; set; } // total costs of benefits for the entire company
        // The percentage of the cost of benefits that the company pays for
        private double companyContributions { get; set; }
        // Constructor to be used when starting with a new database
        public Statistics()
        {
            this.avgDependentsPerEmployee = 0;
            this.avgCostToEmployee = 0;
            this.numEmployees = 0; 
            this.numDependents = 0; 
            this.totalEmployeeCosts = 0;
            this.totalDepenedentCosts = 0; 
            this.totalCompanyCosts = 0;
            this.companyContributions = 0.5;
        }
        // Constructor to be used when there is an existing database
        public Statistics(double avgDependentsPerEmployee, double avgCostToEmployee, int numEmployees, int numDependents, double totalEmployeeCosts, double totalDepenedentCosts, double totalCompanyCosts)
        {
            this.avgDependentsPerEmployee = avgDependentsPerEmployee;
            this.avgCostToEmployee = avgCostToEmployee;
            this.numEmployees = numEmployees;
            this.numDependents = numDependents;
            this.totalEmployeeCosts = totalEmployeeCosts;
            this.totalDepenedentCosts = totalDepenedentCosts;
            this.totalCompanyCosts = totalCompanyCosts;
        }
    }
}
