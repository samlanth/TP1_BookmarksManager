using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TP1_BookmarksManager.Scripts
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

        public enum Sex { Homme = 1, Femme = 2, Autres = 3 };

        [Required(ErrorMessage ="Select one item")]
        public Sex Sexes { get; set; }


        [Required]
        [DataType(DataType.DateTime)]
        public System.DateTime CreationDate { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public System.DateTime BirthDate { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Email must contains at least {2} characters.", MinimumLength = 6)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public UserView()
        {
            UserName = "";
            Password = "";
            FirstName = "";
            LastName = "";
            Sexes = 0;
            BirthDate = System.DateTime.Now;
            Email = "";

        }
        public class LoginView
        {
            [Required]
            [RegularExpression(REGEX_Identification, ErrorMessage = "Contains forbidden characters.")]
            [StringLength(50, ErrorMessage = "UserName must contains at least {2} characters.", MinimumLength = 6)]
            [DataType(DataType.Text)]
            public string UserName { get; set; }
            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }

    }

}