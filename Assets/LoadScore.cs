using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScore : MonoBehaviour {

    public GameObject backGround;
    public GameObject scorePanel;
    public GameObject mainMenuPanel;
    public GameObject particles;


    public void LoadScorePanel()
    {
        mainMenuPanel.SetActive(false);
        particles.SetActive(false);
        backGround.SetActive(true); // it is higher in hierarchy, so it overlays the home menu backgournd if activated
        scorePanel.SetActive(true);
    }
}
