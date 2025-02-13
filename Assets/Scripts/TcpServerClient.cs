using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using UnityEngine;
using NaughtyAttributes;
using TMPro;

public class TcpServerClient : MonoBehaviour
{
    private bool isServer;
    private string ipAddress = "192.168.8.112";
    private int port = 54448;
    private TcpClient client;
    private NetworkStream stream;
    private StringBuilder receivedData = new StringBuilder();
    [SerializeField] private NetworkMessageManager _networkMessageManager;

    [SerializeField] private TMP_InputField ipField;
    [SerializeField] private TMP_InputField portField;
    [SerializeField] private GameObject ipCanvas;
    [SerializeField] private GameObject connectedErrorMessage;
    
    //NOTE: Some UI logic is done through events, and is not implemented on this script.

    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.P)) StartClient();
        
        if (client != null && client.Connected)
        {
            ReceiveData();
        }
    }

    [Button]
    public void StartClient()
    {
        Connect(ipField.text,  Int32.Parse(portField.text),"Testing Message to connect to server.");
    }

    void Connect(string server, int port, string message)
    {
        try
        {
            client = new TcpClient(server,  port);
            byte[] data = Encoding.ASCII.GetBytes(message);
            stream = client.GetStream();

            Debug.Log(client.Connected);
            if(client.Connected) GameManager.S.StartGame();
            else
            {
                connectedErrorMessage.SetActive(true);
                return;
            }

            // Send the message to the connected TcpServer.
            //stream.Write(data, 0, data.Length);
            Debug.Log("Sent: {0}" + message);
            ipCanvas.SetActive(false);
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
                        
                        //Keeps last incomplete message. Do not want
                        /*
                        if (messages.Length > 0 && !string.IsNullOrEmpty(messages[messages.Length - 1]))
                        {
                            receivedData.Append(messages[messages.Length - 1]);
                        }*/
                    }
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Exception in ReceiveData: " + e.Message);
            receivedData.Clear();
        }
    }

    void ProcessMessage(string message)
    {
        // Handle each complete message
        Debug.Log("Received complete message: " + message);
        // You can call DecodeInt or any other processing method here based on your message format
        _networkMessageManager.ReceiveMessage(message);
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
