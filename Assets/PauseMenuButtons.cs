using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuButtons : MonoBehaviour {

	public void Continue()
    {
        EventManager.TriggerEvent("continue");
    }

    public void RestartFromCheckPoint()
    {
        EventManager.TriggerEvent("restartFromCheckPoint");
    }

    public void RestartLevel()
    {
        EventManager.TriggerEvent("restartLevel");
    }

    public void MainMenu()
    {
        EventManager.TriggerEvent("mainMenu");
    }
}
