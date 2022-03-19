using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Thesis.Web.Models
{
    [Table("Subjects")]
    public class Subject
    {

        [Display(Name = "Subject ID")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SubjectId { get; set; }


        [Display(Name = "Subject Name")]
        [Required]
        [MinLength(5, ErrorMessage = "{0} should have at least {1} characters")]
        [MaxLength(20, ErrorMessage = "{0} can have {1} characters")]
        public string SubjectName { get; set; }


        #region Navigation Properties to Faculty and Project Model
        public ICollection<Faculty> Faculties { get; set; }
        public ICollection<Project> Projects { get; set; }

        #endregion
    }
}
