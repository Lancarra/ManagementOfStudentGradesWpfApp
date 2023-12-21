using ManagementOfStudentGradesWpfApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementOfStudentGradesWpfApp.Controllers
{
    public class DatabaseController
    {
        private readonly ApplicationDbContext _dbContext;

        public DatabaseController()
        {
            _dbContext = new ApplicationDbContext();
        }

        public User ValidateUserCredentials(string loginData, string password)
        {
            // Проверка учетных данных пользователя в базе данных
            var user = _dbContext.Users.SingleOrDefault(u => (u.Username == loginData && u.Password == password) || (u.Email == loginData && u.Password == password));
            return user;
        }
    }

}
