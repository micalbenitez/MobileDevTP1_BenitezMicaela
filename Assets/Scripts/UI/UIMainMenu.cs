using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

public class UIMainMenu : MonoBehaviour
{
    [Header("Scenes")]
    [SerializeField] private string creditsSceneName = "";
    [SerializeField] private string gameSceneName = "";

    [Header("Groups")]
    [SerializeField] private GameObject menuGroup = null;
    [SerializeField] private GameObject ConfigurationGroup = null;

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
}