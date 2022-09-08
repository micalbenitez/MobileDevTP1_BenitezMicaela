using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

public class UICredits : MonoBehaviour
{
    [Header("Scenes")]
    [SerializeField] private string mainMenuSceneName = "";

    public void MainMenu()
    {
        LoaderManager.Instance.LoadScene(mainMenuSceneName);
    }
}