using UnityEngine;

public class UIController : MonoBehaviour
{
    public enum PanelType
    {
        None,
        Start,
        Game,
        Win,
        Fail
    }
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject failPanel;

    private GameObject _currentPanel;
    private GameObject CurrentPanel
    {
        get => _currentPanel;
        set
        {
            if (_currentPanel==value) 
                return;

            if (_currentPanel!=null) 
                _currentPanel.SetActive(false);
                _currentPanel = value;
                _currentPanel.SetActive(true);
        }
    }

    public void ShowPanel(PanelType type)
    {
        switch (type)
        {
            case PanelType.Start:
                CurrentPanel = startPanel;
                break;
            case PanelType.Game:
                CurrentPanel = gamePanel;
                break;
            case PanelType.Win:
                CurrentPanel = winPanel;
                break;
            case PanelType.Fail:
                CurrentPanel = failPanel;
                break;
        }
    }
}
