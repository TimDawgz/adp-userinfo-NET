# ADP UserInfo Product Library for c#/.NET (Beta)

The ADP UserInfo Product Library can be used to obtain basic information about the user that is logging-in to the ADP application. The Library includes a sample application that can be run out-of-the-box to connect to the ADP Marketplace API **test** gateway. This library uses the latest version of the adp-connection library.

There are two ways of installing and using this library:

  - Clone the repo from Github: This allows you to access the raw source code of the library as well as provides the ability to run the sample application and view the Library documentation
  - Search & Add the library as a NuGet library package to your Visual Studio solution. This is the recommended method when you are ready to develop amazing Apps for the ADP Marketplace store.

### Version
1.0.0

### Installation

**Clone from Github**

You can either use the links on Github or the command line git instructions below to clone the repo.

```sh
$ git clone https://github.com/adplabs/adp-userinfo-NET.git adp-userinfo-NET
```

followed by

```sh
$ cd adp-connection-NET
$ nuget restore adp-userinfo-NET.sln
$ devenv.exe adp-userinfo-NET /ReBuild
$ devenv.exe /Run
```

The build instruction should install the dependent packages from NuGet else get the packages from NuGet in the packages folder. If you run into errors you may need to open and run the solution in Visual Studio.

##### Alternative: 
*Running the sample app*

Load the solution in Visual Studio and Hit [Ctrl F5] (for Start without Debugging)

You can run the sample app included using the Visual Studio environment or deploy it to your favourite ASP.NET web server and enjoy the ease of developing using the ADP Library.

This starts an HTTP server on port 8889 (this port must be unused to run the sample application). You can point your browser to http://localhost:8889. The sample app allows you to connect to the ADP test API Gateway using the **client_credentials** and **authorization_code** grant types. For the **authorization_code** connection, you will be asked to provide an ADP username (MKPLDEMO) and password (marketplace1). The test using the Authorization Code link will prompt a login and upon a successful login present the basic information about the user logged-in.

***

***

## Examples

### Retrieve UserInfo using Authorization Code grant type

```c#
using System;
using System.Web.Mvc;
using ADPClient;
using ADPClient.Product;
using ADPClient.Product.dto;

namespace UserInfoDemo {
    public class marketplaceController : Controller {
        // GET: marketplace
        public ActionResult Index() {
            return View();
        }
        public ActionResult Authorize() {
            String authorizationurl = null;
            AuthorizationCodeConnection connection = null;

            // get new connection configuration
            // JSON config object placed in Web.config configuration or
            // set individual config object attributes
            String clientconfig = UserInfoDemo.Properties.Settings.Default.AuthorizationCodeConfiguration; 
            if (String.IsNullOrEmpty(clientconfig)) {
                ViewBag.IsError = true;
                ViewBag.Message = "Settings file or default options not available.";
                Console.WriteLine(ViewBag.Message);
            } else {
                // Initialize the Connection Configuration Object.
                // specifying the ConnectionConfiguration type will get back the right ConnectionConfiguration type object.
                // AuthorizationCodeConfiguration object is returned
                // JavaScriptSerializer oJS = new JavaScriptSerializer();
                AuthorizationCodeConfiguration connectionCfg = JSONUtil.Deserialize<AuthorizationCodeConfiguration>(clientconfig);

                // create a new connection based on the connection configuration object provided
                connection = (AuthorizationCodeConnection)ADPApiConnectionFactory.createConnection(connectionCfg);
                try {
                    // Authorization Code Apps require a user to login to ADP
                    // So obtain the authorization URL to redirect the user's
                    // browser so they can login
                    authorizationurl = connection.getAuthorizationURL();
                    Console.WriteLine("Got auth URL... {0}... redirecting", authorizationurl);
                    // save connection for later use
                    HttpContext.Session["AuthorizationCodeConnection"] = connection;
                } catch (Exception e) {
                    ViewBag.isError = true;
                    ViewBag.Message = e.Message;
                    Console.WriteLine(ViewBag.Message);
                    return View("Index");
                }
            }
            return Redirect(authorizationurl);
        }

        public ActionResult getUserInfo() {
            UserInfo userinfo = null;
            // get connection from session
            AuthorizationCodeConnection connection = HttpContext.Session["AuthorizationCodeConnection"] as AuthorizationCodeConnection;

            if (connection == null || ((AuthorizationCodeConfiguration)connection.connectionConfiguration).authorizationCode == null) {
                //is the connection available in session or is the 
                // cached connection expired then lets re-authorize
                return Authorize();
            }
            try {
                connection.connect();
                // connection was successfull 
                if (connection.isConnectedIndicator()) {
                    // so get the worker like we wanted
                    UserInfoHelper helper = new UserInfoHelper(connection, null);
                    userinfo = helper.getUserInfo();
                }
            } catch (Exception e) {
                ViewBag.isError = true;
                ViewBag.Message = e.Message;
                Console.WriteLine(ViewBag.Message);
            }
            return View("Index", userinfo);
        }

        public ActionResult Logout() {
            ViewBag.Message = "You're logged out.";
            HttpContext.Session["AuthorizationCodeConnection"] = null;
            return View("Index");
        }
    }
}
```

## API Documentation ##

Documentation on the individual API calls provided by the library is automatically generated from the library code.

 
## Contributing ##

To contribute to the library, please generate a pull request. Before generating the pull request, please insure the following:

1. Appropriate unit tests have been updated or created.
2. Code coverage on the unit tests must be no less than 95%.
3. Your code updates have been fully tested and linted with no errors.
4. Update README.md and API documentation as appropriate.
 
## License ##

This library is available under the Apache 2 license (http://www.apache.org/licenses/LICENSE-2.0).

