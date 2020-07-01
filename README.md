## ADP Marketplace Partners

There are a few pre-requesites that you need to fullfill in order to use this library:
- Replace the certifcates in this library with the ones you recieved from the [CSR Tool](https://apps.adp.com/apps/165104)
- Update the client id and client secret with the ones supplied in your credentials document PDF
- Update endpoints from ```https://iat-api.adp.com``` and ```https://iat-accounts.adp.com``` to  ```https://api.adp.com``` and ```https://accounts.adp.com```.

# ADP UserInfo Product Library for c#/.NET

The ADP UserInfo Product Library can be used to obtain basic information about the user that is logging-in to the ADP application. The Library includes a sample application that can be run out-of-the-box to connect to the ADP Marketplace API **test** gateway. This library uses the latest version of the adp-connection library.

Clone the repo from Github: This allows you to access the raw source code of the library as well as provides the ability to run the sample application and view the Library documentation


### Version
1.0.1

### Installation

**Clone from Github**

You can either use the links on Github or the command line git instructions below to clone the repo.

```sh
$ git clone https://github.com/adplabs/adp-userinfo-NET.git adp-userinfo-NET
$ cd adp-connection-NET

open the solution in VisualStudio
    adp-userinfo-NET.sln

run the demo client project ADPClientWebDemo

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
        public RedirectResult Authorize() {
            String authorizationurl = null;
            AuthorizationCodeConnection connection = null;

            // get new connection configuration
            String clientconfig = @"{
                clientID:      ""88a73992-07f2-4714-ab4b-de782acd9c4d"",
                clientSecret:  ""a130adb7-aa51-49ac-9d02-0d4036b63541"",
                sslCertPath:   ""..\\..\\Content\\certs\\cert.pfx"",
                tokenServerUR: ""https://iat-api.adp.com/auth/oauth/v2/token"",
                grantType:     ""client_credentials""
              }";

            // [1] build configuratiuon object
            AuthorizationCodeConfiguration connectionCfg = JSONUtil.Deserialize<AuthorizationCodeConfiguration>(clientconfig);

            // [2] create a new connection based on the connection configuration object provided
            connection = (AuthorizationCodeConnection)ADPApiConnectionFactory.createConnection(connectionCfg);

            // [3] get authorization URL to redirect the user
            authorizationurl = connection.getAuthorizationURL();

            return Redirect(authorizationurl);
        }

        public ActionResult getUserInfo() {
            // [1] get connection from session
            AuthorizationCodeConnection connection = HttpContext.Session["AuthorizationCodeConnection"] as AuthorizationCodeConnection;
            UserInfo user = null;   // user define product DTO class

            connection.connect();

            // connection was successfull
            if (connection.isConnectedIndicator()) {

                // so get the worker like we wanted
                UserInfoHelper helper = new UserInfoHelper(connection);
                user = helper.getUserInfo();
            }

            return View("Index", user);
        }
```

## API Documentation ##

Documentation on the individual API calls provided by the library is automatically generated from the library code.

```
Visual Studio build will generate the XML documentation
```

## Tests ##

Nunit tests are available in Nunit test project found in the solution.


Use Visual Studio code analysis feature to check the code coverage..



## Contributing ##

To contribute to the library, please generate a pull request. Before generating the pull request, please insure the following:

1. Appropriate unit tests have been updated or created.
2. Code coverage on the unit tests must be no less than 95%.
3. Your code updates have been fully tested and linted with no errors.
4. Update README.md and API documentation as appropriate.

## License ##

This library is available under the Apache 2 license (http://www.apache.org/licenses/LICENSE-2.0).

