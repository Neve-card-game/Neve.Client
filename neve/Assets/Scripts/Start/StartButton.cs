<<<<<<< HEAD
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    

    public Button button_CardCollection;
    
    public Animator transition;

    public float transitionTime = 1.0f;

    private AsyncOperation load;

    public bool IsSceneLoad = false;

    

    void Awake()
    {
        LoadCardCollection();
        StartCoroutine(buttoncheck());
        
    }


    private void LoadGame()
    {
        transition.SetBool("FadeIn", false);
        transition.SetBool("Fadeout", true);
        StartCoroutine(LoadScene(1));
    }

    private void LoadCardCollection()
    {
        
        StartCoroutine(LoadScene(2));
    }

    IEnumerator LoadScene(int index)
    {
        

        yield return new WaitForSeconds(transitionTime);

        load = SceneManager.LoadSceneAsync(index);

        load.allowSceneActivation = false;

        IsSceneLoad = true;
    }

    private void LoadCollection()
    {
        transition.SetBool("FadeIn", true);
        transition.SetBool("Fadeout", false);
        
        load.allowSceneActivation = true;

        transition.SetBool("FadeIn", false);
        transition.SetBool("Fadeout", true);

    }

    IEnumerator buttoncheck()
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();
            if (IsSceneLoad)
            {
                
                button_CardCollection.onClick.AddListener(LoadCollection);
            }
        }
    }
}
=======
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    

    public Button button_CardCollection;
    
    public Animator transition;

    public float transitionTime = 1.0f;

    private AsyncOperation load;

    public bool IsSceneLoad = false;

    

    void Awake()
    {
        LoadCardCollection();
        StartCoroutine(buttoncheck());
        
    }


    private void LoadGame()
    {
        transition.SetBool("FadeIn", false);
        transition.SetBool("Fadeout", true);
        StartCoroutine(LoadScene(1));
    }

    private void LoadCardCollection()
    {
        
        StartCoroutine(LoadScene(2));
    }

    IEnumerator LoadScene(int index)
    {
        

        yield return new WaitForSeconds(transitionTime);

        load = SceneManager.LoadSceneAsync(index);

        load.allowSceneActivation = false;

        IsSceneLoad = true;
    }

    private void LoadCollection()
    {
        transition.SetBool("FadeIn", true);
        transition.SetBool("Fadeout", false);
        
        load.allowSceneActivation = true;

        transition.SetBool("FadeIn", false);
        transition.SetBool("Fadeout", true);

    }

    IEnumerator buttoncheck()
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();
            if (IsSceneLoad)
            {
                
                button_CardCollection.onClick.AddListener(LoadCollection);
            }
        }
    }
}
>>>>>>> 0803b0379f9681b21ef2903850e2288c306242e9
