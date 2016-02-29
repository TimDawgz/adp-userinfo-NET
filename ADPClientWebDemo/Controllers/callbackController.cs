using ADPClient;
using System;
using System.Web.Mvc;

namespace UserInfoDemo.Controllers
{
    public class callbackController : Controller
    {
        // GET: callback
        public RedirectResult Index()
        {
            string returncode = null;
            AuthorizationCodeConnection connection = null;
            string error = Request.QueryString["error"];
            ViewBag.IsError = false;

            // checking if there were error/s from the api communication
            if (!String.IsNullOrEmpty(error))
            {
                ViewBag.Message = String.Format("Callback Error: {0}", error);
                ViewBag.IsError = true;
                Console.WriteLine(ViewBag.Message);
            }
            else {
                returncode = Request.QueryString["code"];

                // a successfull communication should result in an authorization code
                if (String.IsNullOrEmpty(returncode))
                {
                    ViewBag.Message = "Callback Error: Unauthorized";
                    ViewBag.IsError = true;
                    Console.WriteLine(ViewBag.Message);
                }
                else {
                    // callback was successfull so get connection from session
                    connection = HttpContext.Session["AuthorizationCodeConnection"] as AuthorizationCodeConnection;

                    if (connection == null)
                    {
                        ViewBag.Message = "Error: Session expired. Re-Authorization required.";
                        ViewBag.IsError = true;
                        Console.WriteLine(ViewBag.Message);
                    }
                    else {
                        // update connection's authorization code
                        ((AuthorizationCodeConfiguration)connection.connectionConfiguration).authorizationCode = returncode;
                    }
                }

            }

            // cache connection in Session
            HttpContext.Session["AuthorizationCodeConnection"] = connection;

            return Redirect("marketplace");
        }
    }
}