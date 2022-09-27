using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Toolbox;

public class StartScene : MonoBehaviour
{
    [Header("Loading bar")]
    public Image loadingBar = null;

    const string mainMenuScene = "MainMenu";
    private Timer timer = new Timer();

    private IEnumerator Start()
    {
        timer.SetTimer(1, Timer.TIMER_MODE.INCREASE, true);
        loadingBar.fillAmount = 0;

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

    private void Update()
    {        
        if (timer.Active)
        {
            timer.UpdateTimer();
            loadingBar.fillAmount = timer.CurrentTime;
        }

        if (timer.ReachedTimer()) loadingBar.fillAmount = 1;
    }
}
