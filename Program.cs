using System.Text;
using static System.Console;
using System.Net;
using System.Net.Sockets;

namespace TcpIpServer01
{
    class Program
    {

        static void Main(string[] args)
        {
            Server.StartServer(5678);
            Server.Listener();
         
        }
    }
    class Server
    {
        private static TcpListener ServerListener { get; set; }
        private static bool Acept { get; set; }

        public static void StartServer(int porta)
        {
            ServerListener = new TcpListener(IPAddress.Parse("192.168.1.105"),porta);
            ServerListener.Start();
            Acept = true;
            WriteLine($"O servidor está disponivel na porta {porta}");
        }
        public static void Listener()
        {
            if (ServerListener != null && Acept)
            {
                while (true)
                {
                    WriteLine("Aguardando um cliente...");
                    var clienteTask = ServerListener.AcceptTcpClientAsync();

                    if (clienteTask != null)
                    {
                        WriteLine("Receptor Conectado. Agardando informacoes...");
                        var client = clienteTask.Result;
                        string menssagem = "";
                        while (menssagem != null && !menssagem.Contains("quit"))
                        {
                            byte[] data = Encoding.ASCII.GetBytes("Envie uma nova menssage [digite 'quit' para encerrar]");
                            client.GetStream().Write(data,0,data.Length);

                            byte[] buffer = new byte[1024];
                            client.GetStream().Read(buffer,0,buffer.Length);
                            menssagem = Encoding.ASCII.GetString(buffer);
                            WriteLine(menssagem);                            
                        }
                        WriteLine("Encerrando Conexao...");
                        client.Dispose();
                    }
                }
            }
        }


    }
}
