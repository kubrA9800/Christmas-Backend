using Microsoft.AspNetCore.Identity;

namespace ChristmasBackend.Models
{
	public class AppUser:IdentityUser
	{
		public string FullName { get; set; }
	}
}
