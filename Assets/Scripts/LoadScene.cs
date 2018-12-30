using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {

    //scene indexes can be seen in the build setting menu

    public void ContinueGame()
    {
        SceneState.instance.continueGame = true;
        SceneState.instance.ignoreFirstCheckpoint = true;
        SceneManager.LoadScene(1);
    }

    public void NewGame()
    {
        SceneState.instance.newGame = true;
        SceneState.instance.ignoreFirstCheckpoint = false;
        SceneManager.LoadScene(1);
    }

    public void MainMenu()
    {
        SceneState.instance.newGame = false;
        SceneState.instance.continueGame = false;
        SceneState.instance.ignoreFirstCheckpoint = false;
        SceneManager.LoadScene(0);
    }
}
