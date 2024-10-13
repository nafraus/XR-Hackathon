using System;
using System.Net;
using UnityEngine;

namespace UnityWebSockets
{
    [RequireComponent(typeof(WebsocketDispatcher))]
    public class WebsocketFactory : MonoBehaviour
    {
        private WebsocketDispatcher dispatcher;

        private void Awake()
        {
            dispatcher = GetComponent<WebsocketDispatcher>();

            if (dispatcher == null) throw new NullReferenceException("dispatcher is null");
        }

        public WebsocketWrapper CreateWebSocket(IPAddress address, int port)
        {
            string websocketAddress = $"ws://{address}:{port}/";
            return new WebsocketWrapper(dispatcher, websocketAddress);
        }
    }
}
