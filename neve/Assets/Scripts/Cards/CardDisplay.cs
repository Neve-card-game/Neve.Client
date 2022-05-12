<<<<<<< HEAD
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardDisplay : MonoBehaviour
{
    public Sprite cardFace;
    public Sprite cardBack;

    private Image cardImage;
    //private Selectable selectable;
    private CardDataBase cardDatabase;



    void Start()
    {

        cardDatabase = FindObjectOfType<CardDataBase>();
        List<Card> cardList = cardDatabase.LoadCardData();

        int i = 0;

        foreach (Card card in cardList)
        {
            if (this.name == card.Id)
            {
                cardFace = cardDatabase.CardFace[i];

                break;
            }
            i++;
        }
        cardImage = GetComponent<Image>();
    }

    void Update()
    {
        cardImage.sprite = cardFace;
    }


}
=======
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardDisplay : MonoBehaviour
{
    public Sprite cardFace;
    public Sprite cardBack;

    private Image cardImage;
    //private Selectable selectable;
    private CardDataBase cardDatabase;



    void Start()
    {

        cardDatabase = FindObjectOfType<CardDataBase>();
        List<Card> cardList = cardDatabase.LoadCardData();

        int i = 0;

        foreach (Card card in cardList)
        {
            if (this.name == card.Id)
            {
                cardFace = cardDatabase.CardFace[i];

                break;
            }
            i++;
        }
        cardImage = GetComponent<Image>();
    }

    void Update()
    {
        cardImage.sprite = cardFace;
    }


}
>>>>>>> 0803b0379f9681b21ef2903850e2288c306242e9
