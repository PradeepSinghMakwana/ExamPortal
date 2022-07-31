using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamPortal.Models
{
    public class UpcomingTestVM
    {
        public int scholarNo { get; set; }
        
        public IEnumerable<UnregisteredTest> UpcomingTests { get; set; }

        public UpcomingTestVM(int id) {
            this.scholarNo = id;
        }
    }
}