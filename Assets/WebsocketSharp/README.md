# websocket-sharp4Unity
This is a Unity-friendly wrapper for the popular C# websocket library [websocket-sharp](https://github.com/sta/websocket-sharp). It provided wrappers for handling websockets that handle the thread-switching for you, so you can call Unity-related functions via the wrapper without the aditional overhead.

websocket-sharp is provided as a `.dll` file. If you want you can replace this with your own build of websocket-sharp.

## Usage
1. Clone, download, or add this project as a submodule to your unity project.
2. Add the `WebsocketFactory` component to your scene. It should automatically add the `WebsocketDispatcher` if not already present.
3. Aquire a `WebsocketWrapper` instance via the factory method `CreateWebSocket(IPAddress address, int port)`.
4. Call `Open()` on the `WebsocketWrapper` instance.
5. If successfull, you should now be able to communicate with the connected websocket server.

## Example
```cs
using System;
using System.Net;
using UnityEngine;
using UnityWebSockets;

public class WebsocketTest : MonoBehaviour
{
    public WebsocketFactory factory;
    public WebsocketWrapper socket;

    public float delay = 1f;
    private float nextPing = 0;

    void Start()
    {
        socket = factory.CreateWebSocket(IPAddress.Parse("127.0.0.1"), 4649);
        socket.OnOpen += OnOpen;
        socket.OnClose += OnClose;
        socket.OnMessage += OnMessage;
        socket.Connect();
    }

    void Update()
    {
        if (Time.time > nextPing && socket.IsOpen)
        {
            nextPing = Time.time + delay;
            socket.Send("Ping!");
        }
    }

    private void OnOpen(object sender, EventArgs e)
    {
        Debug.Log("It's aliiiiive!");
    }

    private void OnClose(object sender, EventArgs e)
    {
        Debug.Log("He's dead jim!");
    }

    private void OnMessage(object sender, string e)
    {
        Debug.Log($"I just received: {e}!");
    }
}
```
