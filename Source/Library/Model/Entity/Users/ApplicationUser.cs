using Microsoft.AspNetCore.Identity;

namespace Models;

[Serializable]
public class ApplicationUser : IdentityUser
{
    public string GoogleId { get; set; }

    public ApplicationUser()
    {
        
    }
}
