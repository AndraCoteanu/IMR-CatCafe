using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitButton : MonoBehaviour
{
    void Start()
    {
        exitGame();
    }

    public void exitGame()
    {
        Application.Quit();
    }
}
