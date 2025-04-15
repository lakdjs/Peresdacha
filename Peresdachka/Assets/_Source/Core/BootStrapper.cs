using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BootStrapper : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private UImanager UIManager;
    [SerializeField] private float scorePerSecond = 10f;

    private void Awake()
    {
        SetUp();
    }
    private void SetUp()
    {
        gameManager.SetScorePerSecond(scorePerSecond);
        gameManager.LoadGame();

        UIManager.UpdateScore(gameManager.Score);
        UIManager.UpdateTime(gameManager.PlayTime);
    }
}
