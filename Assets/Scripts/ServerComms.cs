using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;



public class Message
{
    public string message;
    public bool success;
}
public class ServerComms : MonoBehaviour
{
    private HttpListener httpListener;

    void Start()
    {
        StartHttpServer();
    }

    async void StartHttpServer()
    {
        httpListener = new HttpListener();
        // Add prefixes (endpoints) that you want to listen for
        httpListener.Prefixes.Add("http://localhost:8080/");
        httpListener.Start();

        Debug.Log("HTTP Server started on http://localhost:8080/");

        while (httpListener.IsListening)
        {
            HttpListenerContext context = await httpListener.GetContextAsync();
            HttpListenerRequest request = context.Request;
            HttpListenerResponse response = context.Response;

            // Get the path the client requested
            string path = request.Url.AbsolutePath;

            // Check which route the request is for
            if (path == "/test")
            {
                HandleTestRoute(response, request);
            }
            if (path == "/startupcheck")
            {
                HandleStartupCheckRoute(response);
            }
            if (path == "/yes")
            {

            }
            if (path == "/no")
            {

            }
            else
            {
                HandleNotFound(response);
            }
        }
    }

    // Handle the "/test" route
    void HandleTestRoute(HttpListenerResponse response, HttpListenerRequest request)
    {
        string responseString = "<html><body>This is the /test route!</body></html>";
        byte[] buffer = Encoding.UTF8.GetBytes(responseString);

        response.ContentLength64 = buffer.Length;
        JsonGetter(request);
        using (System.IO.Stream output = response.OutputStream)
        {
            output.Write(buffer, 0, buffer.Length);
            
        }
    }

    // Handle the "/startupcheck" route
    void HandleStartupCheckRoute(HttpListenerResponse response)
    {
        string responseString = "<html><body>Startup check completed successfully!</body></html>";
        byte[] buffer = Encoding.UTF8.GetBytes(responseString);

        response.ContentLength64 = buffer.Length;
        using (System.IO.Stream output = response.OutputStream)
        {
            output.Write(buffer, 0, buffer.Length);
        }
    }

    // Handle not found route
    void HandleNotFound(HttpListenerResponse response)
    {
        string responseString = "<html><body>404 - Not Found</body></html>";
        byte[] buffer = Encoding.UTF8.GetBytes(responseString);

        response.StatusCode = (int)HttpStatusCode.NotFound;
        response.ContentLength64 = buffer.Length;
        using (System.IO.Stream output = response.OutputStream)
        {
            output.Write(buffer, 0, buffer.Length);
        }
    }

    void OnApplicationQuit()
    {
        httpListener?.Stop();
    }

    public void JsonGetter(HttpListenerRequest request)
    {
        string jsonData;
        using (var reader = new StreamReader(request.InputStream, request.ContentEncoding))
        {
            jsonData = reader.ReadToEnd();
            Message message = JsonUtility.FromJson<Message>(jsonData);
            print(message.message);
            
        }

    }
}
