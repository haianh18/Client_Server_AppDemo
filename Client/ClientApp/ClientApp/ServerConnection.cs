using Models.DTO;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

namespace ClientApp
{
    public class ServerConnection
    {
        private static TcpClient client;
        private static NetworkStream stream;

        public void ConnectServer(string server, int port)
        {
            client = new TcpClient(server, port);
            stream = client.GetStream();
        }

        public void DisconnectServer()
        {
            stream.Close();
            client.Close();
        }

        public ResponseDTO? SendRequest(RequestDTO request)
        {
            if (request == null)
            {
                return null;
            }
            var requestJson = JsonSerializer.Serialize(request);
            Byte[] data = Encoding.ASCII.GetBytes(requestJson);
            stream.Write(data, 0, data.Length);

            // Read the length of the response
            Byte[] lengthBytes = new Byte[4];
            stream.Read(lengthBytes, 0, lengthBytes.Length);
            int responseLength = BitConverter.ToInt32(lengthBytes, 0);

            // Prepare to receive the response data
            Byte[] responseBytes = new Byte[responseLength];
            stream.Read(responseBytes, 0, responseLength);
            var responseJson = Encoding.ASCII.GetString(responseBytes, 0, responseLength);
            var responseDTO = JsonSerializer.Deserialize<ResponseDTO>(responseJson);
            return responseDTO;
        }
    }
}
