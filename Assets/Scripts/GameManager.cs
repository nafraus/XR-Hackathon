using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Flux flux;
    [SerializeField] private Fire fire;
    [SerializeField] private Juice juice;
    [SerializeField] private TcpServerClient tcp;
    [SerializeField] private GameObject winObj;

    public static GameManager S;

    private void Awake()
    {
        if (S == null) S = this;
        else Destroy(gameObject);
    }

    [Button]
    public void StartGame()
    {
     flux.BreakFlux();
     fire.BreakFire();
     juice.BreakJuice();
     winObj.SetActive(false);
    }


    public bool EvaluateFixedPuzzles()
    {
        return (flux.isFixed & juice.isFixed & fire.isFixed);
    }

    private bool hasWon = false;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) ) WinGame();
        if (hasWon) return;

        if (EvaluateFixedPuzzles())
        {
            WinGame();
        }
    }

    private void WinGame()
    {
        Debug.Log("Sending Winner Message");
        hasWon = true;
        tcp.SendMessage("you win");
        winObj.SetActive(true);
    }
}
