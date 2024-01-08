using System.Collections.Generic;

namespace Adaro.Centralize.Authorization.Users.Profile.Dto
{
    public class GenerateGoogleAuthenticatorKeyOutput
    {
        public string QrCodeSetupImageUrl { get; set; }
        public string GoogleAuthenticatorKey { get; set; }
    }
}
