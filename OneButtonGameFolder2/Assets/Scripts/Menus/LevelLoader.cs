using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider loadBar;
    public void LoadLevel(string sceneName)
    {
        StartCoroutine(sceneName);
    }

    IEnumerator LoadAsynchronously (string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        loadingScreen.SetActive(true);
        yield return new WaitForSeconds(1);
        
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress /.9f);
            loadBar.value = progress;
            yield return null;
        }
    }

}
