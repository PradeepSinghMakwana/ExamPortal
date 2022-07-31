using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamPortal.Models.ViewModels
{
    public class StudentsGrid:Student_Current_Qualification
    {
        public string unique_name { get; set; }
        public string photo { get; set; }
        public string Pretty_Class_name { get; set; }
    }
}