using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;

namespace DataAccesslayer.Services
{
    //Inherit from Attribute & IAuthorizationFilter
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        //Array to store roles. integars, because it stores primary keys of the roles.
        private readonly int[] _roles;

        public AuthorizeAttribute(params int[] role)
        {
            _roles = role;
        }

        //Method triggers when you put the [Authorize] line in the controller
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //Check the role of the person who logged in, null if nobody logged in
            var userRole = context.HttpContext.Session.GetInt32("RoleID");

            //Checks if the user logged in or if the role is authorized
            if (!userRole.HasValue || !_roles.Contains(userRole.Value))
            {
                //If the user is NOT authorized, it will redirect them to "AccessDenied" Action method in the "Account" controller
                context.Result = new RedirectToActionResult("AccessDenied", "Account", null);
            }
        }
    }
}
