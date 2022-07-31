using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamPortal.Models.ViewModels
{
    public class TestResult:QuestionVM
    {
        public int q_virtual_no { get; set; }
        public bool is_student_selected_option1 { get; set; }
        public bool is_student_selected_option2 { get; set; }
        public bool is_student_selected_option3 { get; set; }
        public bool is_student_selected_option4 { get; set; }
        public bool is_marked_for_review { get; set; }

        public TestResult(Student_Test_Record r) {
            Question q = QuestionVM.ExtractQuestionFromStudentTestRecord(r);
            QuestionVM m = new QuestionVM(q);
            this.is_student_selected_option1 = r.is_student_selected_option1.HasValue&&r.is_student_selected_option1.Value;
            this.is_student_selected_option2 = r.is_student_selected_option2.HasValue&&r.is_student_selected_option2.Value;
            this.is_student_selected_option3 = r.is_student_selected_option3.HasValue && r.is_student_selected_option3.Value;
            this.is_student_selected_option4 = r.is_student_selected_option4.HasValue && r.is_student_selected_option4.Value;
            this.q_virtual_no = r.q_virtual_id;
            this.is_marked_for_review = (!this.is_student_selected_option1) && (!this.is_student_selected_option2) && (!this.is_student_selected_option3) && (!this.is_student_selected_option4);
        }
    }
}