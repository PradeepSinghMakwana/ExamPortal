using ExamPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace ExamPortal.Data
{
    public class DateOfTestRepository
    {
        /*public IEnumerable<SelectListItem> GetDateOfTest()
        {
            using (var db = new ExamPortalEntities())
            {
                List<SelectListItem> dates = db.Tests.AsNoTracking().OrderBy(t => t.start_datetime).Select(s=>s.start_datetime.Date).Distinct().Select(s => new SelectListItem
                {
                    Value = s.ToShortDateString(),
                    Text = s.ToShortDateString()
                }).ToList();
                var datetip = new SelectListItem()
                {
                    Value = null,
                    Text = "--- Select Date ---"
                };
                dates.Insert(0, datetip);
                return new SelectList(dates, "Value", "Text");
            }
        }*/
        public string getTestDatesAsString() {

            List<DateTime> dates = new List<DateTime>();
            List<string> strdates = new List<string>();
            using (var db = new ExamPortalEntities())
            {
                dates = db.Tests.OrderBy(t => t.start_datetime).Select(s => s.start_datetime).ToList();
            }
            
            dates.ForEach(q => strdates.Add(q.ToString("dd-MM-yyyy")));
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(strdates);
            return json;
        }
        public IEnumerable<SelectListItem> GetStartTimeOfTestOnDate(DateTime date)
        {
            DateTime startDate = date.Date;
            DateTime endDate = date.Date.AddDays(1);
            List<Test> tests = new List<Test>();
            using (var db = new ExamPortalEntities())
            {
                tests = db.Tests.AsNoTracking().Where(t => t.start_datetime > startDate).Where(t=>t.end_datetime<endDate).OrderBy(t => t.end_datetime).ToList();
            }
            DateTime startTime = date.Date.AddHours(7);
            DateTime endTime = startTime.AddHours(12);
            List<SelectListItem> times = new List<SelectListItem>();
            for (DateTime time = startTime;time.AddMinutes(30) < endTime; time=time.AddMinutes(30))
            {
                bool found = false;
                foreach (Test test in tests) {
                    if ((time.AddMinutes(30) >= test.start_datetime) && (time <= test.end_datetime)) {
                        found = true;
                        break;
                    }
                }
                if (!found) {
                    times.Add(new SelectListItem()
                    {
                        Value = time.ToString(),
                        Text = time.ToShortTimeString()
                    });
                } else {
                    times.Add(new SelectListItem()
                    {
                        Value = null,
                        Text = time.ToShortTimeString()
                    });
                }        
            }
            var timetip = new SelectListItem()
            {
                Value = null,
                Text = "--- Select Start Time ---"
            };
            times.Insert(0, timetip);
            return new SelectList(times, "Value", "Text");
        }
        public IEnumerable<SelectListItem> GetEndTimeOfTestByStartTime(DateTime startTime)
        {
            DateTime startDate = startTime.Date;
            DateTime endDate = startTime.Date.AddDays(1);
            List<Test> tests = new List<Test>();
            using (var db = new ExamPortalEntities())
            {
                tests = db.Tests.AsNoTracking().Where(t => t.start_datetime > startDate).Where(t => t.end_datetime < endDate).OrderBy(t => t.end_datetime).ToList();
            }
            /*List<Tuple<DateTime, DateTime>> disabledTimes = new List<Tuple<DateTime, DateTime>>();
            using (var db = new ExamPortalEntities())
            {
                disabledTimes = db.Tests.AsNoTracking().Where(t => t.start_datetime.Date == startTime.Date).OrderBy(t => t.end_datetime).Select(s => new Tuple<DateTime, DateTime>(s.start_datetime, s.end_datetime)).Distinct().ToList();
            }*/
            DateTime endTime = startTime.Date.AddHours(12+7);
            List<SelectListItem> times = new List<SelectListItem>();
            for (DateTime time = startTime.AddMinutes(30); time < endTime; time = time.AddMinutes(30))
            {
                bool found = false;
                foreach (Test test in tests)
                {
                    if ((time >= test.start_datetime) && (time <= test.end_datetime.AddMinutes(30)))
                    {
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    times.Add(new SelectListItem()
                    {
                        Value = time.ToString(),
                        Text = time.ToShortTimeString()
                    });
                }
                else
                {
                    break;
                    /*times.Add(new SelectListItem()
                    {
                        Value = null,
                        Text = time.ToShortTimeString()
                    });
                    */
                }
            }
            var timetip = new SelectListItem()
            {
                Value = null,
                Text = "--- Select End Time ---"
            };
            times.Insert(0, timetip);
            return new SelectList(times, "Value", "Text");
        }
        
    }
}