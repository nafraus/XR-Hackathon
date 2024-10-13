using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using NaughtyAttributes;

public class TcpServerClient : MonoBehaviour
{
    private bool isServer;
    private string ipAddress = "192.168.8.112";
    private int port = 54448;
    private TcpClient client;
    private NetworkStream stream;
    private StringBuilder receivedData = new StringBuilder();
    [SerializeField] private NetworkMessageManager _networkMessageManager;

    void Update()
    {
        if (client != null && client.Connected)
        {
            ReceiveData();
        }
    }

    [Button]
    void StartClient()
    {
        Connect(ipAddress, "Testing Message to connect to server.");
    }

    void Connect(string server, string message)
    {
        try
        {
            client = new TcpClient(server, port);
            byte[] data = Encoding.ASCII.GetBytes(message);
            stream = client.GetStream();

            Debug.Log(client.Connected);
            GameManager.S.StartGame();

            // Send the message to the connected TcpServer.
            stream.Write(data, 0, data.Length);
            Debug.Log("Sent: {0}" + message);
        }
        catch (SocketException e)
        {
            Debug.LogError("SocketException: {0}" + e);
        }
    }

    void ReceiveData()
    {
        byte[] buffer = new byte[256];
        try
        {
            if (stream.DataAvailable)
            {
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                if (bytesRead > 0)
                {
                    // Accumulate received data
                    string incomingData = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                    receivedData.Append(incomingData);

                    // Check for message termination character
                    if (receivedData.ToString().Contains(";"))
                    {
                        // Split by ';' and process each message
                        string[] messages = receivedData.ToString().Split(';');
                        for (int i = 0; i < messages.Length - 1; i++) // Last one may be empty
                        {
                            ProcessMessage(messages[i]);
                        }

                        // Keep the last incomplete message (if any)
                        receivedData.Clear();
                        if (messages.Length > 0 && !string.IsNullOrEmpty(messages[messages.Length - 1]))
                        {
                            receivedData.Append(messages[messages.Length - 1]);
                        }
                    }
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Exception in ReceiveData: " + e.Message);
        }
    }

    void ProcessMessage(string message)
    {
        // Handle each complete message
        Debug.Log("Received complete message: " + message);
        // You can call DecodeInt or any other processing method here based on your message format
        _networkMessageManager.ReceiveMessage(message);
    }

    void DecodeInt(int code)
    {
        // Process the decoded integer
        switch (code)
        {
            case 0:
                Debug.Log("Received code: 0");
                break;
            case 1:
                Debug.Log("Received code: 1");
                break;
        }
    }

    [Button]
    public void InspectorSendMessage()
    {
        SendMessage("Your message here;");
    }

    public void SendMessage(string message)
    {
        byte[] data = Encoding.ASCII.GetBytes(message);
        stream.Write(data, 0, data.Length);
    }

    private void OnApplicationQuit()
    {
        Debug.Log("Stopping client on application quit");
        client.Close();
    }
}
