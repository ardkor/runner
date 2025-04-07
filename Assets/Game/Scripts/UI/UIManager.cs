using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject MainMenu;
    [SerializeField] private GameObject EndGameMenu;
    
    public void OpenMainMenu()
    {
        MainMenu.SetActive(true);
    }
    public void CloseMainMenu()
    {
        MainMenu.SetActive(false);
    }
    
    public void OpenEndGameMenu()
    {
        EndGameMenu.SetActive(true);
    }
    public void CloseEndGameMenu()
    {
        EndGameMenu.SetActive(false);
    }
}
