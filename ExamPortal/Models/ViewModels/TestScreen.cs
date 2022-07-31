using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamPortal.Models.ViewModels
{
    public class TestScreen
    {
        public Test Test { get; set; }
        public Subject Subject { get; set; }
        public DateTime DateTime { get; set; }
        public Student Student { get; set; }
        public TestResult CurrentQuestion { get; set; }
        public IEnumerable<TestResult> Questions { get; set; }
        public IEnumerable<TestResult> AttemptedQuestions { get { return Questions.Where(q=>q.q_virtual_no<=Max_Q_Virtual_No).AsEnumerable(); } }
        public bool Submittable { get; set; } = false;
        public int Max_Q_Virtual_No { get; set; }
        public bool hasNextQuestion { get; set; } = false;
        public bool hasPreviousQuestion { get; set; } = false;
        //next previous remaining
    }
}