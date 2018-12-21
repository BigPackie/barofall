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
        backGround.SetActive(false); 
        scorePanel.SetActive(true);
    }
}
