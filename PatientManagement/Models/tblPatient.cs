//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PatientManagement.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblPatient
    {
        public int Id { get; set; }
        public byte[] PatImage { get; set; }
        public string PatName { get; set; }
        public string PatContact { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime EntryDate { get; set; }
    }
}
