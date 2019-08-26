using ApibookingAdministrator.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
namespace ApibookingAdministrator.Filters
{ 
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]

public class BasicValidateTokenFilter : AuthorizationFilterAttribute
{
    bool Active = true;

    private Repository repository;

    public BasicValidateTokenFilter()
    {
        if (repository == null)
        {
            repository = new Repository();
        }
        
    }

    /// <summary>
    /// Overriden constructor to allow explicit disabling of this
    /// filter's behavior. Pass false to disable (same as no filter
    /// but declarative)
    /// </summary>
    /// <param name="active"></param>
    public BasicValidateTokenFilter(bool active)
    {
        Active = active;
        if (repository == null)
        {
            repository = new Repository();
        }
    }

    /// <summary>
    /// Override to Web API filter method to handle Basic Auth check
    /// </summary>
    /// <param name="actionContext"></param>
    public override void OnAuthorization(HttpActionContext actionContext)
    {
        if (Active)
        {
            var token = ParseAuthorizationHeader(actionContext);
            if (token == null)
            {
                Challenge(actionContext);
                return;
            }


            if (!OnAuthorizeUser(token, actionContext))
            {
                Challenge(actionContext);
                return;
            }

            // inside of ASP.NET this is required
            //if (HttpContext.Current != null)
            //    HttpContext.Current.User = principal;
            base.OnAuthorization(actionContext);
        }
    }

    /// <summary>
    /// Base implementation for user authentication - you probably will
    /// want to override this method for application specific logic.
    /// 
    /// The base implementation merely checks for username and password
    /// present and set the Thread principal.
    /// 
    /// Override this method if you want to customize Authentication
    /// and store user data as needed in a Thread Principle or other
    /// Request specific storage.
    /// </summary>
    /// <param name="username"></param>
    /// <param name="password"></param>
    /// <param name="actionContext"></param>
    /// <returns></returns>
    protected virtual bool OnAuthorizeUser(string token, HttpActionContext actionContext)
    {
        if (!repository.ValidateToken(token))
            return false;

        return true;
    }

    /// <summary>
    /// Parses the Authorization header and creates user credentials
    /// </summary>
    /// <param name="actionContext"></param>
    protected virtual string ParseAuthorizationHeader(HttpActionContext actionContext)
    {
        string authHeader = null;
        var auth = actionContext.Request.Headers.Authorization;
        if (auth != null && auth.Scheme == "Bearer")
            authHeader = auth.Parameter;

        if (string.IsNullOrEmpty(authHeader))
            return null;

        return (authHeader);
    }

    /// <summary>
    /// Send the Authentication Challenge request
    /// </summary>
    /// <param name="message"></param>
    /// <param name="actionContext"></param>
    void Challenge(HttpActionContext actionContext)
    {
        var host = actionContext.Request.RequestUri.DnsSafeHost;
        actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
        actionContext.Response.Headers.Add("WWW-Authenticate", string.Format("Basic realm=\"{0}\"", host));
    }
}
}