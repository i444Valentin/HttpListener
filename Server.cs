using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace HttpListenerProject
{  
    class Server : ConsoleHandler
    {
        [Obsolete]
        static async Task Main(string[] args)
        {       
            //проверяем наличие прав админа у пользователя
            if (!UserControls.IsUserAdministrator())
            {
                Console.WriteLine("Недостаточно полномочий для запуска программы");
                return;
            }
            InitializeEventHandlers();
            HttpListener listener = new HttpListener();
            string ip = DNSUtility.getIPMachine().ToString(); //получаем адрес машины
            string httpAddress = "http://" + ip + "/";
            listener.Prefixes.Add(httpAddress); //локальный ip этого ПК (можно узнать с помощью команды ipconfig)
            Console.WriteLine("Сервер будет запущен на адресе: " + httpAddress);
            listener.Start();
            Console.WriteLine("Сервер запущен");
            while (true)
            {
                // метод GetContext блокирует текущий поток, ожидая получение запроса 
                HttpListenerContext context = await listener.GetContextAsync();
                HttpListenerRequest request = context.Request;

                Console.WriteLine("Обращение к серверу по адресу: " + request.RemoteEndPoint.Address.ToString() + " , порт: " + request.RemoteEndPoint.Port.ToString());
                // получаем объект ответа
                HttpListenerResponse response = context.Response;

                // создаем ответ в виде кода html
                string responseStr = HTMLLoader.LoadHTML();
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseStr);
      
                // получаем поток ответа и пишем в него ответ
                response.ContentLength64 = buffer.Length;
                Stream output = response.OutputStream;

                output.Write(buffer, 0, buffer.Length);

                //закрываем поток
                output.Close();
            }
        }
    }
}