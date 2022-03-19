using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace Thesis.Web.Models
{
    [Table("SubmissionDetails")]
    public class SubmissionDetail
    {
        [Display(Name = "Student Id")]
        [ForeignKey(nameof(SubmissionDetail.User))]      // foreign key to the object in the current model.
        public Guid Id { get; set; }           // User Id of the Project
        public MyIdentityUser User { get; set; }

        [Display(Name = "Submission ID")]
        [Key]
        public int SubmissionId { get; set; }

        #region
        [Display(Name = "Project Title")]
        [ForeignKey(nameof(SubmissionDetail.Project))]
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        #endregion


        [Display(Name = "Submission Description")]
        [Required]
        [MinLength(15, ErrorMessage = "{0} should have at least {1} characters")]
        [MaxLength(60, ErrorMessage = "{0} can have {1} characters")]
        public string SubmissionDescription { get; set; }


        [Display(Name = "Submission Due Date")]
        [Column(TypeName = "date")]
        [Required]
        public DateTime SubmissionDueDate { get; set; }


        [Display(Name = "Submission Date")]
        [Column(TypeName = "date")]
        [Required]
        public DateTime SubmissionDate { get; set; }


        #region Navigation Properties to the Project Model
        public ICollection<Project> Projects { get; set; }
        #endregion

        //file pending

        //approved / reject status create radio button

        //reviewed by to be introduced in version 2




    }
}
