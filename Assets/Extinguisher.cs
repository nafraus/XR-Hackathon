using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extinguisher : MonoBehaviour
{
    [SerializeField] private int ID;


    private void OnCollisionEnter(Collision other)
    {
        Fire fire= other.gameObject.GetComponent<Fire>();
        
        if(fire != null) fire.TryPutOutFire(ID);
    }
}
