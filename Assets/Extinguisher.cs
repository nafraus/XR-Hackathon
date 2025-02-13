using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extinguisher : MonoBehaviour
{
    [SerializeField] private int ID;

    [SerializeField] private Fire fire;

    [SerializeField] private GameObject hoverObject;

    public void Hover()
    {
        hoverObject.SetActive(true);
    }

    public void Unhover()
    {
        hoverObject.SetActive(false);
    }

    public void Clicked()
    {
        if(fire != null) fire.TryPutOutFire(ID);
        Unhover();
    }
    
    
}
