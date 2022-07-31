using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExamPortal.Models.ViewModels
{
    public class QuestionVM : Question
    {
        ExamPortalEntities db;
        Question question;
        public int new_q_id { get
            {
                int? max = db.Questions.Max(q => q.q_id);
                return ((max == null) ? 0 : (int)max) + 1;
            }
        }
        public IEnumerable<SelectListItem> Subjects { get; set; }

        public IEnumerable<SelectListItem> DifficultyLevels { get; set; }

        public bool use_option1 { get; set; }
        public bool use_option2 { get; set; }
        public bool use_option3 { get; set; }
        public bool use_option4 { get; set; }
        public new string option1 { get; set; }
        public new string option2 { get; set; }
        public new string option3 { get; set; }
        public new string option4 { get; set; }
        public new bool is_option1_correct { get; set; }
        public new bool is_option2_correct { get; set; }
        public new bool is_option3_correct { get; set; }
        public new bool is_option4_correct { get; set; }
        public string strTestIds { get; set; }
        public QuestionVM()
        {
            db = new ExamPortalEntities();
            question = new Question();
            this.use_option1 = !string.IsNullOrWhiteSpace(question.option1);
            this.use_option2 = !string.IsNullOrWhiteSpace(question.option2);
            this.use_option3 = !string.IsNullOrWhiteSpace(question.option3);
            this.use_option4 = !string.IsNullOrWhiteSpace(question.option4);
            this.option1 = string.IsNullOrWhiteSpace(question.option1) ? "" : question.option1;
            this.option2 = string.IsNullOrWhiteSpace(question.option2) ? "" : question.option2;
            this.option3 = string.IsNullOrWhiteSpace(question.option3) ? "" : question.option3;
            this.option4 = string.IsNullOrWhiteSpace(question.option4) ? "" : question.option4;
            this.is_option1_correct = (question.is_option1_correct.HasValue && question.is_option1_correct.Value == (true)) ? true : false;
            this.is_option2_correct = (question.is_option2_correct.HasValue && question.is_option2_correct.Value == (true)) ? true : false;
            this.is_option3_correct = (question.is_option3_correct.HasValue && question.is_option3_correct.Value == (true)) ? true : false;
            this.is_option4_correct = (question.is_option4_correct.HasValue && question.is_option4_correct.Value == (true)) ? true : false;
        }

        public QuestionVM(Question question)
        {
            db = new ExamPortalEntities();
            this.q_id = question.q_id;
            this.Test_Question = question.Test_Question;
            this.strTestIds = string.Join(" ", db.Test_Question.Where(q=>q.question_id==question.q_id).Select(t => (t.test_id.ToString())).DefaultIfEmpty("").ToArray());
            this.Subject = question.Subject;
            this.question_text = question.question_text;
            this.use_option1 = !string.IsNullOrWhiteSpace(question.option1);
            this.use_option2 = !string.IsNullOrWhiteSpace(question.option2);
            this.use_option3 = !string.IsNullOrWhiteSpace(question.option3);
            this.use_option4 = !string.IsNullOrWhiteSpace(question.option4);
            this.option1 = string.IsNullOrWhiteSpace(question.option1) ? "" : question.option1;
            this.option2 = string.IsNullOrWhiteSpace(question.option2) ? "" : question.option2;
            this.option3 = string.IsNullOrWhiteSpace(question.option3) ? "" : question.option3;
            this.option4 = string.IsNullOrWhiteSpace(question.option4) ? "" : question.option4;
            this.is_option1_correct = (question.is_option1_correct.HasValue && question.is_option1_correct.Value == (true)) ? true : false;
            this.is_option2_correct = (question.is_option2_correct.HasValue && question.is_option2_correct.Value == (true)) ? true : false;
            this.is_option3_correct = (question.is_option3_correct.HasValue && question.is_option3_correct.Value == (true)) ? true : false;
            this.is_option4_correct = (question.is_option4_correct.HasValue && question.is_option4_correct.Value == (true)) ? true : false;
        }
        public static Question ExtractQuestionFromStudentTestRecord(Student_Test_Record r) {
            Question q = new Question();
            q.is_option1_correct = r.is_option1_correct;
            q.is_option2_correct = r.is_option2_correct;
            q.is_option3_correct = r.is_option3_correct;
            q.is_option4_correct = r.is_option4_correct;
            q.option1 = r.option1;
            q.option2 = r.option2;
            q.option3 = r.option3;
            q.option4 = r.option4;
            q.question_text = r.question_text;
            q.q_id = r.q_id;
            return q;
        }

        public static Question ConvertToQuestion(QuestionVM questionVM) {
            Question question = new Question();
            question.difficulty_level = questionVM.difficulty_level;
            if (!string.IsNullOrEmpty(questionVM.question_text))
            {
                question.question_text = questionVM.question_text;
            }
            if (questionVM.use_option1 && (!string.IsNullOrEmpty(questionVM.option1)))
            {
                question.option1 = questionVM.option1;
                question.is_option1_correct = questionVM.is_option1_correct;
            }
            else if (!questionVM.use_option1 || (string.IsNullOrEmpty(questionVM.option1)))
            {
                question.option1 = null;
                question.is_option1_correct = null;
            }

            if (questionVM.use_option2 && (!string.IsNullOrEmpty(questionVM.option2)))
            {
                question.option2 = questionVM.option2;
                question.is_option2_correct = questionVM.is_option2_correct;
            }
            else if (!questionVM.use_option2 || (string.IsNullOrEmpty(questionVM.option2)))
            {
                question.option2 = null;
                question.is_option2_correct = null;
            }
            if (questionVM.use_option3 && (!string.IsNullOrEmpty(questionVM.option3)))
            {
                question.option3 = questionVM.option3;
                question.is_option3_correct = questionVM.is_option3_correct;
            }
            else if (!questionVM.use_option3 || (string.IsNullOrEmpty(questionVM.option3)))
            {
                question.option3 = null;
                question.is_option3_correct = null;
            }
            if (questionVM.use_option4 && (!string.IsNullOrEmpty(questionVM.option4)))
            {
                question.option4 = questionVM.option4;
                question.is_option4_correct = questionVM.is_option4_correct;
            }
            else if (!questionVM.use_option4 || (string.IsNullOrEmpty(questionVM.option4)))
            {
                question.option4 = null;
                question.is_option4_correct = null;
            }
            return question;
        }
        
    }
}