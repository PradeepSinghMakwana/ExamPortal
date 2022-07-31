using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamPortal.Models.ViewModels
{
    public class ModifyTestVM:Test
    {
        ExamPortalEntities db = new ExamPortalEntities();
        public IEnumerable<Subject> Subjects { get { return db.Subjects.AsEnumerable() ; } }
        public IEnumerable<DateTime> DateOfTests { get { return db.Tests.Select(t=>t.start_datetime.Date); } }
        public IEnumerable<Tuple<DateTime,TimeSpan>> StartTimeNDurationOfTests(DateTime date) { return db.Tests.Where(t=>t.start_datetime.Date==date).Select(t => new Tuple<DateTime,TimeSpan>(t.start_datetime.ToLocalTime(),t.end_datetime.Subtract(t.start_datetime))); }
    }
}