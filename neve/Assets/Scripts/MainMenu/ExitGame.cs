using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitGame : MonoBehaviour
{
   
    public void Exit()
    {
        //EditorApplication.isPlaying = false;
        Application.Quit();
        Debug.Log("ÍË³öÓÎÏ·");
    }
}
