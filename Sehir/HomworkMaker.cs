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
    
    public partial class HomworkMaker
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HomworkMaker()
        {
            this.H_feedBack = new HashSet<H_feedBack>();
        }
    
        public int ID { get; set; }
        public string H_Name { get; set; }
        public string H_Code { get; set; }
        public string descript { get; set; }
        public Nullable<int> Price { get; set; }
        public byte[] transcript { get; set; }
        public byte[] img { get; set; }
        public Nullable<bool> approv { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<H_feedBack> H_feedBack { get; set; }
    }
}