using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Managers;

public class UIMainMenu : MonoBehaviour
{
    [Header("Scenes")]
    [SerializeField] private string creditsSceneName = "";
    [SerializeField] private string gameSceneName = "";

    [Header("Groups")]
    [SerializeField] private GameObject menuGroup = null;
    [SerializeField] private GameObject ConfigurationGroup = null;

    [Header("Players")]
    [SerializeField] private Button[] playersButtons = null;

    [Header("Difficult")]
    [SerializeField] private Button[] difficultButtons = null;

    private void Start()
    {
        GameConfiguration.Instance.ChangeButtonColor(playersButtons, (int)GameConfiguration.Instance.playersQuantity);
        GameConfiguration.Instance.ChangeButtonColor(difficultButtons, (int)GameConfiguration.Instance.difficult);
    }

    public void ActiveMenuGroup()
    {
        menuGroup.SetActive(true);
        ConfigurationGroup.SetActive(false);
    }

    public void ActiveConfigurationGroup()
    {
        menuGroup.SetActive(false);
        ConfigurationGroup.SetActive(true);
    }

    public void Game()
    {
        LoaderManager.Instance.LoadScene(gameSceneName);
    }

    public void Credits()
    {
        LoaderManager.Instance.LoadScene(creditsSceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SetPlayers(int gameMode)
    {
        GameConfiguration.Instance.SetPlayers(gameMode);
        GameConfiguration.Instance.ChangeButtonColor(playersButtons, (int)GameConfiguration.Instance.playersQuantity);
    }

    public void SetDifficult(int gameDifficult)
    {
        GameConfiguration.Instance.SetDifficult(gameDifficult);
        GameConfiguration.Instance.ChangeButtonColor(difficultButtons, (int)GameConfiguration.Instance.difficult);
    }
}