using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExamPortal.Data
{
    public class SemesterRepository
    {
        public IEnumerable<SelectListItem> GetSemesters()
        {
            List<SelectListItem> sems = new List<SelectListItem>();
            for (int i = 1; i <= 8; i++)
            {
                sems.Add(new SelectListItem
                {
                    Value = i.ToString(),
                    Text = "Semester " + i.ToString()
                });
            }
            var semtip = new SelectListItem()
            {
                Value = null,
                Text = "--- Select Difficulty ---"
            };
            sems.Insert(0, semtip);
            return new SelectList(sems, "Value", "Text");
        }
    }
}