using Newtonsoft.Json;
using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;
using System.Web;

namespace JukkaServerLib
{
    struct Response
    {
        static Response() { }
        public Response(byte[] body, int statusCode, string statusDescription)
        {
            Body = body;
            StatusCode = statusCode;
            StatusDescription = statusDescription;
        }
        public byte[] Body { get; set; }
        public int StatusCode { get; set; }
        public string StatusDescription { get; set; }
    }

    class Program
    {
        static public void RunServer()
        {
            var prefix = "http://*:4333/";
            HttpListener listener = new HttpListener();
            listener.Prefixes.Add(prefix);
            try
            {
                listener.Start();
            }
            catch (HttpListenerException hlex)
            {
                Console.WriteLine($"HttpListener exception: {hlex.Message}");
                return;
            }
            while (listener.IsListening)
            {
                var context = listener.GetContext();
                ProcessRequest(context);
            }
            listener.Close();
        }

        static private void ProcessRequest(HttpListenerContext context)
        {
            // Get the data from the HTTP stream
            Console.WriteLine("===============================");

            var body = new StreamReader(context.Request.InputStream).ReadToEnd();
            Console.WriteLine($"HttpMethod : {context.Request.HttpMethod} \n body : {body}\n");

            // Response back to client
            var output = context.Response.OutputStream;

            byte[] ResponseBody = Encoding.UTF8.GetBytes("ACK");
            Response APIResponse;
            APIResponse = new Response(new byte[0], 200, "Success");

            context.Response.StatusCode = 200;
            context.Response.KeepAlive = false;
            context.Response.ContentLength64 = ResponseBody.Length;

            switch (context.Request.HttpMethod)
            {
                case "GET":
                    Console.WriteLine("GET");
                    switch (context.Request.Url.AbsolutePath)
                    {
                        // Endpoint URLs
                        case "/get":
                            Console.WriteLine("/get");
                            break;
                        case "/prod/api/account/login":
                            Console.WriteLine("/prod/api/account/login");
                            break;
                    }
                    break;
                case "POST":
                    Console.WriteLine($"Processing POST request: {context.Request.Url.AbsolutePath}");
                    switch (context.Request.Url.AbsolutePath)
                    {
                        // Login
                        case "/prod/api/account/login":
                            NameValueCollection LoginCollection = MakeLoginCollectionFromQueryString(body);

                            APIResponse = MakeLogin(LoginCollection);
                            context.Response.StatusCode = APIResponse.StatusCode;
                            context.Response.StatusDescription = APIResponse.StatusDescription;
                            context.Response.ContentLength64 = APIResponse.Body.Length;
                            break;

                        // Order Status
                        case "/api/order/status":
                            // Request body contain OrderStatus as Json
                            var OrderStatus = JsonConvert.DeserializeObject<OrderStatus>(body);

                            ResponseBody = MakeOrderStatus(OrderStatus);
                            context.Response.ContentLength64 = ResponseBody.Length;
                            break;
                        // Order Item Status
                        case "/api/orderitem/status":
                            // Request body contain OrderItemStatus as Json
                            var OrderItemStatus = JsonConvert.DeserializeObject<OrderItemStatus>(body);

                            ResponseBody = MakeOrderItemStatus(OrderItemStatus);
                            context.Response.ContentLength64 = ResponseBody.Length;
                            break;

                        // Payment
                        case "/api/payment":
                            var Payment = JsonConvert.DeserializeObject<Payment>(body);

                            ResponseBody = MakePayment(Payment);
                            context.Response.ContentLength64 = ResponseBody.Length;
                            break;

                        // Machine Status
                        case "/api/machine/status":
                            // Request body contain Machine as Json
                            MachineStatus status = JsonConvert.DeserializeObject<MachineStatus>(body);

                            ResponseBody = MakeMachineStatus(status);
                            context.Response.ContentLength64 = ResponseBody.Length;
                            break;

                            //default: // Dale: Need to complete this
                    }
                    Console.WriteLine();
                    break;
            }

            // Send Respone to Client Jukka machine
            output.Write(APIResponse.Body, 0, APIResponse.Body.Length);
            context.Response.Close();
        }

        private static byte[] MakeMachineStatus(MachineStatus status)
        {
            Console.WriteLine($"Machine Status:");
            Console.WriteLine(status);
            // ToDo: update something.

            return new byte[0];
        }

        private static byte[] MakePayment(Payment payment)
        {
            Console.WriteLine($"\nPayment:");
            Console.WriteLine(payment);

            // Process payment and store in database
            return new byte[0];
        }

        private static byte[] MakeOrderItemStatus(OrderItemStatus status)
        {
            Console.WriteLine($"\nOrder Item Status:");
            Console.WriteLine(status);
            // TODO: update something such as the database with the order status.

            return new byte[0];
        }

        private static byte[] MakeOrderStatus(OrderStatus status)
        {
            Console.WriteLine($"\nOrder Status:");
            Console.WriteLine(status);
            // TODO: update something such as the database with the order status.

            return new byte[0];
        }

        // Transforms the x-www-form-urlencoded body to the NameValueCollection
        private static NameValueCollection MakeLoginCollectionFromQueryString(string body)
        {
            NameValueCollection LoginCollection = HttpUtility.ParseQueryString(body);
            return LoginCollection;
        }

        // loginCollection is a collection of the payload or Request x-www-form-urlencoded parameters.
        // In otherwords, the username, password and grant_type
        private static Response MakeLogin(NameValueCollection loginCollection)
        {
            Console.WriteLine("Login Payload collection:");
            foreach (string s in loginCollection.AllKeys)
            {
                Console.WriteLine($"{s} = {loginCollection[s]}");
            }

            // Todo: Process login here.

            // Create the Login object
            Response resp = new Response();
            LoginExtensionManager mgr = new LoginExtensionManager();
            Login login = new Login(loginCollection, mgr);

            // The AuthenticateUser method of Login does the actual athentication
            if (login.AuthenticateUser())
            {
                Console.WriteLine($"\nLogin Payload body:");
                Console.WriteLine(loginCollection.ToString());
                Console.WriteLine($"\nLogin Response:");
                Console.WriteLine($"{login}");

                // Return the serialzed Json as a Byte array for the Response.
                resp = new Response(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(login,
                   Formatting.Indented)), 200, "Success");
                return resp;
            }
            else
            {
                Console.WriteLine("Login Failed!!");
                return new Response(new byte[0], 400, "Bad Request");
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("===== Jukka HTTP RESTful Listener =====");

            RunServer();
        }


    }
}
