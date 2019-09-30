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
        // The percentage of the cost of benefits that the company pays for
        public double companyContributions { get; set; }
        // Constructor to be used when starting with a new database
        public Statistics()
        {
            this.companyContributions = 0.5;// Default value is 50%
        }

        public double costPerPaycheck(bool employeeDiscount, int numDependents, int numDependentDiscounts)
        {
            // Calculate the paycheck deduction cost for the employee's benefits
            double employeeDeduction = (1 - companyContributions) * employeeBenefitsPerYear / numPaychecksPerYear;
            // Does this employee get a discount?
            if (employeeDiscount) { employeeDeduction *= theARate; }
            // If the employee has dependents, calculate their costs per paycheck
            double dependentDeduction = 0;
            for(int i = 0; i < numDependents; i++)
            {
                // Apply discount if still available
                if(numDependentDiscounts > 0)
                {
                    dependentDeduction += (1 - companyContributions) * dependentBenefitsPerYear * theARate / numPaychecksPerYear;
                    numDependentDiscounts--;
                }
                // No discounts left, charge full amount
                else
                {
                    dependentDeduction += (1 - companyContributions) * dependentBenefitsPerYear / numPaychecksPerYear;
                }
            }
            return employeeDeduction + dependentDeduction;
        }
        // return the paycheck value the employee will receive after deductions
        public double getPaycheck(double deductions)
        {
            return paycheck - deductions;
        }
    }
}
