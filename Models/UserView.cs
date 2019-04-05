using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TP1_BookmarksManager.Models;

namespace TP1_BookmarksManager.Models
{
    public class UserView
    {
        private const string REGEX_Identification =
            @"^((?!^Name$)[-a-zA-Z0-9àâäçèêëéìîïòôöùûüÿñÀÂÄÇÈÊËÉÌÎÏÒÔÖÙÛÜ_. '])+$";

        [Required]
        [RegularExpression(REGEX_Identification, ErrorMessage = "Contains forbidden characters.")]
        [StringLength(50, ErrorMessage = "UserName must contains at least {2} characters.", MinimumLength = 6)]
        [DataType(DataType.Text)]
        public string UserName { get; set; }

        [Required]
        [RegularExpression(REGEX_Identification, ErrorMessage = "Contains forbidden characters.")]
        [StringLength(50, ErrorMessage = "Password must contains at least {2} characters.", MinimumLength = 6)]
        [DataType(DataType.Password)]

        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmation")]
        [Compare("Password", ErrorMessage = "Password confirmation doesn't match with password.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [RegularExpression(REGEX_Identification, ErrorMessage = "Contains forbidden characters.")]
        public string FirstName { get; set; }

        [Required]
        [RegularExpression(REGEX_Identification, ErrorMessage = "Contains forbidden characters.")]
        public string LastName { get; set; }

        public bool Admin { get; set; }
        
        [Required(ErrorMessage ="Select one item")]
        public SexType Sex { get; set; }


        [Required]
        [DataType(DataType.DateTime)]
        public System.DateTime CreationDate { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Invalide")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public System.DateTime BirthDate { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public UserView()
        {
            UserName = "";
            Password = "";
            FirstName = "";
            LastName = "";
            Email = "";
            Sex = SexType.Homme;

        }
    }
}