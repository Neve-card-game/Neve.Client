using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScenes : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1.0f;
    public AsyncOperation async;
    public int SceneIndex;

    void Start()
    {
        LoadSceneAsync();
    }

    public void LoadSceneAsync()
    {
        async = SceneManager.LoadSceneAsync(SceneIndex);
        async.allowSceneActivation = false;
    }

    public void Load(int Index)
    {
        transition.SetBool("FadeIn", false);
        transition.SetBool("Fadeout", true);
        StartCoroutine(LoadScene(Index));
    }

    IEnumerator LoadScene(int Index)
    {
        yield return new WaitForSeconds(transitionTime);
        if(Index == SceneIndex){
            async.allowSceneActivation = true;
        }
        else{
            SceneManager.LoadScene(Index);
        }
    }
}
