using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class MainMenu : MonoBehaviour
{
    [Header("Main Menu Configuration")]
    public GameObject MMUI;
    public GameObject OptUI;
    public GameObject LoadUI;
    public Slider loadBar;

    [Header("Press Any Key Configuration")]
    public GameObject PAKUI;
    public List<TextMeshProUGUI> snippets;
    private bool gateBool = false;

    public enum screenState { PAK, MM };
    public static screenState currentScreen { get; set; } //trying to use properties here, rather than Getter/Setter functions. 

    private void Start()
    {
        MMUI.SetActive(false);
        PAKUI.SetActive(true);
        currentScreen = screenState.PAK;
        Time.timeScale = 1;
    }

    private void Update()  //very small state machine to control the press any key screen. 
    {
        switch (currentScreen)
        {
            case screenState.PAK:
                if (Input.anyKey)
                {
                    currentScreen = screenState.MM;
                }
                break;
            case screenState.MM:
                if(gateBool == false)
                {
                    Cursor.visible = true;
                    MMUI.SetActive(true);
                    PAKUI.SetActive(false);
                    gateBool = true;
                }
                break;
        }
    }
    #region MAINMENU
    public void PlayGame()
    {
        StartCoroutine(LoadAsynchronously("MegaBark"));
    }

    public void QuitGame()
    {
        Debug.Log("Quitted");
        Application.Quit();
    }
    #endregion MAINMENU

    #region LOADINGSCREEN

    IEnumerator LoadAsynchronously(string sceneName) //code for for the loading screen's progress bar. 
    {
        LoadUI.SetActive(true);
        MMUI.SetActive(false);
        yield return new WaitForSeconds(1);
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName); //loads the scene asynchronously.

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);  //clamps value between 0 and 1
            loadBar.value = progress;
            yield return null;
        }
    }
    #endregion LOADINGSCREEN
    #region OPTIONS
    public void Options()
    {
        MMUI.SetActive(false);
        OptUI.SetActive(true);
    }
    public void BackFromOptions()
    {
        MMUI.SetActive(true);
        OptUI.SetActive(false);
    }

    //volumecontrol - music not implemented. 

    #endregion OPTIONS
}

