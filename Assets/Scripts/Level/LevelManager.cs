using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject[] levels;

    private GameObject _currentLevel;
    
    public PlayerController PlayerController { get; private set; }

    public void InstantiateLevel(int index)
    {
        if (_currentLevel)
        {
            Destroy(_currentLevel);
        }

        index = index % levels.Length;
        _currentLevel = Instantiate(levels[index], transform);
        PlayerController = _currentLevel.GetComponentInChildren<PlayerController>();

    }
}
