using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExamPortal.Models.ViewModels
{
    public partial class TeacherVM:Teacher
    {
        
        public string username { get; set; }
        public string password { get; set; }
        
    }
}