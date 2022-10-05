using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private LevelManager _levelManager;
    private SaveController _saveController;
    private CameraMovement _cameraMovement;
    private UiController _uiController;
    private GameData _gameData;

    public int Coins => _gameData.Coins;
    public event Action<int> OnCoinCountChanged;

    private void Awake()
    {
        _levelManager = FindObjectOfType<LevelManager>();
        _saveController = new SaveController();
        _cameraMovement = FindObjectOfType<CameraMovement>();
        _uiController = FindObjectOfType<UiController>();
        _gameData = _saveController.LoadData();
        _uiController.ShowPanel(UiController.PanelType.Start);
    }

    public void StartGame()
    {
        _levelManager.InstantiateLevel(_gameData.Level);
        _uiController.ShowPanel(UiController.PanelType.Game);
        OnGameStarted();
    }

    private void OnGameStarted()
    {
        _cameraMovement.Initialize(_levelManager.PlayerController.transform);
        _levelManager.PlayerController.OnWin += OnWin;
        _levelManager.PlayerController.OnDead += OnFail;
        _levelManager.PlayerController.OnCoinCollected += OnCoinCollected;
    }

    private void OnGameEnded()
    {
        _levelManager.PlayerController.OnWin -= OnWin;
        _levelManager.PlayerController.OnDead -= OnFail;
        _levelManager.PlayerController.OnCoinCollected -= OnCoinCollected;
        _saveController.SaveData(_gameData);
    }

    private void OnWin()
    {
        _gameData.Level++;
        _uiController.ShowPanel(UiController.PanelType.Win);
        OnGameEnded();
    }

    private void OnFail()
    {
        _uiController.ShowPanel(UiController.PanelType.Fail);
        OnGameEnded();
    }

    private void OnCoinCollected()
    {
        _gameData.Coins++;
        OnCoinCountChanged?.Invoke(_gameData.Coins);
    }
}