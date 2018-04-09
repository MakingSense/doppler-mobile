using System.Text.RegularExpressions;

namespace Doppler.Mobile.Helper
{
    public static class EmailHelper
    {
        private const string EmailRegex = "^([\\w\\.\\-]+)@([\\w\\-]+)((\\.(\\w){2,3})+)$";

        public static bool EmailIsValid(string email)
        {
            return Regex.Match(email, EmailRegex).Success;
        }
    }
}
