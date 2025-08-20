using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace PharmacyManagement.Models
{
    public class PharmayStaffOnlyAttribute: Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new ChallengeResult();
                return;
            }

            var isAdmin = context.HttpContext.User.IsInRole("admin");
            var isUser = context.HttpContext.User.IsInRole("user"); 

            if (!isAdmin && !isUser)
            {
                context.Result = new ForbidResult();
            }
        }
    }

}
