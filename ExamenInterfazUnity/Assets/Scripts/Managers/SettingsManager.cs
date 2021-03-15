using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{

    private Text[] textList;
    private GameObject[] panelList;
    private Button[] buttons;

    public Canvas canvasSettings, canvasMenu;

    public Slider textSize, volume;
    public Dropdown fontColor;
    public Toggle darkTheme;
    public AudioSource music;

    public int defaultFontSize;

    
    // Start is called before the first frame update
    void Start()
    {

        textList = FindObjectsOfType<Text>();
        buttons = FindObjectsOfType<Button>();
        panelList = GameObject.FindGameObjectsWithTag("Panel");
        defaultFontSize = 40;

        canvasSettings.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeFontColor()
    {
        Color color = Color.black;

        switch (fontColor.value)
        {
            case 0: //BLACK
                color = Color.black;
                break;

            case 1: //RED
                color = Color.red;
                break;

            case 2: //YELLOW
                color = Color.yellow;
                break;

            case 3: //GREEN
                color = Color.green;
                break;

            case 4: //BLUE
                color = Color.blue;
                break;
        }

        foreach (Text t in textList)
        {
            t.color = color;
        }

    }

    public void ChangeFontSize()
    {
        foreach (Text t in textList)
        {  
            t.fontSize = (int) (defaultFontSize * textSize.value);           
        }
    }

    public void ChangeVolume()
    {
        music.volume = volume.value;
    }

    public void ChangeTheme()
    {
        if (darkTheme.isOn)
        {
            foreach (GameObject g in panelList)
            {
                g.GetComponent<Image>().color = Color.black;
            }

            foreach (Button b in buttons)
            {
                var colors = b.colors;
                colors.normalColor = Color.grey;
                b.colors = colors;
            }

            foreach (Text t in textList)
            {
                t.color = Color.white;
            }

            fontColor.GetComponent<Image>().color = Color.black;
        }
        else
        {
            foreach (GameObject g in panelList)
            {
                g.GetComponent<Image>().color = Color.white;
            }

            foreach (Button b in buttons)
            {
                var colors = b.colors;
                colors.normalColor = Color.white;
                b.colors = colors;
            }

            foreach (Text t in textList)
            {
                t.color = Color.black;
            }

            fontColor.GetComponent<Image>().color = Color.grey;

        }
    }

    public void exitToMainMenu()
    {
        canvasSettings.enabled = false;
        canvasMenu.enabled = true;
    }

    public void goToSettings()
    {
        canvasSettings.enabled = true;
        canvasMenu.enabled = false;
    }

    public void exitGame()
    {
        Application.Quit();
    }
}
