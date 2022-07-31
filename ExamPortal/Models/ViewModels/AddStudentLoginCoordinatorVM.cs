using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExamPortal.Models.ViewModels
{
    public class AddStudentLoginCoordinatorVM:UserLogin
    {
        public string role { get; set; }
        public IEnumerable<UserLogin> UnregisteredStudents { get; set; }

        public AddStudentLoginCoordinatorVM() {

        }
    }
}