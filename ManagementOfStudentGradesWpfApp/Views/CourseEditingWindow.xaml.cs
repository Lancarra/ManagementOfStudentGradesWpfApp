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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MessageBox = System.Windows.MessageBox;

namespace ManagementOfStudentGradesWpfApp.Views
{
    /// <summary>
    /// Interaction logic for CourseEditingWindow.xaml
    /// </summary>
    public partial class CourseEditingWindow : Window
    {
        private ApplicationDbContext _dbContext;

        public CourseEditingWindow(Course course)
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

                    gradesDataGrid.ItemsSource = _dbContext.Enrollments.Where(x=>x.Course.Title == course.Title).ToList();
                    var c = _dbContext.Enrollments.Include(x=>x.Teacher).FirstOrDefault(x => x.Course.Title == course.Title);

                    tbCourse.Text = course.Title;
                    tbTeacher.Text = c.Teacher.FullName;

                    comboBoxStudent.ItemsSource = _dbContext.Students.ToList();

                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                using (_dbContext = new ApplicationDbContext())
                {
                    int IdLib = (gradesDataGrid.SelectedItem as Enrollment).IdEnrollment;
                    
                    Enrollment updateEnrollment = (from d in _dbContext.Enrollments where d.IdEnrollment == IdLib
                                                   select d).Single();
                    
                    updateEnrollment.Grade = (gradesDataGrid.SelectedItem as Enrollment).Grade;
                    updateEnrollment.GradeDate = DateTime.Now;


                    _dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
            
        }

        private void RefreshData()
        {
            using (_dbContext = new ApplicationDbContext())
            {
                _dbContext.Enrollments.Load();
                _dbContext.Courses.Load();
                _dbContext.Teachers.Load();
                _dbContext.Students.Load();
                _dbContext.Enrollments.Load();
                gradesDataGrid.ItemsSource = _dbContext.Enrollments.Where(x => x.Course.Title == tbCourse.Text).ToList();
            }
        }

        private void btnAddEnrollment_Click(object sender, RoutedEventArgs e)
        {
            using (_dbContext = new ApplicationDbContext())
            {
                var courseId = _dbContext.Courses.FirstOrDefault(x => x.Title == tbCourse.Text).IdCourse;

                var teacherId = _dbContext.Teachers.FirstOrDefault(x => x.FullName == tbTeacher.Text).IdTeacher;

                var studentId = _dbContext.Students.FirstOrDefault(x => x.FullName == comboBoxStudent.Text).IdStudent;

                var enrollment = new Enrollment
                {
                    CourseId = courseId,
                    TeacherId = teacherId,
                    StudentId = studentId,
                    GradeDate = DateTime.Now,
                    Grade = int.Parse(tbGrade.Text)
                };

                _dbContext.Enrollments.Add(enrollment);
                _dbContext.SaveChanges();
                RefreshData();

            }
        }
    }

}
