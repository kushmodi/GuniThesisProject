using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Thesis.Web.Models
{
    [Table("Faculties")]
    public class Faculty
    {

        #region Navigational Properties to the MyIdentityUser model (1:1 mapping)
        [Display(Name = "Faculty ID")]
        [Key]
        [ForeignKey(nameof(Faculty.User))]
        public Guid FacultyId { get; set; }
        public MyIdentityUser User { get; set; }

        #endregion

        #region  Navigational Properties to the Subject model

        [Display(Name = "Subject Name")]
        [ForeignKey(nameof(Faculty.Subject))]
        public int SubjectId { get; set; }
        public Subject Subject { get; set; } //object to map with Subject Table 
        #endregion


        #region Navigational Properties to the Project Model
        //public ICollection<Project> Projects { get; set; }
        public Project Projects { get; set; }
        #endregion

        //add faculty type in version 2.0

    }
}
