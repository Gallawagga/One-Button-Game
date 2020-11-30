using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MechanicsGlobal : MonoBehaviour
{
    [Header("Global UI")]
    public TextMeshProUGUI kittyCounter;
    public TextMeshProUGUI gameTimeText;
    GOMenu goMenu;
    //PauseMenu pauseMenu;
    //OptionsMenu optionsMenu;


    //private variables:
    private int gameTimeMax = 60;
    private int currentTime;
    private int endTime = 0;



    //SCORE THRESHOLDS:
    static int bronzeBoneGoal = 20; static int silverBoneGoal = 40; static int goldBoneGoal = 60; static int platinumBoneGoal = 70;
    //static variables:
    public static int kittyKillCount;

    void Start()
    {
        currentTime = gameTimeMax;
        kittyKillCount = 0;
        goMenu = GetComponent<GOMenu>();
        StartCoroutine(GameTimeCount());
        GOMenu.CurrentPrize = GOMenu.BonePrize.nobone;
        Time.timeScale = 1;
    }

    void Update()
    {
        kittyCounter.text = kittyKillCount + "";
        if(Time.timeScale == 0)
        { Cursor.visible = true; }
        if(Time.timeScale ==1 )
        {
            Cursor.visible = false;
        }
    }

    IEnumerator GameTimeCount ()
    {
        while (currentTime > endTime)
        {
            currentTime -= 1;
            gameTimeText.text = "" + currentTime;
            yield return new WaitForSeconds(1);
        }
        goMenu.EndGame();
    }

    public static void AddToScore(int value) //accessed and used by the Cat Class.
    {
        kittyKillCount += value;

        if (kittyKillCount >= bronzeBoneGoal) //wanted to check prize status only when the score is added to, rather than in constant update. 
        {
            GOMenu.CurrentPrize = GOMenu.BonePrize.bronze;
        }
        if (kittyKillCount >= silverBoneGoal)
        {
            GOMenu.CurrentPrize = GOMenu.BonePrize.silver;
        }
        if (kittyKillCount >= goldBoneGoal)
        {
            GOMenu.CurrentPrize = GOMenu.BonePrize.gold;
        }
        if (kittyKillCount >= platinumBoneGoal)
        {
            GOMenu.CurrentPrize = GOMenu.BonePrize.platinum;
        }
    }
}
