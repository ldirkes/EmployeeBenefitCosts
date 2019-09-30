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
            if (SearchButton.IsChecked == true)
            {
                Person[] people;
                try
                {
                    int employeeID = Int32.Parse(EmployeeIDInput.Text);
                    if (IsDependent.IsChecked == true)
                    {
                        people = database.SearchExact(employeeID, true);
                    }
                    else
                    {
                        people = database.SearchExact(employeeID, false);
                    }
                    //MessageBox.Show($"Search returned EmployeeID:{people[0].employeeID}, Name: {people[0].personName}, Dependent: {people[0].isDependent}, Gets a discount: {people[0].hasDiscount}");
                    DataTable table = createTable(people);
                    DataGrid.ItemsSource = table.DefaultView;
                    int numDependents = people.Length - 1;
                    bool employeeDiscount = people[0].hasDiscount;
                    double paycheckDeductions = calculator.costPerPaycheck(people[0].hasDiscount, people.Length - 1, 0);
                    NumDependents.Content = numDependents; //There should always be one employee
                    HasDiscount.Content = employeeDiscount; //Employee discount status
                    CostPerPaycheck.Content = paycheckDeductions.ToString("#.00");
                    Paycheck.Content = calculator.getPaycheck(paycheckDeductions).ToString("#.00");
                }
                catch (FormatException)
                {
                    people = database.SearchLikeName(NameInput.Text);
                    MessageBox.Show($"Search returned EmployeeID:{people[0].employeeID}, Name: {people[0].personName}, Dependent: {people[0].isDependent}, Gets a discount: {people[0].hasDiscount}");
                }
                catch (IndexOutOfRangeException)
                {
                    MessageBox.Show("There is no one in our system associated with that Employee ID.");
                }
            }
            else if (AddButton.IsChecked == true)
            {
                //try
                //{
                //    int employeeID = Int32.Parse(EmployeeIDInput.Text);
                //    string name = NameInput.Text;
                //    char firstLetter = name[0];
                //    bool hasDiscount;
                //    bool isDependent;
                //    if (firstLetter == 'A' || firstLetter == 'a') { hasDiscount = true; }
                //    else { hasDiscount = false; }
                //    if (IsDependent.IsChecked == true) { isDependent = true; }
                //    else { isDependent = false; }
                //    Person person = new Person(employeeID, name, hasDiscount, isDependent);
                //    database.AddPerson(person);
                //}
                //catch(Exception)
                //{
                //    MessageBox.Show($"There was an exception");
                //}
                int employeeID = Int32.Parse(EmployeeIDInput.Text);
                string name = NameInput.Text;
                char firstLetter = name[0];
                bool hasDiscount;
                bool isDependent;
                if (firstLetter == 'A' || firstLetter == 'a') { hasDiscount = true; }
                else { hasDiscount = false; }
                if (IsDependent.IsChecked == true) { isDependent = true; }
                else { isDependent = false; }
                Person person = new Person(employeeID, name, hasDiscount, isDependent);
                database.AddPerson(person);
            }
            else if (RemoveButton.IsChecked == true)
            {
                int employeeID = Int32.Parse(EmployeeIDInput.Text);
                string name = NameInput.Text;
                char firstLetter = name[0];
                bool hasDiscount;
                bool isDependent;
                if (firstLetter == 'A' || firstLetter == 'a') { hasDiscount = true; }
                else { hasDiscount = false; }
                if (IsDependent.IsChecked == true) { isDependent = true; }
                else { isDependent = false; }
                Person person = new Person(employeeID, name, hasDiscount, isDependent);
                database.RemovePerson(person);
            }
            else if (EditButton.IsChecked == true)
            {
                int employeeID = Int32.Parse(EmployeeIDInput.Text);
                string name = NameInput.Text;
                bool isDependent;
                if (IsDependent.IsChecked == true) { isDependent = true; }
                else { isDependent = false; }
                Person[] people = database.SearchExact(employeeID, name, isDependent);
                Person originalPerson = new Person(people[0].employeeID, people[0].personName, people[0].hasDiscount, people[0].isDependent);

                int newEmployeeID = Int32.Parse(EditEmployeeIDInput.Text);
                string editedName = EditNameInput.Text;
                char firstLetter = editedName[0];
                bool editedHasDiscount;
                bool editedIsDependent;
                if (firstLetter == 'A' || firstLetter == 'a') { editedHasDiscount = true; }
                else { editedHasDiscount = false; }
                if (EditIsDependent.IsChecked == true) { editedIsDependent = true; }
                else { editedIsDependent = false; }
                Person editedPerson = new Person(newEmployeeID, editedName, editedHasDiscount, editedIsDependent);
                database.EditPerson(originalPerson, editedPerson);
            }
            EmployeeIDInput.Clear();
            NameInput.Clear();
            IsDependent.IsChecked = false;
            EditEmployeeIDInput.Clear();
            EditNameInput.Clear();
            EditIsDependent.IsChecked = false;
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            string ccri = CompanyContributionRatioInput.Text;
            int percentage = Int32.Parse(ccri);
            double rate = percentage * 0.01;
            calculator.companyContributions = rate;
        }

        private DataTable createTable(Person[] people)
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
            return table;
        }

    }
}
