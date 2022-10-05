using TMPro;
using UnityEngine;

public class UiCoins : MonoBehaviour
{
    private TextMeshProUGUI _text;

    private void Awake()
    {
        _text = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void UpdateCoins(int coins)
    {
        _text.text = coins.ToString();
    }
}