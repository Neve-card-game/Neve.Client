using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    public Button button_CardCollection;
    public Button button_MatchUp;
    public Animator transition;

    private ServerConnector connector;

    public float transitionTime = 1.0f;

    void Start()
    {
        try
        {
            connector = FindObjectOfType<ServerConnector>();
            button_MatchUp.onClick.AddListener(LoadGame);
            button_CardCollection.onClick.AddListener(LoadCardCollection);
        }
        catch { }
    }

    private void LoadGame()
    {
        transition.SetBool("FadeIn", false);
        transition.SetBool("Fadeout", true);
        StartCoroutine(LoadScene(4));
    }

    private void LoadCardCollection()
    {
        transition.SetBool("FadeIn", false);
        transition.SetBool("Fadeout", true);
        StartCoroutine(LoadScene(2));
    }

    public void BackToMainMenu()
    {
        connector.LeftRoom();
        transition.SetBool("FadeIn", false);
        transition.SetBool("Fadeout", true);
        StartCoroutine(LoadScene(3));
    }

    public void ExitRoom(){
        connector.LeftRoom();
        transition.SetBool("FadeIn", false);
        transition.SetBool("Fadeout", true);
        StartCoroutine(LoadScene(4));
    }
    IEnumerator LoadScene(int index)
    {
        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadSceneAsync(index);

        transition.SetBool("FadeIn", false);
        transition.SetBool("Fadeout", true);
    }
}
