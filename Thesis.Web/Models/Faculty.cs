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
        public int FacultyId { get; set; }

        #endregion

        [Display(Name = "Faculty Name")]
        [Required]
        [StringLength(20, ErrorMessage = "{0} cannot have more than {1} character")]
        [MinLength(3, ErrorMessage = "{0} should have at least {1} character")]
        public string FacultyName { get; set; }

        #region  Navigational Properties to the Subject model

        [Display(Name = "Subject Name")]
        [ForeignKey(nameof(Faculty.Subject))]
        public int SubjectId { get; set; }
        public Subject Subject { get; set; } //object to map with Subject Table 
        #endregion


        #region Navigational Properties to the Project Model
        //public ICollection<Project> Projects { get; set; }
        public ICollection<Project> Projects { get; set; }
        #endregion

        //add faculty type in version 2.0

    }
}
