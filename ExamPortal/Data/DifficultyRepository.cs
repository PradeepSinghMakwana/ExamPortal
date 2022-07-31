using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExamPortal.Data
{
    public class DifficultyRepository
    {
        public IEnumerable<SelectListItem> GetDifficultyLevels()
        {
            List<SelectListItem> levels = new List<SelectListItem>();
            for (int i = 1; i <= 5; i++)
            {
                levels.Add(new SelectListItem
                {
                    Value = i.ToString(),
                    Text = "L-" + i.ToString()
                });
            }
            var leveltip = new SelectListItem()
            {
                Value = null,
                Text = "--- Select Difficulty ---"
            };
            levels.Insert(0, leveltip);
            return new SelectList(levels, "Value", "Text");
        }
    }
}