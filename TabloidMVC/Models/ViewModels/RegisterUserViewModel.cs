using System.Collections.Generic;

namespace TabloidMVC.Models.ViewModels
{
    public class RegisterUserViewModel
    {
        public UserProfile UserProfile { get; set; }
        public List<UserType> UserTypes { get; set; }
    }
}
