using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneState : MonoBehaviour {

    public static bool continueGame = false;
    public static bool newGame = false;

    public void SetContinueGame(bool value)
    {
        SceneState.continueGame = value;
    }

    public void SetNewGame(bool value)
    {
        SceneState.newGame = value;
    }
}
