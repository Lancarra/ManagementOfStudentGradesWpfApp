using ManagementOfStudentGradesWpfApp.Models;
using Microsoft.EntityFrameworkCore;
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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ManagementOfStudentGradesWpfApp.Views
{
    /// <summary>
    /// Interaction logic for StudentWindow.xaml
    /// </summary>
    public partial class StudentWindow : Window
    {
        private ApplicationDbContext _dbContext;

        public StudentWindow()
        {
            try
            {
                InitializeComponent();
                using (_dbContext = new ApplicationDbContext())
                {
                    _dbContext.Enrollments.Load();
                    _dbContext.Courses.Load();
                    _dbContext.Teachers.Load();
                    _dbContext.Students.Load();

                    gradesDataGrid.ItemsSource = _dbContext.Enrollments.Local.ToBindingList();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadData()
        {
            using(_dbContext = new ApplicationDbContext())
            {
                _dbContext.Enrollments.Load();
                _dbContext.Courses.Load();
                _dbContext.Teachers.Load();
                _dbContext.Students.Load();
                gradesDataGrid.ItemsSource = _dbContext.Enrollments.Local.ToBindingList();
            }

        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (_dbContext = new ApplicationDbContext())
                {
                    _dbContext.Enrollments.Load();
                    _dbContext.Courses.Load();
                    _dbContext.Teachers.Load();
                    _dbContext.Students.Load();

                    IEnumerable<Enrollment> data = null;


                    if (tbCourse.Text != string.Empty && tbTeacher.Text == string.Empty && dateGrade.Text == string.Empty)
                    {
                        data = _dbContext.Enrollments.Where(x => x.Course.Title.Contains(tbCourse.Text)).ToList();
                    }
                    if (tbCourse.Text == string.Empty && tbTeacher.Text != string.Empty && dateGrade.Text == string.Empty)
                    {
                        data = _dbContext.Enrollments.Where(x => x.Teacher.FullName.Contains(tbCourse.Text)).ToList();
                    }
                    if (tbCourse.Text == string.Empty && tbTeacher.Text == string.Empty && dateGrade.Text != string.Empty)
                    {
                        data = _dbContext.Enrollments.Where(x => x.GradeDate.Date == DateTime.Parse( dateGrade.Text)).ToList();

                    }

                    gradesDataGrid.ItemsSource = data;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }

}
