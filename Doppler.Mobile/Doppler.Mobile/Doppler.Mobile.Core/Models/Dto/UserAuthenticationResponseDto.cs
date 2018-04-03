using System;
namespace Doppler.Mobile.Core.Models.Dto
{
    public class UserAuthenticationResponseDto
    {
        public string access_token { get; set; }

        public int accountId { get; set; }

        public string username { get; set; }

        public string issued_at { get; set; }

        public string expiration_date { get; set; }
    }
}
