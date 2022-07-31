using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExamPortal.Models.ViewModels
{
    public class TestVM:Test
    {
        [DisplayName("Date Of Test")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime dateOfTest { get; set; }

        public string datesContainingTest { get; set; }
        
        public IEnumerable<SelectListItem> Subjects { get; set; }

        [DisplayName("Start Time Of Test")]
        public IEnumerable<SelectListItem> StartTimeOfTests { get; set; }
        [DataType(DataType.Time), DisplayFormat(DataFormatString = "{0:hh:mm}", ApplyFormatInEditMode = true)]
        public DateTime startTimeOfTest { get; set; }

        [DisplayName("End Time Of Test")]
        public IEnumerable<SelectListItem> EndTimeOfTests { get; set; }
        [DataType(DataType.Time), DisplayFormat(DataFormatString = "{0:hh:mm}", ApplyFormatInEditMode = true)]
        public DateTime endTimeOfTest { get; set; }
    }
}