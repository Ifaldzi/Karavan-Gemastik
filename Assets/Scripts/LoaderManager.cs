using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoaderManager : MonoBehaviour
{

    public GameObject loadingScreen;

    public void loadScene(int sceneIndex)
    {
        StartCoroutine(loadSceneAsync(sceneIndex));
    }

    IEnumerator loadSceneAsync(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            yield return null;
        }
    }
}
