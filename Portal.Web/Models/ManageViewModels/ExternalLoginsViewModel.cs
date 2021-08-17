using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Portal.Web.Models.ManageViewModels
{
    public class ExternalLoginsViewModel
    {
        public List<UserLoginInfo> CurrentLogins { get; set; }

        public List<AuthenticationScheme> OtherLogins { get; set; }

        public bool ShowRemoveButton { get; set; }

        public string StatusMessage { get; set; }
    }
}
