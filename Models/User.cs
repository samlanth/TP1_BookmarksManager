//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TP1_BookmarksManager.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web;

    public enum SexType { Homme = 0, Femme = 1, Autre = 2}
    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            this.Bookmarks = new HashSet<Bookmark>();
            Id = 0;
            Admin = false;
            UserName = "";
            Password = "";
            FirstName = "";
            LastName = "";
            Sex = (int)SexType.Homme;
            CreationDate = DateTime.Now;
            BirthDate = DateTime.Now;
            Email = "";
        }

        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Admin { get; set; }
        public int Sex { get; set; }
        public System.DateTime CreationDate { get; set; }
        public System.DateTime BirthDate { get; set; }
        public string Email { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Bookmark> Bookmarks { get; set; }

        public string GetFullName()
        {
            return FirstName + " " + LastName;
        }
        public static User FromUserView(UserView userView)
        {
            User user = new User();
            user.Id = 0;
            user.Admin = false;
            user.UserName = userView.UserName;
            user.Password = userView.Password;
            user.FirstName = userView.FirstName;
            user.LastName = userView.LastName;
            user.Sex = (int)userView.Sex;
            user.BirthDate = userView.BirthDate;
            user.Email = userView.Email;
            return user;
        }
        public UserView CreateUserView()
        {
            UserView userView = new UserView();
            userView.UserName = UserName;
            userView.Password = Password;
            userView.ConfirmPassword = Password;
            userView.FirstName = FirstName;
            userView.LastName = LastName;
            userView.Sex = (SexType)Sex;
            userView.BirthDate = BirthDate;
            userView.Email = Email;
            return userView;
        }
        public void UpdateWithUserView(UserView userView)
        {
            UserName = userView.UserName;
            Password = userView.Password;
            FirstName = userView.FirstName;
            LastName = userView.LastName;
            Sex = (int)userView.Sex;
            BirthDate = userView.BirthDate;
            Email = userView.Email;
        }
        public void Update(User user)
        {
            Id = user.Id;
            UserName = user.UserName;
            Password = user.Password;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Sex = user.Sex;
            BirthDate = user.BirthDate;
            Email = user.Email;
        }
    }
    public class UserView
    {
        private const string REGEX_Identification = @"^((?!^Name$)[-a-zA-Z0-9àâäçèêëéìîïòôöùûüÿñÀÂÄÇÈÊËÉÌÎÏÒÔÖÙÛÜ_. '])+$";

        [Required]
        public string UserName { get; set; }

        [Required]
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

        public SexType Sex { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Invalide")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime BirthDate { get; set; }
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
    public class UserLoginView
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
    public static class OnlineUsers
    {
        public static void AddSessionUser(User user)
        {
            if (HttpRuntime.Cache["OnLineUsers"] == null)
            {
                HttpRuntime.Cache["OnLineUsers"] = new List<User>();
            }
            ((List<User>)HttpRuntime.Cache["OnLineUsers"]).Add(user);
            HttpContext.Current.Session["User"] = user;
            HttpContext.Current.Session.Timeout = 5;
        }
        public static void RemoveSessionUser()
        {
            ((List<User>)HttpRuntime.Cache["OnLineUsers"]).Remove(GetSessionUser());
            HttpContext.Current.Session.Abandon();
        }
        public static User GetSessionUser()
        {
            try
            {
                return (User)HttpContext.Current.Session["User"];
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static bool IsOnLine(int userId)
        {
            foreach (User user in (List<User>)HttpRuntime.Cache["OnLineUsers"])
            {
                if (user.Id == userId)
                    return true;
            }
            return false;
        }
    }
}