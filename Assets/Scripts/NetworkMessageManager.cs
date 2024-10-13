using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkMessageManager : MonoBehaviour
{
    [SerializeField] private Flux flux;
    [SerializeField] private Juice juice;
    [SerializeField] private Fire fire; //Not networked
    public void ReceiveMessage(string message)
    {
        switch (message[0])
        {
            case '1':  //flux
                // Split by underscore and get the part after it
                string numbersPart = message.Split('_')[1];

                // Split by comma and convert to an array of integers
                int[] numbers = Array.ConvertAll(numbersPart.Split(','), int.Parse);
                
                flux.TryFluxInput(numbers);
                break;
            case '2': //Juice
                // Split by underscore and get the part after it
                string numberPart = message.Split('_')[1];
                // Convert the part after the underscore to an integer
                int parsedNumber = int.Parse(numberPart);
                juice.SetJuiceLevel(parsedNumber);
                break;
        }
    }
}
