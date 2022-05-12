<<<<<<< HEAD
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FULLScreen : MonoBehaviour
{
    Toggle toggle;
    private void Start()
    {
         toggle= FindObjectOfType<Toggle>();
    }

   
   private void SetUnFullScreen()
    {
        Screen.fullScreen = false;
    }

    private  void SetFullScreen()
    {
        Screen.SetResolution(1920, 1080, true);
        Screen.fullScreen = true;
    }

    public void SetScreen()
    {
        if (toggle.isOn)
        {
            Debug.Log("开启全屏");
            SetFullScreen();
        }
        else
        {
            Debug.Log("关闭全屏");
            SetUnFullScreen();
        }
    }
}
=======
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FULLScreen : MonoBehaviour
{
    Toggle toggle;
    private void Start()
    {
         toggle= FindObjectOfType<Toggle>();
    }

   
   private void SetUnFullScreen()
    {
        Screen.fullScreen = false;
    }

    private  void SetFullScreen()
    {
        Screen.SetResolution(1920, 1080, true);
        Screen.fullScreen = true;
    }

    public void SetScreen()
    {
        if (toggle.isOn)
        {
            Debug.Log("开启全屏");
            SetFullScreen();
        }
        else
        {
            Debug.Log("关闭全屏");
            SetUnFullScreen();
        }
    }
}
>>>>>>> 0803b0379f9681b21ef2903850e2288c306242e9
