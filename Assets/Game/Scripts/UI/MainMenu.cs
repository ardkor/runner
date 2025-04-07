using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI
{
    public class MainMenu
    {
        [SerializeField] private UIManager _uiManager;
        [SerializeField] private GameManager _gameManager;
        
        [SerializeField] private RecordTable _scoreTable;

        [SerializeField] private Button _startButton;
        [SerializeField] private Button _recordsTableButton;
        [SerializeField] private Button _recordsTableCloseButton;
        [SerializeField] private Button _exitButton;

        private void OnEnable()
        {
            _startButton.onClick.AddListener(_gameManager.StartGame);
            _startButton.onClick.AddListener(Close);
            _exitButton.onClick.AddListener(_gameManager.Exit);
            _recordsTableButton.onClick.AddListener(OpenTable);
            _recordsTableCloseButton.onClick.AddListener(CloseTable);
        }

        private void OnDisable()
        {
            _startButton.onClick.RemoveListener(_gameManager.StartGame);
            _startButton.onClick.RemoveListener(Close);
            _exitButton.onClick.RemoveListener(_gameManager.Exit);
            _recordsTableButton.onClick.AddListener(OpenTable);
            _recordsTableCloseButton.onClick.AddListener(CloseTable);
        }
        
        private void OpenTable()
        {
            _scoreTable.Open();
        }
        private void CloseTable()
        {
            _scoreTable.Close();
        }

        private void Close()
        {
            _uiManager.CloseMainMenu();
        }
    }
}