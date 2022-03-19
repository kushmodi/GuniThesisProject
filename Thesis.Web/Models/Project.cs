using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Thesis.Web.Models
{
    [Table("Projects")]
    public class Project
    {
        [Display(Name = "Project Id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProjectId { get; set; }


        [Display(Name = "Project Title")]
        [Required]
        [MinLength(5, ErrorMessage = "{0} should have at least {1} characters")]
        [MaxLength(20, ErrorMessage = "{0} can have {1} characters")]
        public string ProjectTitle { get; set; }


        [Display(Name = "Project Description")]
        [Required]
        [MinLength(15, ErrorMessage = "{0} should have at least {1} characters")]
        [MaxLength(50, ErrorMessage = "{0} can have {1} characters")]
        public string ProjectDescription { get; set; }


        [Display(Name = "Project Start Date")]
        [Column(TypeName = "date")]
        public DateTime StartDate { get; set; }


        [Display(Name = "End Date")]
        [Column(TypeName = "date")]
        public DateTime EndDate { get; set; }


        #region Navigational Properties to the Subject Model 
        [Display(Name = "Subject Name")]
        [ForeignKey(nameof(Project.Subject))]
        public int SubjectId { get; set; }

        public Subject Subject { get; set; } //object to map with Subject Table 
        #endregion

        #region Navigational Properties to the Student Model 

        [Display(Name = "Student ID")]
        [ForeignKey(nameof(Project.Student))]
        public Guid StudentId { get; set; }
        public Student Student { get; set; } //foreign key to StudentId in Students Table

        #endregion


        #region Navigational Properties to the Faculty Model 
        [Display(Name = "Faculty Id")]
        [ForeignKey(nameof(Project.Faculty))]
        public Guid FacultyId { get; set; }  //foreign key to FacultyId in Faculty Table
        public Faculty Faculty { get; set; }
        #endregion


        #region Navigational Properties to the Submission Details Model 
        public ICollection<SubmissionDetail> SubmissionDetails { get; set; }
        #endregion








    }
}
