using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamPortal.Models
{
    public class AddQuestionToRepositoryVM
    {
        ExamPortalEntities db = new ExamPortalEntities();
        public ViewModels.QuestionVM question { get; set; }
        public AddQuestionToRepositoryVM() {
            question = new ViewModels.QuestionVM();
        }
    }
}