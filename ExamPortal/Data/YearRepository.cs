using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExamPortal.Data
{
    public class YearRepository
    {
        public IEnumerable<SelectListItem> GetYears()
        {
            List<SelectListItem> years = new List<SelectListItem>();
            for (int i = 1; i <= 4; i++)
            {
                years.Add(new SelectListItem
                {
                    Value = i.ToString(),
                    Text = "Year " + i.ToString()
                });
            }
            var yeartip = new SelectListItem()
            {
                Value = null,
                Text = "--- Select Difficulty ---"
            };
            years.Insert(0, yeartip);
            return new SelectList(years, "Value", "Text");
        }
    }
}