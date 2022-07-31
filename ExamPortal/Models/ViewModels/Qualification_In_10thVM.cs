using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ExamPortal.Models.ViewModels
{
    public class Qualification_In_10thVM : Student_10th_Qualification
    {

        public Qualification_In_10thVM(int subject_code, string subject_name, decimal percent_marks = 0.00M, int scholar_no = 0)
        {
            this.scholar_no = scholar_no;
            this.subject_code = subject_code;
            this.subject_name = subject_name;
            this.percent_marks = percent_marks;
        }

        [DisplayName("Subject")]
        public string subject_name { get; set; }

        [Column(TypeName = "numeric")]
        [DisplayName("Marks In Percent")]
        [RegularExpression(@"^\d{0,2}(\.\d{1,2})?$", ErrorMessage = "Invalid Marks in 10th subject")]
        public new decimal percent_marks { get; set; }

        public Student_10th_Qualification extractStudent_10th_Qualification()
        {
            Student_10th_Qualification qua = new Student_10th_Qualification();
            qua.percent_marks = percent_marks/100;
            qua.scholar_no = scholar_no;
            qua.subject_code = subject_code;
            return qua;
        }
    }
}