using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RoomClick : MonoBehaviour, IPointerClickHandler
{
    public delegate void OnClickThisRoom(PointerEventData eventData);
    public static event OnClickThisRoom RoomOnClick;
    public Color originColor = new Color();

    private void OnEnable()
    {
        RoomClick.RoomOnClick += RoomClickHandler;
    }

    private void OnDisable()
    {
        RoomClick.RoomOnClick -= RoomClickHandler;
    }

    private void Awake()
    {
        originColor = this.GetComponent<Image>().color;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        RoomOnClick(eventData);
    }

    private void RoomClickHandler(PointerEventData eventData)
    {
        if (eventData.pointerClick.name != this.name)
        {
            this.GetComponent<Image>().color = originColor;
        }
    }
}
