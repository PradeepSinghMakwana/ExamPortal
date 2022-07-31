using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamPortal.Models.ViewModels
{
    public class ClassVM:Class
    {
        public ClassVM(Class c) {
            this.class_coordinator = c.class_coordinator;
            this.class_id = c.class_id;
            this.course_name = c.course_name;
            this.semester = c.semester;
            this.year = c.year;
        }
        public string Pretty_Class_name
        {
            get
            {
                string class_name = course_name;
                string suffix = "";
                if (semester == 0)
                {
                    class_name += " " + year.ToString();
                    switch (year)
                    {
                        case 1:
                            suffix = "st";
                            break;
                        case 2:
                            suffix = "nd";
                            break;
                        case 3:
                            suffix = "rd";
                            break;
                        default:
                            suffix = "th";
                            break;
                    }
                    class_name += suffix + " year";
                }
                else
                {
                    int sem = (year - 1) * 2 + semester;
                    class_name += " " + sem.ToString();
                    switch (sem)
                    {
                        case 1:
                            suffix = "st";
                            break;
                        case 2:
                            suffix = "nd";
                            break;
                        case 3:
                            suffix = "rd";
                            break;
                        default:
                            suffix = "th";
                            break;
                    }
                    class_name += suffix + " semester";
                }
                return class_name;
            }
        }
    }
}