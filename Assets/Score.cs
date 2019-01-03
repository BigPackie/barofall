using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    public Text restarts;
    public Text level;
    public Text totalTime;
    public Text levelTime;

    public Button next;
    public Button previous;

    private int scoreIndex;

    public List<LevelScore> levelScores;


    // Use this for initialization
    void Start () {
        levelScores = Persistance.Load().levelScores;
        scoreIndex = levelScores.Count - 1;
        this.CurrentScore();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

     void ShowLevelScores()
    {
        var levelScore = levelScores[this.scoreIndex];
        this.level.text = "Level "  + levelScore.levelFinished;
        this.levelTime.text = levelScore.levelTime.ToString("00:00.00");
        this.totalTime.text = levelScore.totalTime.ToString("00:00.00");
        this.restarts.text = levelScore.restarts.ToString();
    }

    public void CurrentScore()
    {
        this.ShowLevelScores();
        this.DissableButtons();
    }

    public void NextScore()
    {
        this.scoreIndex++;
        this.ShowLevelScores();
        this.DissableButtons();
    }

    public void PreviousScore()
    {
        this.scoreIndex--;
        this.ShowLevelScores();
        this.DissableButtons();
    }

    void DissableButtons()
    {
        if(scoreIndex  == 0)
        {
            previous.interactable = false;
        }
        else
        {
            previous.interactable = true;
        }

        if(scoreIndex == levelScores.Count - 1)
        {
            next.interactable = false;
        }
        else
        {
            next.interactable = true;
        }
    }
}
