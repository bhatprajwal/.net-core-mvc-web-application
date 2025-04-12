using Microsoft.AspNetCore.Identity;

namespace Entity;

/// <summary>
/// Application User
/// </summary>
[Serializable]
public class ApplicationUser : IdentityUser
{
    #region Constructor
    /// <summary>
    /// Constructor
    /// </summary>
    public ApplicationUser()
    {} 
    #endregion

    /// <summary>
    /// Full Name
    /// </summary>
    public string FullName { get; set; }

    /// <summary>
    /// Profile Picture
    /// </summary>
    public string ProfilePictureUrl { get; set; }
}
