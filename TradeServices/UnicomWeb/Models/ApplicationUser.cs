using Microsoft.AspNet.Identity.EntityFramework;

namespace UnicomWeb.Models
{
    public class ApplicationUser : IdentityUser
    {
        public int Year { get; set; }
        public bool LoginApproved { get; set; }
        public ApplicationUser()
        {

        }
    }
}