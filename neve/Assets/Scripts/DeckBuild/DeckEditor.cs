using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DeckEditor : MonoBehaviour, IPointerClickHandler
{
    public delegate void OnClickThisDeck(PointerEventData eventData);
    public static event OnClickThisDeck DeckOnClick;

    public delegate void OnClickDeckEdit(string DeckName);
    public static event OnClickDeckEdit EditOnClick;

    public delegate void OnClickDeckDelet(string DeckName);
    public static event OnClickDeckDelet DeletOnClick;

    public DeckBuilder deckBuilder;


    private void Start()
    {
        deckBuilder = FindObjectOfType<DeckBuilder>();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        DeckOnClick(eventData);
    }

    public void EditButtonOnClick()
    {
        EditOnClick(this.name);
    }

    public void DeletButtonOnClick()
    {
        DeletOnClick(this.name);
    }

}
