using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Collections;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using System.Runtime.CompilerServices;



public class Message
{
    public string message;
    public bool success;
}

public class JsonData
{
    public bool fromUnity = true;
    public string order;
}
public class ServerComms : MonoBehaviour
{
    private HttpListener httpListener;
    private HttpRequestMessage httpRequest;

    public string isAsking;

    public string currentName;
    public string currentTrueName;

    void Start()
    {
        StartHttpServer();
    }

    async void StartHttpServer()
    {
        httpListener = new HttpListener();
        httpRequest = new HttpRequestMessage();

        httpRequest.Method = new HttpMethod("POST");
        httpRequest.Content = new StringContent("test");
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
                JsonGetter(request);
            }
            if (path == "/no")
            {
                JsonGetter(request);
            }
            if (path == "/input")
            {
                whatwasasked(isAsking, DataReturnerString(request));
            }
            else
            {
                HandleNotFound(response);
            }


        }
    }


    public void whatwasasked(string str, string param)
    {
        if (str == "name")
        {
            currentName = param;
            isAsking = null;
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


    public string DataReturnerString(HttpListenerRequest request)
    {
        string jsonData;
        using (var reader = new StreamReader(request.InputStream, request.ContentEncoding))
        {
            jsonData = reader.ReadToEnd();
            Message message = JsonUtility.FromJson<Message>(jsonData);
            return(message.message);
        }
    }


    public void requestmaker(string order)
    {
        string url = "http://localhost:3000/UnityOrder";
        StartCoroutine(SendPostRequest(url, order));
    }

    IEnumerator SendPostRequest(string url, string order)
    {
        // Create JSON data for the request body (customize this data as needed)
        JsonData jsondata = new JsonData();
        jsondata.order = order;

        // Convert JSON data to bytes
        byte[] postData = Encoding.UTF8.GetBytes(jsondata.order);
        postData.AddRange(Encoding.UTF8.GetBytes(jsondata.fromUnity.ToString()));
        print(postData);
        // Create a new UnityWebRequest, set method to POST
        UnityWebRequest request = new UnityWebRequest(url, "POST");

        // Set the upload handler with the JSON data
        request.uploadHandler = new UploadHandlerRaw(postData);
        request.downloadHandler = new DownloadHandlerBuffer();

        // Set the request content-type to JSON
        request.SetRequestHeader("Content-Type", "application/json");

        // Send the request and wait for the response
        yield return request.SendWebRequest();

        // Check for errors
        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error: " + request.error);
            Debug.LogError(request.url);
            Debug.LogError(request.responseCode);
        }
        else
        {
            // Handle the response
            Debug.Log("Response: " + request.downloadHandler.text);
        }
    }

    /*    IEnumerator FetchJsonData()
        {
            // Send a GET request to the URL
            UnityWebRequest request = UnityWebRequest.Post(url);
            yield return request.SendWebRequest();

        }
    */
}