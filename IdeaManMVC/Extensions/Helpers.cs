using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;

namespace IdeaManMVC.Extensions
{
    public static class Helpers
    {
        public static string GetFullName(this System.Security.Principal.IPrincipal usr)
        {
            var fullNameClaim = ((ClaimsIdentity)usr.Identity).FindFirst("iDea:FullName");
            if (fullNameClaim != null)
                return fullNameClaim.Value;

            return "";
        }

    }
}