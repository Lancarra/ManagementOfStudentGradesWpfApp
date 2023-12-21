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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ManagementOfStudentGradesWpfApp.Views
{
    /// <summary>
    /// Interaction logic for TeacherWindow.xaml
    /// </summary>
    public partial class TeacherWindow : Window
    {
        private ApplicationDbContext _dbContext;

        public TeacherWindow()
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
                    //gradesDataGrid.ItemsSource = _dbContext.Enrollments.Local.ToBindingList();
                    enrollmentListView.ItemsSource = _dbContext.Courses.Local.ToBindingList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void OnCourseSelected(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (enrollmentListView.SelectedItem != null && enrollmentListView.SelectedItem is Course selectedCourse)
                {
                    using (_dbContext = new ApplicationDbContext())
                    {
                        _dbContext.Enrollments.Load();
                        _dbContext.Courses.Load();
                        _dbContext.Teachers.Load();
                        _dbContext.Students.Load();

                        // Отримати ID вибраної замітки
                        var selectedNoteId = selectedCourse.Title;
                        CourseEditingWindow courseEditingWindow = new CourseEditingWindow(_dbContext.Courses.FirstOrDefault(x=>x.Title == selectedNoteId));
                        courseEditingWindow.Show();
                        this.Close();
                    }

                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            //CourseEditingWindow courseEditingWindow = new CourseEditingWindow();
        }
    }
}
