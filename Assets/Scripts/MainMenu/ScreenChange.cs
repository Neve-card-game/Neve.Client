using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenChange : MonoBehaviour
{
    Dropdown dropdown;

    private void Start()
    {
        dropdown = GetComponent<Dropdown>();
    }

    public void ScreenSet()
    {
        if (string.Equals(dropdown.captionText.text, "1920*1080"))
        {
            Screen.SetResolution(1920, 1080, Screen.fullScreen);
            Debug.Log("Resolution changed to" + dropdown.captionText.text);
        }
        else if (string.Equals(dropdown.captionText.text, "1280*720"))
        {
            Screen.SetResolution(1280, 720, Screen.fullScreen);
            Debug.Log("Resolution changed to" + dropdown.captionText.text);
        }
        else if (string.Equals(dropdown.captionText.text, "800*600"))
        {
            Screen.SetResolution(800, 600, Screen.fullScreen);
            Debug.Log("Resolution changed to" + dropdown.captionText.text);
        }
    }
}
