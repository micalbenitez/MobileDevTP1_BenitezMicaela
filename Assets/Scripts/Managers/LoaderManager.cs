using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Toolbox;

namespace Managers
{
    public class LoaderManager : MonoBehaviourSingleton<LoaderManager>
    {
        const string loadingScene = "LoadingScene";

        public void LoadScene(string sceneName)
        {
            StartCoroutine(InternalLoadScene(sceneName));
        }

        private IEnumerator InternalLoadScene(string sceneName)
        {
            SceneManager.LoadScene(loadingScene);

            yield return null;

            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
            asyncLoad.allowSceneActivation = false;
            while (asyncLoad.progress < 0.9f)
            {
                Debug.Log("Loading: " + asyncLoad.progress);
                yield return null;
            }

            yield return new WaitForSeconds(1f);
            asyncLoad.allowSceneActivation = true;
            Destroy(gameObject);
        }
    }
}