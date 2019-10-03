// confirmPassword: "Adogo@247"
// password: "Adogo@247"
// token: "CfDJ8JPyQ9HjjHdPgCm60ZaytioOGekMq/xBK1fYuRWLXOSyDTVIuy/N520xuLbuKlg5483klaGlLSF/7AZIV891TdPSc5wGfey7px9w9oFwrOE9Dcy3ob4MKuywMRZnU9UYVT4dnOUtIqFH3AtOQ0lybNN8NxJGnQClLUTlgQm1XFv4edNARinpkDq+7GTjRb6nRNbRccguSSgrdZ4vsB972oQlZ/wN1iaaxLqPgihN1Tg/"
// userEmail: "bald.bearded.ape@gmail.com"

using System.ComponentModel.DataAnnotations;

namespace API.Controllers.Resources.Http.RequestResources.Users
{
    public class ResetPasswordRequestResource
    {
 
        [Required, StringLength(255)]
        public string Password { get; set; }

        [Required, StringLength(500)]
        public string Token { get; set; }

        [Required, EmailAddress]
        public string UserEmail { get; set; }
    }
}