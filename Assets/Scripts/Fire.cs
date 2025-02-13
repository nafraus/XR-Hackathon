using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Fire : MonoBehaviour
{
    private int failureId;
    public bool isFixed = false;

    [SerializeField] private GameObject[] fireObjects;
    

    public void TryPutOutFire(int id)
    {
        if (id == failureId) FixFire();
        //else()
    }

    public void FixFire()
    {
        fireObjects[failureId].SetActive(false);
        isFixed = true;
    }

    public void BreakFire()
    {
        isFixed = false;
        failureId = Random.Range(0, 3);
        Debug.Log(failureId);

        fireObjects[failureId].SetActive(true);
        
        //0 red
        //1 blue
        //2 green
    }
}
