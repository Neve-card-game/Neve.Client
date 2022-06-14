using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
public class CardDetail : MonoBehaviour
{
    private GameObject EnlargeCard;
    private CardDataBase cardDatabase;


    void OnEnable()
    {
        UICardEnlarge.OnClick += OnClickHandler;
    }

    void OnDisable()
    {
        UICardEnlarge.OnClick -= OnClickHandler;
    }


    void OnClickHandler(PointerEventData eventData)
    {
        cardDatabase = FindObjectOfType<CardDataBase>();
        List<Card> cardList = cardDatabase.LoadCardData();

        EnlargeCard = eventData.pointerClick;
        GameObject.Find("ImagePosition").GetComponent<Image>().sprite = EnlargeCard.GetComponent<Image>().sprite;
        GameObject.Find("ImagePosition").GetComponent<Image>().color = new Color(255f, 255f, 255f, 255f);

        foreach (Card card in cardList)
        {
            if (card.Id == EnlargeCard.name)
            {
                this.GetComponentInChildren<Text>().text = card.CardName;
                GameObject.FindGameObjectWithTag("detail").GetComponent<Text>().text = "卡牌ID：" + card.Id + "\n" + "卡牌颜色：" + card.CardColor + "\n" + "卡牌类型：" + card.CardType + "\n" + "稀有度：" + card.CardRarity + "\n" + "BP/RP：" + card.BluePoint + "/" + card.RedPoint + "\n" + card.CardDescription;
                break;
            }
        }
    }



}
