using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace MakersBnB.ActionFilters
{
  // a class which implements the IActionFilter interface
  public class AuthenticationFilter : IActionFilter {

    // any class implementing IActionFilter must implement this method
    public void OnActionExecuting(ActionExecutingContext context) {

      // check if there is a user_id in current user's session
      int? user_id = context.HttpContext.Session.GetInt32("user_id");
      if(user_id == null) {
        // if there is no user id, redirect to sign in
        context.Result = new RedirectResult("/Sessions/New");
        return;
      }
      else
      {
        // otherwise, continue
        return;
      }
    }

    // any class implementing IActionFilter must implement this method
    public void OnActionExecuted(ActionExecutedContext context) {

    }
  }
}
