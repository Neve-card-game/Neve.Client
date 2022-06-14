using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UICardEnlarge : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    Canvas canvas;

    public delegate void OnClickThisCard(PointerEventData eventData);
    public static event OnClickThisCard OnClick;
    void Start()
    {
        canvas = FindObjectOfType<Canvas>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        Vector2 Size = this.GetComponent<RectTransform>().sizeDelta;

        GameObject CardEnlarge = new GameObject(this.name + "Max", typeof(Image), typeof(CanvasGroup));

        CardEnlarge.GetComponent<CanvasGroup>().blocksRaycasts = false;

        CardEnlarge.transform.SetParent(GameObject.FindGameObjectWithTag("View Area").transform);

        CardEnlarge.GetComponent<Image>().sprite = this.GetComponent<Image>().sprite;

        if (eventData.position.y >= 660f)
        {

            CardEnlarge.transform.position = new Vector3(this.transform.position.x + 200f * canvas.scaleFactor, this.transform.position.y - 200f * canvas.scaleFactor, this.transform.position.z);
        }
        else if (eventData.position.y < 660f && eventData.position.y >= 300f)
        {

            CardEnlarge.transform.position = new Vector3(this.transform.position.x + 200f * canvas.scaleFactor, this.transform.position.y, this.transform.position.z);
        }
        else if (eventData.position.y < 300f)
        {

            CardEnlarge.transform.position = new Vector3(this.transform.position.x + 200f * canvas.scaleFactor, this.transform.position.y + 225f * canvas.scaleFactor, this.transform.position.z);
        }

        CardEnlarge.GetComponent<RectTransform>().sizeDelta = new Vector2(Size.x * 2 * canvas.scaleFactor, Size.y * 2 * canvas.scaleFactor);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Destroy(GameObject.Find(this.name + "Max"));
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.pointerClick.name == this.name)
        {
            OnClick(eventData);
        }
    }
}
