//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ExamPortal.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Teach
    {
        public int faculty_id { get; set; }
        public int class_id { get; set; }
        public int subject_code { get; set; }
    
        public virtual Class Class { get; set; }
        public virtual Subject Subject { get; set; }
        public virtual Teacher Teacher { get; set; }
    }
}
