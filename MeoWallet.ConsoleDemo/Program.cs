using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeoWallet.ConsoleDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new WalletClient();

            try
            {
                var response = client.StartCheckout(6.5, new[]
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
                requiredFields: new RequiredFields
                {
                    Name = false,
                    Email = false,
                    Phone = false,
                    Shipping = false
                })
                .Result;

                Process.Start(response.UrlRedirect);
            }
            catch (AggregateException e)
            {
                throw e.InnerException;
            }
        }
    }
}
