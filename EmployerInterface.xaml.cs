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
        public MainWindow()
        {
            InitializeComponent();
            database = new DatabaseManagement();
        }

        private void ExecuteButton_Click(object sender, RoutedEventArgs e)
        {
            if (SearchButton.IsChecked == true)
            {
                Person[] people = new Person[1];
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
                    MessageBox.Show($"Search returned EmployeeID:{people[0].employeeID}, Name: {people[0].personName}, Dependent: {people[0].isDependent}, Gets a discount: {people[0].hasDiscount}");
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
                EmployeeIDInput.Clear();
                NameInput.Clear();
            }
            else if (AddButton.IsChecked == true)
            {
                MessageBox.Show("Add Selected");
            }
            else if (RemoveButton.IsChecked == true)
            {
                MessageBox.Show("Remove Selected");
            }
            else if (EditButton.IsChecked == true)
            {
                MessageBox.Show("Edit Selected");
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            string ccri = CompanyContributionRatioInput.Text;
            MessageBox.Show(ccri);
        }
    }
}
