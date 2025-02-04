using Base.Models;
using Base.Models.DTO;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;


namespace Server_ConsoleApp
{
    class Program
    {
        private static readonly ProductDbContext context = new ProductDbContext();
        static int numberOfClient = 0;
        static void Main(string[] args)
        {
            string host = "127.0.0.1";
            int port = 1500;
            Console.WriteLine("Server App");
            IPAddress localAddr = IPAddress.Parse(host);
            TcpListener server = new TcpListener(localAddr, port);
            server.Start();

            Console.WriteLine("************************");
            Console.WriteLine("waiting....");

            while (true)
            {
                TcpClient client = server.AcceptTcpClient();
                Console.WriteLine("*************************");
                Console.WriteLine($"Number of client connected: {++numberOfClient}");
                Thread thread = new Thread(new ParameterizedThreadStart(ProcessClient!));
                thread.Start(client);

            }
        }

        static void ProcessClient(object parmeter)
        {
            TcpClient client = (TcpClient)parmeter;
            string data;
            int count;
            NetworkStream stream = client.GetStream();
            Byte[] bytes = new Byte[1024];
            try
            {
                while ((count = stream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    data = System.Text.Encoding.ASCII.GetString(bytes, 0, count);
                    var request = JsonSerializer.Deserialize<RequestDTO>(data);

                    if (request == null)
                    {
                        Console.WriteLine("Request is null");
                        return;
                    }

                    var response = ProcessRequest(request);
                    Console.WriteLine(response.Message);

                    var responseJson = JsonSerializer.Serialize(response);
                    Byte[] responseBytes = System.Text.Encoding.ASCII.GetBytes(responseJson);

                    // Send the length of the response first
                    int responseLength = responseBytes.Length;
                    Byte[] lengthBytes = BitConverter.GetBytes(responseLength);
                    stream.Write(lengthBytes, 0, lengthBytes.Length);

                    // Send the actual response data
                    stream.Write(responseBytes, 0, responseBytes.Length);
                }
            }
            catch (IOException e)
            {

                Console.WriteLine(e.Message);
            }

            client.Close();
            Console.WriteLine($"Number of client connected: {--numberOfClient}");
        }



        static ResponseDTO ProcessRequest(RequestDTO request)
        {
            return request.ActionType switch
            {
                "AddProduct" => AddProduct(JsonSerializer.Deserialize<Product>(request.Data.ToString())),
                "EditProduct" => EditProduct(JsonSerializer.Deserialize<Product>(request.Data.ToString())),
                "DeleteProduct" => DeleteProduct(JsonSerializer.Deserialize<Product>(request.Data.ToString())),
                "GetProducts" => GetProducts(),
                "GetProductByProductId" => GetProductByProductId(JsonSerializer.Deserialize<Int32>(request.Data.ToString())),
                "GetProductsByCategoryId" => GetProductsByCategoryId(JsonSerializer.Deserialize<Int32>(request.Data.ToString())),
                "GetCategories" => GetCategories(),
                _ => new ResponseDTO { Status = "Error", Message = "Invalid Action Type", Data = null },
            };
        }



        static ResponseDTO AddProduct(Product product)
        {
            context.Products.Add(product);
            context.SaveChanges();
            Console.WriteLine("***********************");
            var pro = context.Products.Find(product.Id);
            if (pro == null)
            {
                Console.WriteLine("Product Added Fail");
                return new ResponseDTO { Status = "Error", Message = "Product Added Fail", Data = null };
            }
            return new ResponseDTO { Status = "Success", Message = "Product Added", Data = null };
        }

        static ResponseDTO EditProduct(Product product)
        {
            var pro = context.Products.Find(product.Id);
            if (pro == null)
            {
                Console.WriteLine("***********************");
                Console.WriteLine("Product Not Found");
                return new ResponseDTO { Status = "Error", Message = "Product Not Found", Data = null };
            }
            pro.Name = product.Name;
            pro.Price = product.Price;
            pro.CategoryId = product.CategoryId;
            context.SaveChanges();
            Console.WriteLine("***********************");
            return new ResponseDTO { Status = "Success", Message = "Product Updated", Data = null };
        }

        static ResponseDTO DeleteProduct(Product product)
        {
            var pro = context.Products.Find(product.Id);
            if (pro == null)
            {
                Console.WriteLine("***********************");
                Console.WriteLine("Product Not Found");
                return new ResponseDTO { Status = "Error", Message = "Product Not Found" };
            }
            context.Products.Remove(pro);
            context.SaveChanges();
            Console.WriteLine("***********************");
            return new ResponseDTO { Status = "Success", Message = "Product Deleted", Data = null };
        }

        static ResponseDTO GetProducts()
        {
            Console.WriteLine("**********************");
            Console.WriteLine("Getting product list...");
            var proList = context.Products.ToList();
            if (proList.Count == 0)
            {
                Console.WriteLine("Product List Empty");
                return new ResponseDTO { Status = "Error", Message = "Product List Empty", Data = null };
            }
            return new ResponseDTO { Status = "Success", Message = "Product List Found", Data = proList };
        }

        static ResponseDTO GetProductByProductId(int id)
        {
            Console.WriteLine("**********************");
            Console.WriteLine("Getting product...");
            var pro = context.Products.Find(id);
            if (pro == null)
            {
                Console.WriteLine("Product Not Found");
                return new ResponseDTO { Status = "Error", Message = "Product Not Found", Data = null };
            }
            return new ResponseDTO { Status = "Success", Message = "Product Found", Data = pro };
        }

        static ResponseDTO GetProductsByCategoryId(int id)
        {
            Console.WriteLine("**********************");
            Console.WriteLine("Getting product list...");
            var proList = context.Products.Where(p => p.CategoryId == id).ToList();
            if (proList.Count == 0)
            {
                Console.WriteLine("Product List Empty");
                return new ResponseDTO { Status = "Error", Message = "Product List Empty", Data = null };
            }
            return new ResponseDTO { Status = "Success", Message = "Product List Found", Data = proList };
        }

        static ResponseDTO GetCategories()
        {
            Console.WriteLine("**********************");
            Console.WriteLine("Getting category list...");
            var cateList = context.Categories.ToList();
            if (cateList.Count == 0)
            {
                Console.WriteLine("Category List Empty");
                return new ResponseDTO { Status = "Error", Message = "Category List Empty", Data = null };
            }
            return new ResponseDTO { Status = "Success", Message = "Category List Found", Data = cateList };
        }
    }
}
