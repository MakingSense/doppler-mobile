using System;
namespace Doppler.Mobile.Core.Models.Dto
{
    public class UserAuthenticationDto
    {
        public string grant_type { get; set; }

        public string username { get; set; }

        public string password { get; set; }
    }
}
