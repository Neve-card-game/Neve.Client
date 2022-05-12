using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FullScreen : MonoBehaviour
{
    Toggle toggle;
    private void Start()
    {
        toggle = FindObjectOfType<Toggle>();
    }


    private void SetUnFullScreen()
    {
        Screen.fullScreen = false;
    }

    private void SetFullScreen()
    {
        Screen.SetResolution(1920, 1080, true);
        Screen.fullScreen = true;
    }

    public void SetScreen()
    {
        if (toggle.isOn)
        {
            Debug.Log("start full screen");
            SetFullScreen();
        }
        else
        {
            Debug.Log("stop full screen");
            SetUnFullScreen();
        }
    }
}
