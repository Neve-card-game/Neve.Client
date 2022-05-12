using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardEnlage : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
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

        GameObject CardInlarge = new GameObject(this.name + "Max", typeof(Image), typeof(CanvasGroup));

        CardInlarge.GetComponent<CanvasGroup>().blocksRaycasts = false;

        CardInlarge.transform.SetParent(GameObject.FindGameObjectWithTag("View Area").transform);

        CardInlarge.GetComponent<Image>().sprite = this.GetComponent<Image>().sprite;

        if (eventData.position.y >= 660f)
        {

            CardInlarge.transform.position = new Vector3(this.transform.position.x + 200f * canvas.scaleFactor, this.transform.position.y - 200f * canvas.scaleFactor, this.transform.position.z);
        }
        else if (eventData.position.y < 660f && eventData.position.y >= 300f)
        {

            CardInlarge.transform.position = new Vector3(this.transform.position.x + 200f * canvas.scaleFactor, this.transform.position.y, this.transform.position.z);
        }
        else if (eventData.position.y < 300f)
        {

            CardInlarge.transform.position = new Vector3(this.transform.position.x + 200f * canvas.scaleFactor, this.transform.position.y + 225f * canvas.scaleFactor, this.transform.position.z);
        }

        CardInlarge.GetComponent<RectTransform>().sizeDelta = new Vector2(Size.x * 2 * canvas.scaleFactor, Size.y * 2 * canvas.scaleFactor);
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
