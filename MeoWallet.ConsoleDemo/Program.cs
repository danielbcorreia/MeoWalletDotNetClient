using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Newtonsoft.Json;
using Owin;
using Microsoft.Owin.Hosting;

namespace MeoWallet.ConsoleDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            // setup the Web API that will listen for the user return request
            const string baseAddress = "http://localhost:9001/";

            using (WebApp.Start<Startup>(baseAddress))
            {
                // the actual client checkout call
                var client = new WalletClient();

                try
                {
                    var response = client.StartCheckout(
                        // total amount
                        6.5,
                        // the items that the customer will pay for
                        new[]
                        {
                            new PaymentItem
                            {
                                Name = "Some-item",
                                Description = "Some item",
                                Quantity = 2,
                                Amount = 2.5,
                                Reference = 123
                            },

                            new PaymentItem
                            {
                                Name = "Another-item",
                                Description = "Another item",
                                Quantity = 1,
                                Amount = 4.0,
                                Reference = 124
                            }
                        },
                        // the fields that the user will have to fill to complete the payment
                        requiredFields: new RequiredFields
                        {
                            Name = false,
                            Email = false,
                            Phone = false,
                            Shipping = false
                        },
                        // these can be defined in the App.config. see the README.md for more details.
                        confirmCallback: baseAddress + "api/callback?type=confirm",
                        cancelCallback: baseAddress + "api/callback?type=cancel"
                    ).Result;

                    Process.Start(response.UrlRedirect);
                }
                catch (AggregateException e)
                {
                    throw e.InnerException;
                }

                Console.WriteLine("Waiting for requests to the API. Press enter to exit.");
                Console.ReadLine();
            }
        }
    }

    public class CallbackController : ApiController
    {
        public async Task<HttpResponseMessage> Get(string type, Guid checkoutid)
        {
            if (type == "confirm")
            {
                // double check to ensure that this request was not forged
                var client = new WalletClient();
                var checkout = await client.GetCheckout(checkoutid);

                if (checkout.Payment.Status == TransactionStatus.Completed)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "The transaction was completed successfully!");
                }
                
                return Request.CreateResponse(HttpStatusCode.OK, "The transaction was not completed. Current state is: " + JsonConvert.SerializeObject(checkout.Payment.Status));
            }

            return Request.CreateResponse(HttpStatusCode.OK, string.Format("Type={0}, CheckoutId={1}", type, checkoutid));
        }
    }

    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var config = new HttpConfiguration();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            appBuilder.UseWebApi(config);
        }
    } 
}
