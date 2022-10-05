using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiGamePanel : MonoBehaviour
{
    private UiCoins _uiCoins;
    private GameManager _gameManager;

    private void Awake()
    {
        _uiCoins = GetComponentInChildren<UiCoins>();
        _gameManager = FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        _gameManager.OnCoinCountChanged += UpdateCoins;
    }

    private void OnEnable()
    {
        _uiCoins.UpdateCoins(_gameManager.Coins);
    }

    private void OnDestroy()
    {
        _gameManager.OnCoinCountChanged -= UpdateCoins;
    }

    private void UpdateCoins(int coins)
    {
        _uiCoins.UpdateCoins(coins);
    }
}
