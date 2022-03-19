using Microsoft.AspNetCore.Identity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Thesis.Web.Models
{
    public class MyIdentityUser : IdentityUser<Guid>
    {

        [Display(Name = "Name")]
        [Required(ErrorMessage = "{0} is Required")]
        [MinLength(2, ErrorMessage = "{0} requires minimum {1} characters")]
        [StringLength(50, ErrorMessage = "{0} cannot have more than {1} characters")]
        public string DisplayName { get; set; }

        [Display(Name = "Date Of Birth")]
        [Required]
        [PersonalData]
        [Column(TypeName = "date")]
        public DateTime DateOfBirth { get; set; }

        /*
        [Required]   //step2
        [Display(Name = "Gender")]
        [PersonalData]
        public MyIdentityGenders Gender { get; set; }  // enum to be created
        */
        //role type :

        [Display(Name = "Is Admin User")]
        [Required]
        public bool IsAdminUser { get; set; }


        #region Navigational Properties to the Faculty Model (1:0 mapping)
        /*public ICollection<Faculty> Faculties { get; set; }
        public ICollection<Student> Students { get; set; }
        */

        public Student Student { get; set; }
        public Faculty Faculty { get; set; }


        #endregion



    }
}



/*
namespace Thesis.Web.Models
{
    public class MyIdentityUser
    {
    }
}
*/