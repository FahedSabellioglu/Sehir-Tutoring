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
    
    public partial class HomeworkMaker
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HomeworkMaker()
        {
            this.H_feedBack = new HashSet<H_feedBack>();
            this.HomeworkRequests = new HashSet<HomeworkRequest>();
        }
    
        public int ID { get; set; }
        public string title { get; set; }
        public string C_Code { get; set; }
        public byte[] img { get; set; }
        public string descrip { get; set; }
        public int price { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<H_feedBack> H_feedBack { get; set; }
        public virtual Homework Homework { get; set; }
        public virtual User User { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HomeworkRequest> HomeworkRequests { get; set; }
    }
}