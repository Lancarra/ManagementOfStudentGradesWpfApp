using ManagementOfStudentGradesWpfApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        ApplicationDbContext _dbContext;
        public AdminWindow()
        {
            InitializeComponent();
            using (_dbContext = new ApplicationDbContext())
            {
                _dbContext.Enrollments.Load();
                _dbContext.Courses.Load();
                _dbContext.Teachers.Load();
                _dbContext.Students.Load();

                dataGridCourse.ItemsSource = _dbContext.Courses.Local.ToBindingList();

                dataGridGroups.ItemsSource = _dbContext.Groups.Local.ToBindingList();

                dataGridStudents.ItemsSource = _dbContext.Students.Include(x => x.User).Include(x => x.Group).ToList();

                comboBoxGroupName.ItemsSource = _dbContext.Groups.ToList();
            }
            LoadDbTeacher();
        }

        private void btnAddCourse_Click(object sender, RoutedEventArgs e)
        {
            using (_dbContext = new ApplicationDbContext())
            {
                var course = new Course
                {
                    Title = tbCourseTitle.Text,
                    Description = tbCourseDescription.Text,
                };

                _dbContext.Courses.Add(course);
                _dbContext.SaveChanges();
                LoadDbCourse();
            }
        }

        private void btnDeleteCourseById_Click(object sender, RoutedEventArgs e)
        {
            using (_dbContext = new ApplicationDbContext())
            {
                var course = new Course() { IdCourse = int.Parse(tbCourseDeleteById.Text) };
                _dbContext.Courses.Attach(course);

                _dbContext.Courses.Remove(course);
                _dbContext.SaveChanges();
                LoadDbCourse();
            }
        }

        private void LoadDbCourse()
        {
            using (_dbContext = new ApplicationDbContext())
            {
                _dbContext.Enrollments.Load();
                _dbContext.Courses.Load();
                _dbContext.Teachers.Load();
                _dbContext.Students.Load();

                dataGridCourse.ItemsSource = _dbContext.Courses.Local.ToBindingList();
            }
        }

        private void btnAddGroup_Click(object sender, RoutedEventArgs e)
        {
            using (_dbContext = new ApplicationDbContext())
            {
                var group = new Group
                {
                    Name = tbGroupName.Text,
                    Specialty = tbGroupSpecialty.Text,
                };

                _dbContext.Groups.Add(group);
                _dbContext.SaveChanges();
                LoadDbGroups();
            }
        }

        private void btnDeleteGroupById_Click(object sender, RoutedEventArgs e)
        {
            using (_dbContext = new ApplicationDbContext())
            {
                var group = new Group() { IdGroup = int.Parse(tbGroupDeleteById.Text) };
                _dbContext.Groups.Attach(group);

                _dbContext.Groups.Remove(group);
                _dbContext.SaveChanges();
                LoadDbGroups();
            }
        }

        private void LoadDbGroups()
        {
            using (_dbContext = new ApplicationDbContext())
            {
                _dbContext.Enrollments.Load();
                _dbContext.Courses.Load();
                _dbContext.Teachers.Load();
                _dbContext.Students.Load();

                dataGridGroups.ItemsSource = _dbContext.Groups.Local.ToBindingList();
            }
        }

        private void btnAddStudent_Click(object sender, RoutedEventArgs e)
        {
            using (_dbContext = new ApplicationDbContext())
            {
                var groupId = _dbContext.Groups.FirstOrDefault(x => x.Name == comboBoxGroupName.Text).IdGroup;
                var user = new User
                {
                    Username = tbUserUsername.Text,
                    Email = tbUserEmail.Text,
                    Password = tbUserPassword.Text,
                    DateOfBirth = DateTime.Parse(datePickerUserDateOfBirth.Text),
                    Role = "student"
                };

                _dbContext.Users.Add(user);
                _dbContext.SaveChanges();

                var student = new Student
                {
                    FullName = tbStudentFullName.Text,
                    UserId = user.IdUser,
                    GroupId = groupId
                };

                _dbContext.Students.Add(student);
                _dbContext.SaveChanges();
                LoadDbStudents();
            }
        }

        private void btnDeleteStudentById_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (_dbContext = new ApplicationDbContext())
                {
                    var student = new Student() { IdStudent = int.Parse(tbStudentDeleteById.Text) };
                    _dbContext.Students.Attach(student);

                    _dbContext.Students.Remove(student);
                    _dbContext.SaveChanges();
                    LoadDbStudents();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadDbStudents()
        {
            using (_dbContext = new ApplicationDbContext())
            {
                _dbContext.Enrollments.Load();
                _dbContext.Courses.Load();
                _dbContext.Teachers.Load();
                _dbContext.Students.Load();

                dataGridStudents.ItemsSource = _dbContext.Students.Include(x=>x.User).Include(x=>x.Group).ToList();
            }
        }

        private void btnAddTeacher_Click(object sender, RoutedEventArgs e)
        {
            using (_dbContext = new ApplicationDbContext())
            {
                var user = new User
                {
                    Username = tbUserUsernameTeacher.Text,
                    Email = tbUserEmailTeacher.Text,
                    Password = tbUserPasswordTeacher.Text,
                    DateOfBirth = DateTime.Parse(datePickerUserDateOfBirthTeacher.Text),
                    Role = "teacher"
                };

                _dbContext.Users.Add(user);
                _dbContext.SaveChanges();

                var teacher = new Teacher
                {
                    FullName = tbStudentFullName.Text,
                    UserId = user.IdUser,
                };

                _dbContext.Teachers.Add(teacher);
                _dbContext.SaveChanges();
                LoadDbStudents();
            }
        }

        private void btnDeleteTeacherById_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (_dbContext = new ApplicationDbContext())
                {
                    var teacher = new Teacher() { IdTeacher = int.Parse(tbTeacherDeleteById.Text) };
                    _dbContext.Teachers.Attach(teacher);

                    _dbContext.Teachers.Remove(teacher);
                    _dbContext.SaveChanges();
                    LoadDbTeacher();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadDbTeacher()
        {
            using (_dbContext = new ApplicationDbContext())
            {
                _dbContext.Enrollments.Load();
                _dbContext.Courses.Load();
                _dbContext.Teachers.Load();
                _dbContext.Students.Load();

                dataGridTeacher.ItemsSource = _dbContext.Teachers.Include(x => x.User).ToList();
            }
        }
    }
}
