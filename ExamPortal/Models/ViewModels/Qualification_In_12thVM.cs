using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ExamPortal.Models.ViewModels
{
    public class Qualification_In_12thVM : Student_12th_Qualification
    {
        [DisplayName("Subject")]
        public string subject_name { get; set; }

        public bool has_subject { get; set; }

        [Column(TypeName = "numeric")]
        [DisplayName("Marks In Percent")]
        [RegularExpression(@"^\d{0,2}(\.\d{1,2})?$", ErrorMessage = "Invalid Marks in 12th subject")]
        public new decimal percent_marks { get; set; }


        public Qualification_In_12thVM(int subject_code, string subject_name, bool has_subject=false, decimal percent_marks=0.00M,int scholar_no=0)
        {
            this.scholar_no = scholar_no;
            this.subject_code = subject_code;
            this.subject_name = subject_name;
            this.has_subject = has_subject;
            this.percent_marks = percent_marks;
        }

        public Student_12th_Qualification extractStudent_12th_Qualification()
        {
            if (has_subject)
            {
                Student_12th_Qualification qua = new Student_12th_Qualification();
                qua.percent_marks = percent_marks/100;
                qua.scholar_no = scholar_no;
                qua.subject_code = subject_code;
                return qua;
            }
            return null;
        }
    }
}