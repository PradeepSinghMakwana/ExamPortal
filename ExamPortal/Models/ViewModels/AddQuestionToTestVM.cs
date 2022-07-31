using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExamPortal.Models.ViewModels
{
    public class AddQuestionToTestVM
    {
        ExamPortalEntities db = new ExamPortalEntities();
        public IEnumerable<Test> Tests { get; set; }
        public IEnumerable<QuestionVM> Questions { get; set; }

        public List<QuestionVM> QuestionsInUnit1 { get; set; }
        public List<QuestionVM> QuestionsInUnit2 { get; set; }
        public List<QuestionVM> QuestionsInUnit3 { get; set; }
        public List<QuestionVM> QuestionsInUnit4 { get; set; }
        public List<QuestionVM> QuestionsInUnit5 { get; set; }
        
        public int NoOfQuestionsInUnit1 { get; set; }
        public int NoOfQuestionsInUnit2 { get; set; }
        public int NoOfQuestionsInUnit3 { get; set; }
        public int NoOfQuestionsInUnit4 { get; set; }
        public int NoOfQuestionsInUnit5 { get; set; }

        public int faculty_id { get; set; }

        [Display(Name="Subject")]
        //public IEnumerable<SelectListItem> Subjects { get; set; }
        public IEnumerable<Subject> Subjects { get; set; }
        public int question_subject_code;

        //public IEnumerable<DateTime> DateOfTests { get { return db.Tests.Select(t => t.start_datetime.Date); } }
        //public IEnumerable<Tuple<DateTime, TimeSpan>> StartTimeNDurationOfTests(DateTime date) { return db.Tests.Where(t => t.start_datetime.Date == date).Select(t => new Tuple<DateTime, TimeSpan>(t.start_datetime.ToLocalTime(), t.end_datetime.Subtract(t.start_datetime))); }
        
        public AddQuestionToTestVM() {
            
        }
    }
}