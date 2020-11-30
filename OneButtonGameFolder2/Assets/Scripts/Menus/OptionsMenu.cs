using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
    public GameObject OptionsUI;
    public GameObject PauseUI;
    public GameObject GOmenuUI;
    public GameObject GOoptionsUI;

  public void BackToPause()
    {
        OptionsUI.SetActive(false);
        PauseUI.SetActive(true);
    }

    public void BackToGO()
    {
        GOoptionsUI.SetActive(false);
        GOmenuUI.SetActive(true);
    }

    //volume control

}
