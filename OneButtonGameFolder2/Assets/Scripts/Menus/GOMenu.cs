using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class GOMenu : MonoBehaviour
{
    bool gameHasEnded = false;
    public GameObject GOMenuUI;
    public GameObject OptionsMenuUI;
    public TextMeshProUGUI endPrizeText;
    public Image BonePrizeIcon;

    public enum BonePrize { platinum, gold, silver, bronze, nobone };
    public static BonePrize CurrentPrize { get; set; } //trying to use properties here, rather than Getter/Setter functions. 

    public void Update()
    {
        Debug.Log(CurrentPrize + "");
    }
    public void Start()
    {
        gameHasEnded = false;
        GOMenuUI.SetActive(false);
    }

    public void EndGame()
    {
        switch(CurrentPrize) //The reward screen 
        {
            case BonePrize.nobone:
                BonePrizeIcon.color = Color.black;
                endPrizeText.text = "No bone for that performance! \n BAD DOG!";
                endPrizeText.fontSize = 40;
                endPrizeText.color = Color.grey;
                break;
            case BonePrize.bronze:
                BonePrizeIcon.color = new Color(0.7f, 0.3f, 0f, 1);
                endPrizeText.text = "This very mediocre bronze bone for " + MechanicsGlobal.kittyKillCount + " Kitties barked at.";
                endPrizeText.fontSize = 30;
                endPrizeText.color = new Color(0.7f, 0.3f, 0f, 1);
                break;
            case BonePrize.silver:
                BonePrizeIcon.color = new Color(0.8f,0.82f,0.81f,1);
                endPrizeText.text = "This very respectable SILVER bone for " + MechanicsGlobal.kittyKillCount + " Kitties barked at.";
                endPrizeText.fontSize = 40;
                endPrizeText.color = new Color(0.8f, 0.82f, 0.81f, 1);
                break;
            case BonePrize.gold:
                BonePrizeIcon.color = new Color(1,0.83f,0,1);
                endPrizeText.text = "The awe-inspiring Golden Bone of legend for " + MechanicsGlobal.kittyKillCount + " Kitties barked at.";
                endPrizeText.fontSize = 50;
                endPrizeText.color = new Color(1, 0.83f, 0, 1);
                break;
            case BonePrize.platinum:
                BonePrizeIcon.color = Color.white;
                endPrizeText.text = "The ONE and ONLY PLATINUM BONE!";
                endPrizeText.fontSize = 60;
                endPrizeText.color = Color.white;
                break;
        }

        if (gameHasEnded == false)
        {
            gameHasEnded = true;
            GOMenuUI.SetActive(true); //setting the UI for the menu to be visible. 
            //FADE-IN ANIMATION?
            Time.timeScale = 0f;
        }
    }

    #region BUTTON FUNCTIONALITY
    public void Restart()
    {
        SceneManager.LoadScene("MegaBark");
    }

    public void Options() 
    {
        GOMenuUI.SetActive(false);
        OptionsMenuUI.SetActive(true);
        //go to options menu. 
    }

    public void MainMenuFromGO()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    public void QuitGameFromGO()
    {
        Debug.Log("Quitted");
        Application.Quit();
    }
    #endregion FUNCTIONALITY
}
