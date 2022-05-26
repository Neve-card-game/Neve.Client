using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitGame : MonoBehaviour
{
    private ServerConnector connector;

    private void Awake() {
        connector = FindObjectOfType<ServerConnector>();
    }
    public void Disconnect(){
        connector.Disconnect();
    }

    public void Exit()
    {
        //EditorApplication.isPlaying = false;
        Application.Quit();
        Debug.Log("Exit Game");
    }
}
