using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadAbout : MonoBehaviour {

    public GameObject backGround;
    public GameObject backGroundMainMenu;
    public GameObject aboutPanel;
    public GameObject mainMenuPanel;
    public GameObject particles;


    public void LoadAboutPanel()
    {    
        mainMenuPanel.SetActive(false);
        particles.SetActive(false);
        backGround.SetActive(true);
        backGroundMainMenu.SetActive(false);
        aboutPanel.SetActive(true);
    }

}
