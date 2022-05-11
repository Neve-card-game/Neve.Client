using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public AnimationCurve showCurve;
    public AnimationCurve hideCurve;

    public float animatioSpeed;

    private bool IsPanelShow;

    private void Start()
    {
        IsPanelShow = false;
    }



    IEnumerator ShowPanel(GameObject gameObject)
    {
        float timer = 0f;
        while (timer <= 1)
        {
            gameObject.transform.localScale = Vector3.one * showCurve.Evaluate(timer);
            timer += Time.deltaTime * animatioSpeed;
            yield return null;
        }
    }

    IEnumerator HidePanel(GameObject gameObject)
    {
        float timer = 0f;
        while (timer <= 1)
        {
            gameObject.transform.localScale = Vector3.one * hideCurve.Evaluate(timer);
            timer += Time.deltaTime * animatioSpeed;
            yield return null;

        }
    }

    public void Show()
    {
        if (!IsPanelShow)
        {
            StartCoroutine(ShowPanel(this.gameObject));
            IsPanelShow = true;
        }
    }

    public void Hide(bool IsRepeatName)
    {
        if (IsPanelShow && IsRepeatName == false)
        {
            StartCoroutine(HidePanel(this.gameObject));
            IsPanelShow = false;
        }

    }
}



