
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
    
public partial class User
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public User()
    {

        this.C_feedBack = new HashSet<C_feedBack>();

        this.CourseRequests = new HashSet<CourseRequest>();

        this.H_feedBack = new HashSet<H_feedBack>();

        this.homework_request = new HashSet<homework_request>();

        this.HomeworkMakers = new HashSet<HomeworkMaker>();

        this.HomeworkRequests = new HashSet<HomeworkRequest>();

        this.lec_request = new HashSet<lec_request>();

        this.Lecturers = new HashSet<Lecturer>();

    }


    public int id { get; set; }

    public string U_name { get; set; }

    public string U_Surname { get; set; }

    public string dept { get; set; }

    public int enr_year { get; set; }

    public string mail { get; set; }

    public string pass { get; set; }

    public byte[] img { get; set; }

    public byte[] transcript { get; set; }



    public virtual AdminTable AdminTable { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<C_feedBack> C_feedBack { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<CourseRequest> CourseRequests { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<H_feedBack> H_feedBack { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<homework_request> homework_request { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<HomeworkMaker> HomeworkMakers { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<HomeworkRequest> HomeworkRequests { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<lec_request> lec_request { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<Lecturer> Lecturers { get; set; }

}

}
