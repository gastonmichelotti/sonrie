using netCoreNew.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace netCoreNew.ViewModels
{
    public class HomeVM
    {
        public HomeVM()
        {

        }

    }

    public class LoginVM
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}