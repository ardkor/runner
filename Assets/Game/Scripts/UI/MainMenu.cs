using UnityEngine;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private CoinCounter _coinCounter;
    [SerializeField] private RecordTable _scoreTable;

    [SerializeField] private Button _startButton;
    [SerializeField] private Button _recordsTableButton;
    [SerializeField] private Button _recordsTableCloseButton;
    [SerializeField] private Button _exitButton;

    private void OnEnable()
    {
        _startButton.onClick.AddListener(_coinCounter.Open);
        _startButton.onClick.AddListener(_gameManager.StartGame);
        _startButton.onClick.AddListener(Close);
        _exitButton.onClick.AddListener(_gameManager.Exit);
        _recordsTableButton.onClick.AddListener(_scoreTable.Open);
        _recordsTableCloseButton.onClick.AddListener(_scoreTable.Close);
    }

    private void OnDisable()
    {
        _startButton.onClick.RemoveListener(_coinCounter.Open);
        _startButton.onClick.RemoveListener(_gameManager.StartGame);
        _startButton.onClick.RemoveListener(Close);
        _exitButton.onClick.RemoveListener(_gameManager.Exit);
        _recordsTableButton.onClick.RemoveListener(_scoreTable.Open);
        _recordsTableCloseButton.onClick.RemoveListener(_scoreTable.Close);
    }
    
    public void Open()
    {
        gameObject.SetActive(true);
    }
    public void Close()
    {
        gameObject.SetActive(false);
    }
}