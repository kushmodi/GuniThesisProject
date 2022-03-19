using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Thesis.Web.Models
{
    [Table("Students")]
    public class Student
    {
        #region Navigation Properties to the MyIdentityUser Model

        [Display(Name = "Student ID")]
        [Key]
        [ForeignKey(nameof(Student.User))]
        public Guid StudentId { get; set; }

        public MyIdentityUser User { get; set; }

        #endregion

        [Display(Name = "Parent Name")]
        [MaxLength(25, ErrorMessage = "{0} can have {1} characters")]
        public string StudentParentName { get; set; }

        #region

        public Project Projects { get; set; }
        //public ICollection<Project> Projects { get; set; }
        #endregion
    }
}
