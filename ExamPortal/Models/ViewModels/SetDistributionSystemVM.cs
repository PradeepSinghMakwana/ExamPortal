using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExamPortal.Models.ViewModels
{
    public class SetDistributionSystemVM
    {
        public int ColCount { get; set; }
        public int RowCount { get; set; }
        public List<string> IPs { get; set; }
        public string MinIPv4 { get; set; }
        public string MaxIPv4 { get; set; }
        public IList<SelectListItem> IPaddresses { get; set; }
    }
}