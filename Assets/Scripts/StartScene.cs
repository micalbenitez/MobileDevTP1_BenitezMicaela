using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    const string mainMenuScene = "MainMenu";

    private IEnumerator Start()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(mainMenuScene);
        asyncLoad.allowSceneActivation = false;
        while (asyncLoad.progress < 0.9f)
        {
            Debug.Log("Loading: " + asyncLoad.progress);
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        asyncLoad.allowSceneActivation = true;
    }
}
