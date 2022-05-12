<<<<<<< HEAD
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class BackMenu : MonoBehaviour
{
    public Button button_BackMenu;

    public Animator transition;

    public float transitionTime = 1.0f;

    

    void Start()
    {
        button_BackMenu.onClick.AddListener(BacktoMenu);
    }

    private void BacktoMenu()
    {
        transition.SetBool("FadeIn", false);
        transition.SetBool("Fadeout", true);
        StartCoroutine(LoadScene(3));
    }

    IEnumerator LoadScene(int index)
    {
        transition.SetBool("FadeIn", true);
        transition.SetBool("Fadeout", false);

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadSceneAsync(index);
        

    }
}
=======
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class BackMenu : MonoBehaviour
{
    public Button button_BackMenu;

    public Animator transition;

    public float transitionTime = 1.0f;

    

    void Start()
    {
        button_BackMenu.onClick.AddListener(BacktoMenu);
    }

    private void BacktoMenu()
    {
        transition.SetBool("FadeIn", false);
        transition.SetBool("Fadeout", true);
        StartCoroutine(LoadScene(3));
    }

    IEnumerator LoadScene(int index)
    {
        transition.SetBool("FadeIn", true);
        transition.SetBool("Fadeout", false);

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadSceneAsync(index);
        

    }
}
>>>>>>> 0803b0379f9681b21ef2903850e2288c306242e9
