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
        public MainWindow()
        {
            InitializeComponent();
            DatabaseManagement database = new DatabaseManagement();

        }

        private void ExecuteButton_Click(object sender, RoutedEventArgs e)
        {
            if(SearchButton.IsChecked == true)
            {
                MessageBox.Show("Search Selected");
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

            if(IsDependent.IsChecked == true)
            {
                MessageBox.Show("This person is a dependent");
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SearchButton_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void AddButton_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void RemoveButton_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void EditButton_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
