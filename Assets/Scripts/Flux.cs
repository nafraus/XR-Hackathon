using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class Flux : MonoBehaviour
{
    public bool isFixed = false;
    private int solution = 0;
    [SerializeField] private Animator animator;
    
    [SerializeField] private TMP_Text text;

    public void BreakFlux()
    {
        isFixed = false;
        GenerateRandomSolution();
        text.color = Color.red;
    }

    public void FixFlux()
    {
        text.color = Color.green;
        text.text = "C";
        isFixed = true;
    }
    public void TryFluxInput(int[] input)
    {
        if (input[0] == solution)
        {
            FixFlux();
        }
    }

    private void GenerateRandomSolution()
    {
        solution = Random.Range(1, 5);
        string display = "";
        switch ( solution)
        {
            case 1:
                display = "bc";
                break;
            case 2:
                display = "be";
                break;
            case 3:
                display = "ge";
                break;
            case 4:
                display = "gc";
                break;
        }

        text.text = display;
    }
}
