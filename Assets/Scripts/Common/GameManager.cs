using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private LevelManager _levelManager;
    private SaveController _saveController;
    private CameraMovement _cameraMovement;
    private UIController _uiController;
    private GameData _gameData;
    public int Coins => _gameData.Coins;

    public event Action<int> OnCoinCountChanged;

    private void Awake()
    {
        _levelManager = FindObjectOfType<LevelManager>();
        _saveController = new SaveController();
        _cameraMovement = FindObjectOfType<CameraMovement>();
        _uiController = FindObjectOfType<UIController>();
        _gameData = _saveController.LoadData();
        _uiController.ShowPanel(UIController.PanelType.Start);
    }

    public void StartGame()
    {
        _levelManager.InstantiateLevel(_gameData.Level);
        _uiController.ShowPanel(UIController.PanelType.Game);
        OnGameStarted();
    }

    private void OnGameStarted()
    {
        //_cameraMovement.Initialize(_levelManager.PlayerController.transform);
        //_cameraMovement.Initialize(_levelManager.PlayerController.transform.GetChild(1));
        _levelManager.PlayerController.OnWin += OnWin;
        _levelManager.PlayerController.OnDead += OnFail;
        _levelManager.PlayerController.OnCoinCollected += OnCoinCollected;
    }

    private void OnGameEnded()
    {
        _levelManager.PlayerController.OnWin -= OnWin;
        _levelManager.PlayerController.OnWin -= OnFail;
        _levelManager.PlayerController.OnCoinCollected -= OnCoinCollected;
        _saveController.SaveData(_gameData);
    }
    

    private void OnWin()
    {
        _gameData.Level++;
        _uiController.ShowPanel(UIController.PanelType.Win);
        OnGameEnded();
    }
    private void OnFail()
    {
        _uiController.ShowPanel(UIController.PanelType.Fail);
        OnGameEnded();
    }

    private void OnCoinCollected()
    {
        _gameData.Coins++;
        OnCoinCountChanged?.Invoke(_gameData.Coins);
    }

}
