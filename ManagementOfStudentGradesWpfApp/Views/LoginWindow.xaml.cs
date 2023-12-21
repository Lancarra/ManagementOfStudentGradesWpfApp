using ManagementOfStudentGradesWpfApp.Controllers;
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
using System.Windows.Shapes;

namespace ManagementOfStudentGradesWpfApp.Views
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private readonly DatabaseController _dbController;

        public LoginWindow()
        {
            try
            {
                InitializeComponent();
                _dbController = new DatabaseController();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);    
            }
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string login = txtLoginData.Text;
            string password = txtPassword.Password;

            if (_dbController.ValidateUserCredentials(login, password) != null)
            {
                if(_dbController.ValidateUserCredentials(login, password).Role == "student")
                {
                    StudentWindow studentWindow = new StudentWindow();
                    studentWindow.Show();
                    this.Close();
                }
                else if (_dbController.ValidateUserCredentials(login, password).Role == "teacher")
                {
                    TeacherWindow teacherWindow = new TeacherWindow();
                    teacherWindow.Show();
                    this.Close();
                }
                else if (_dbController.ValidateUserCredentials(login, password).Role == "admin")
                {
                    AdminWindow adminWindow = new AdminWindow();
                    adminWindow.Show();
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Invalid username or password.");
            }
        }
    }
}
