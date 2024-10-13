using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Juice : MonoBehaviour
{
    [SerializeField] private Transform juiceScaler;
    private float fillRate;
    private float drainRate = 0;
    [SerializeField] private Renderer errorLight;

    public bool isFixed => juiceScaler.localScale.y >= 0.5f;
    
    [SerializeField] 
    public void SetJuiceLevel(int value)
    {
        
        //BETWEEN 280 - 500
        //500 = 0 
        //280 is highest
        
        value *= -1;
        value += 500;
        fillRate = ((float) value) / 280f;
        fillRate *= .33f;

        timeOfLastJuiceUpdate = Time.time;
    }

    private float timeOfLastJuiceUpdate;

    private void Update()
    {
        Vector3 scale = juiceScaler.localScale;
        //Debug.Log(fillRate + drainRate);
        scale.y += (fillRate + drainRate) * Time.deltaTime;
        scale.y = Mathf.Clamp(scale.y, 0.1f, 0.9f); 
        juiceScaler.localScale = scale;

        if (Time.time - timeOfLastJuiceUpdate >= 1f)
        {
            fillRate = 0;
        }
        
        if(scale.y < 0.5f) errorLight.material.color = Color.red;
        else errorLight.material.color = Color.green;
    }

    public void AddJuice(int value)
    {
        Vector3 scale = juiceScaler.localScale;
        scale.y += value;
        juiceScaler.localScale = scale;
    }

    public void BreakJuice()
    {
        Vector3 scale = juiceScaler.localScale;
        scale.y = 0.15f;
        juiceScaler.localScale = scale;
        drainRate = -0.02f;
    }
}
