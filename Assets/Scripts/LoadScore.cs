using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScore : MonoBehaviour {

    public GameObject backGround;
    public GameObject backGroundMainMenu;
    public GameObject scorePanel;
    public GameObject mainMenuPanel;
    public GameObject particles;


    public void LoadScorePanel()
    {
        mainMenuPanel.SetActive(false);
        particles.SetActive(false);
        backGround.SetActive(true);
        backGroundMainMenu.SetActive(false);
        scorePanel.SetActive(true);

        //LoadScoreStats();
    }


    private void LoadScoreStats()
    {
        var gameState = Persistance.Load();

        if (gameState.levelScores.Count == 0)
        {
            Debug.Log("No scores available");
            return;
        }

        foreach (LevelScore ls in gameState.levelScores)
        {
            Debug.Log("LEvel: " + ls.levelFinished + "  levelTime: " + ls.levelTime + "  totaltime: " + ls.totalTime + "  restarts: " + ls.restarts);
        }

    }


}
