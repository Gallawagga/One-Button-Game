using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarkBar : MonoBehaviour
{
    private Image barkBar;
    private Color barColor;
    public float currentFill;
    private float maxFill;
    public GameObject BarkGun;
    Bark barkScript;

    void Start()
    {
        barkScript = BarkGun.GetComponent<Bark>();
        barkBar = GetComponent<Image>();
        barColor = barkBar.color;
        maxFill = barkScript.maxBarkStrength;
    }

    void Update()
    {
        currentFill = barkScript.barkStrength;
        barkBar.fillAmount = currentFill / maxFill;
    }

    public void LerpColour(float currentTime, float maxTime)  //usually timeHeld and maxHoldDownTime from the Bark script.
    {
        float i = currentTime / maxTime;
        barkBar.color = Color.Lerp(barColor, Color.red, i);
    }
}
