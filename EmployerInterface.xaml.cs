using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using Dapper;


namespace EmployeeBenefitCosts
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DatabaseManagement database;
        Statistics calculator;
        public MainWindow()
        {
            InitializeComponent();
            database = new DatabaseManagement();
            calculator = new Statistics();
        }

        private void ExecuteButton_Click(object sender, RoutedEventArgs e)
        {
            int employeeID = -1;
            string name = "";
            // regex to only allow alphanumerics and white space in the names input
            string namePattern = @"[^\w\s]*";
            Regex rgx = new Regex(namePattern);
            bool isDependent = false;
            // set employeeID to input if it is an int
            bool success = Int32.TryParse(EmployeeIDInput.Text, out employeeID);
            if (!success) { employeeID = -1; }
            if (NameInput.Text != "" && rgx.IsMatch(NameInput.Text)) { name = NameInput.Text; }
            if (IsDependent.IsChecked == true) { isDependent = true; }

            if (SearchButton.IsChecked == true)
            {
                Person[] people;
                // Case where the employer wants to return the whole database
                if(employeeID == -1 && name == "")
                {
                    people = database.SearchAll();
                    updateTable(people);
                    NumDependents.Content = "";
                    HasDiscount.Content = "";
                    CostPerPaycheck.Content = "";
                    Paycheck.Content = "";
                }
                // Case where the employer wants to return all people tied to a employee ID
                else if(employeeID != -1 && name == "")
                {
                    people = database.SearchEmployeeID(employeeID);
                    searchProcess(people);
                }
                // Case where the employer wants to find a specific individual
                else
                {
                    people = database.SearchExact(employeeID, name, isDependent);
                    searchProcess(people);
                }
            }
            else if (AddButton.IsChecked == true)
            {
                char firstLetter = name[0];
                bool hasDiscount;
                if (firstLetter == 'A' || firstLetter == 'a') { hasDiscount = true; }
                else { hasDiscount = false; }
                Person person = new Person(employeeID, name, hasDiscount, isDependent);
                database.AddPerson(person);
            }
            else if (RemoveButton.IsChecked == true)
            {
                database.RemovePerson(employeeID, name);
            }
            else if (EditButton.IsChecked == true)
            {
                // Person that already exists in the database
                Person[] people = database.SearchExact(employeeID, name, isDependent);
                Person originalPerson = new Person(people[0].employeeID, people[0].personName, people[0].hasDiscount, people[0].isDependent);
                // Replacement information
                employeeID = -1;
                name = "";
                isDependent = false;
                success = Int32.TryParse(EmployeeIDInput.Text, out employeeID);
                if (!success) { employeeID = -1; }
                if (EditNameInput.Text != "" && rgx.IsMatch(EditNameInput.Text)) { name = EditNameInput.Text; }
                if (EditIsDependent.IsChecked == true) { isDependent = true; }
                char firstLetter = name[0];
                bool hasDiscount = false;
                if (firstLetter == 'A' || firstLetter == 'a') { hasDiscount = true; }
                Person editedPerson = new Person(employeeID, name, hasDiscount, isDependent);
                database.EditPerson(originalPerson, editedPerson);
            }
            // Clear input fields
            EmployeeIDInput.Clear();
            NameInput.Clear();
            IsDependent.IsChecked = false;
            EditEmployeeIDInput.Clear();
            EditNameInput.Clear();
            EditIsDependent.IsChecked = false;
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            int percentage;
            bool success = Int32.TryParse(CompanyContributionRatioInput.Text, out percentage);
            if (success)
            {
                double rate = percentage * 0.01;
                calculator.companyContributions = rate;
            }
            else
            {
                MessageBox.Show("Invalid company contribution value, please enter an integer.");
                CompanyContributionRatioInput.Clear();
            }
        }

        private void updateTable(Person[] people)
        {
            DataTable table = new DataTable();
            table.Columns.Add("employeeID");
            table.Columns.Add("personName");
            table.Columns.Add("hasDiscount");
            table.Columns.Add("isDependent");
            DataRow row;
            for (int i = 0; i < people.Length; i++)
            {
                row = table.NewRow();
                row["employeeID"] = people[i].employeeID;
                row["personName"] = people[i].personName;
                row["hasDiscount"] = people[i].hasDiscount;
                row["isDependent"] = people[i].isDependent;
                table.Rows.Add(row);
            }
            DataGrid.ItemsSource = table.DefaultView;
        }

        private void searchProcess(Person[] people)
        {
            updateTable(people);
            Person[] allPeople = database.SearchEmployeeID(people[0].employeeID);
            int numDependents = allPeople.Length - 1;
            int numDependentDiscounts;
            bool employeeDiscount = findEmployeeDiscount(allPeople, out numDependentDiscounts);
            double paycheckDeductions = calculator.costPerPaycheck(employeeDiscount, allPeople.Length - 1, numDependentDiscounts);
            NumDependents.Content = numDependents; //There should always be one employee
            HasDiscount.Content = employeeDiscount; //Employee discount status
            CostPerPaycheck.Content = paycheckDeductions.ToString("#.00");
            Paycheck.Content = calculator.getPaycheck(paycheckDeductions).ToString("#.00");
        }

        private bool findEmployeeDiscount(Person[] people, out int numDependentDiscounts)
        {
            bool hasDiscount = false;
            numDependentDiscounts = 0;
            for(int i = 0; i < people.Length; i++)
            {
                if(people[i].isDependent == false && people[i].hasDiscount == true)
                {
                    hasDiscount = true;
                }
                else if(people[i].isDependent == true && people[i].hasDiscount == true)
                {
                    numDependentDiscounts++;
                }
            }
            return hasDiscount;
        }
    }
}
