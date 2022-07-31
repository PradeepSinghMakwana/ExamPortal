using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamPortal.Models.ViewModels
{
    public class StudentTestResultVM:Test
    {
        public IEnumerable<TestResult> testResults { get; set; }
    }
}