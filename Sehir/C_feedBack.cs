//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Sehir
{
    using System;
    using System.Collections.Generic;
    
    public partial class C_feedBack
    {
        public int S_ID { get; set; }
        public int T_ID { get; set; }
        public string C_Name { get; set; }
        public string C_Code { get; set; }
        public string review { get; set; }
        public System.DateTime reviewDate { get; set; }
    
        public virtual Lecturer Lecturer { get; set; }
        public virtual User User { get; set; }
    }
}
