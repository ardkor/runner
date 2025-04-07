using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class EndGameMenu : MonoBehaviour
{
    [SerializeField] private MainMenu _mainMenu;

    [SerializeField] private Player _player;
    [SerializeField] private CoinCounter _coinCounter;

    [SerializeField] private Button _saveScoreButton;
    [SerializeField] private Button _closeButton;
    [SerializeField] private TMP_Text _score;
    [SerializeField] private InputField _input;

    private void OnEnable()
    {
        _saveScoreButton.onClick.AddListener(SaveScore);
        _closeButton.onClick.AddListener(Close);
        _player.died += SetScore;
    }

    private void OnDisable()
    {
        _saveScoreButton.onClick.RemoveListener(SaveScore);
        _closeButton.onClick.RemoveListener(Close);
        _player.died -= SetScore;
    }

    private void SetScore()
    {
        _score.text = _coinCounter.Coins.ToString();
    }

    private void SaveScore()
    {
        Record record = new Record(_input.text, _coinCounter.Coins);
        JsonUtility.ToJson(record);
    }

    private void Close()
    {
        _coinCounter.Close();
        _mainMenu.Open();
        gameObject.SetActive(false);
    }
}