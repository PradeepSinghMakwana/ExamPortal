using ExamPortal.Models;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ExamPortal.Models.ViewModels;

namespace ExamPortal.Data
{
    public class TeachesRepository
    {

        public IEnumerable<Object[]> GetTeaches()
        {
            List<Teach> teaches = new List<Teach>();
            using (var db = new ExamPortalEntities())
            {
                teaches = db.Teaches.Include(c => c.Class).Include(tr => tr.Teacher).Include(s => s.Subject).ToList();
            }
            List<Object[]> teachList = new List<Object[]>();
            teaches.ForEach(a => teachList.Add(new Object[3]{ new ClassVM(a.Class), a.Teacher, a.Subject }));
            return teachList;
        }
    }
}