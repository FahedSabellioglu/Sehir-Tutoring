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
    
    public partial class lec_request
    {
        public int ID { get; set; }
        public string C_Code { get; set; }
        public string descrip { get; set; }
        public byte[] img { get; set; }
        public int price { get; set; }
    
        public virtual Course Course { get; set; }
        public virtual User User { get; set; }
    }
}
