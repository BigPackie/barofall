using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadAbout : MonoBehaviour {

    public GameObject backGround;
    public GameObject aboutPanel;
    public GameObject mainMenuPanel;
    public GameObject particles;


    public void LoadAboutPanel()
    {    
        mainMenuPanel.SetActive(false);
        particles.SetActive(false);
        backGround.SetActive(true); // it is higher in hierarchy, so it overlays the home menu backgournd if activated
        aboutPanel.SetActive(true);
    }

}
