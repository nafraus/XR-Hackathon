using System;
using System.Collections;
using System.Net;
using JetBrains.Annotations;
using NaughtyAttributes;
using UnityEngine;
using UnityWebSockets;
using WebSocketSharp;

public class WebSocketClientServer : MonoBehaviour
{
    public WebsocketFactory factory;
    public WebsocketWrapper socket;

    public float delay = 1f;
    private float nextPing = 0;

    [Button]
    void DoThing()
    {
        socket = factory.CreateWebSocket(IPAddress.Parse("127.0.0.1"), 8080);
        socket.OnOpen += OnOpen;
        socket.OnClose += OnClose;
        socket.OnMessage += OnMessage;
        socket.Connect();
    }

    [Button]
    void SendMessage()
    {
        socket.Send("Test Message");
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