using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamPortal.Models.ViewModels
{
    public class ViewQuestionVM:Question
    {
        ExamPortalEntities db = new ExamPortalEntities();
        Question question = new Question();
        public bool selected { get; set; }
        public ViewQuestionVM()
        {
            question = db.Questions.FirstOrDefault();
        }
    }
}