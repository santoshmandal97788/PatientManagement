using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PatientManagement.Models.ViewModel
{
    public class PatientViewModel
    {
        public int Id { get; set; }
        public byte[] PatImage { get; set; }
        public string PatName { get; set; }
        public string PatContact { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime EntryDate { get; set; }
    }
}