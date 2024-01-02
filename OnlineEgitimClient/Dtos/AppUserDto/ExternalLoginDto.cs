namespace OnlineEgitimClient.Dtos.AppUserDto
{
    public class ExternalLoginDto
    {
        public string userEmail { get; set; }
        public string userName { get; set; }
        public string userId { get; set; }
        public string userFirstName { get; set; }
        public string userLastName { get; set; }
        public string provider { get; set; }
        public string accessToken { get; set; }
    }
}

//var userEmail = claimsIdentity?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
//var userName = claimsIdentity?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
//var userId = claimsIdentity?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
//var userFirstName = claimsIdentity?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.GivenName)?.Value;
//var userLastName = claimsIdentity?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.Surname)?.Value;
//var provider = claimsIdentity?.AuthenticationType;
//var accessToken = await HttpContext.GetTokenAsync(GoogleDefaults.AuthenticationScheme, "access_token");