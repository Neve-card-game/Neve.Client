using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardDataBase : MonoBehaviour
{
    public TextAsset cardData;
    public List<Card> cardList = new List<Card>();
    public Sprite[] CardFace;
    public GameObject cardPrefab;
    public GameObject showArea;

    public Button SearchButton;

    public TMP_InputField SearchInfo;


    private void Start()
    {
        CardFace = LoadCardFace();
        cardList = LoadCardData();

        CardCollectionDisplay(cardList);

        SearchButton.onClick.AddListener(RefreshCollection);
    }

    public List<Card> LoadCardData()
    {
        List<Card> cardList = new List<Card>();
        string[] dataRow = cardData.text.Split('\n');
        foreach (var row in dataRow)
        {
            string[] rowArray = row.Split(',');
            if (!(rowArray[0] == "#"))
            {
                int bluePoint;
                int redPoint;
                string id = rowArray[1];
                string cardName = rowArray[2];
                string cardColor = rowArray[3];
                string cardType = rowArray[4];
                string cardRarity = rowArray[5];
                try
                {
                    bluePoint = int.Parse(rowArray[6]);
                    redPoint = int.Parse(rowArray[7]);
                }
                catch
                {
                    bluePoint = -1;
                    redPoint = -1;
                }
                string cardDescription = rowArray[8];
                Card card = new Card(
                    id,
                    cardName,
                    cardColor,
                    cardType,
                    cardRarity,
                    bluePoint,
                    redPoint,
                    cardDescription
                );

                cardList.Add(card);

                //Debug.Log("读取到：" + id);

            }
        }

        return cardList;
    }

    public static List<Card> LoadCardData(TextAsset CardData)
    {
        List<Card> cardList = new List<Card>();
        string[] dataRow = CardData.text.Split('\n');
        foreach (var row in dataRow)
        {
            string[] rowArray = row.Split(',');
            if (!(rowArray[0] == "#"))
            {
                int bluePoint;
                int redPoint;
                string id = rowArray[1];
                string cardName = rowArray[2];
                string cardColor = rowArray[3];
                string cardType = rowArray[4];
                string cardRarity = rowArray[5];
                try
                {
                    bluePoint = int.Parse(rowArray[6]);
                    redPoint = int.Parse(rowArray[7]);
                }
                catch
                {
                    bluePoint = -1;
                    redPoint = -1;
                }
                string cardDescription = rowArray[8];
                Card card = new Card(
                    id,
                    cardName,
                    cardColor,
                    cardType,
                    cardRarity,
                    bluePoint,
                    redPoint,
                    cardDescription
                );

                cardList.Add(card);

                //Debug.Log("读取到：" + id);

            }
        }

        return cardList;
    }

    public static Sprite[] LoadCardFace()
    {
        Sprite[] CardFaceA = Resources.LoadAll<Sprite>("2");
        Sprite[] CardFaceB = Resources.LoadAll<Sprite>("3");
        Sprite[] CardFaceC = Resources.LoadAll<Sprite>("4");

        Sprite[] ReturnCard = new Sprite[206];
        Sprite[] newReturnCard = new Sprite[206];

        Array.Copy(CardFaceA, 1, ReturnCard, 0, CardFaceA.Length - 1);
        Array.Copy(CardFaceB, 0, ReturnCard, 68, CardFaceB.Length - 1);
        Array.Copy(CardFaceC, 0, ReturnCard, 137, CardFaceC.Length - 1);

        Array.Copy(ReturnCard, 186, newReturnCard, 0, 20);
        Array.Copy(ReturnCard, 20, newReturnCard, 20, 83);
        Array.Copy(ReturnCard, 0, newReturnCard, 103, 10);
        Array.Copy(ReturnCard, 103, newReturnCard, 113, 83);
        Array.Copy(ReturnCard, 10, newReturnCard, 196, 10);

        return newReturnCard;
    }

    void CardCollectionDisplay(List<Card> cardList)
    {
        foreach (Card card in cardList)
        {
            GameObject newCard = Instantiate(cardPrefab);
            newCard.transform.SetParent(showArea.transform);
            newCard.name = card.Id;
        }
    }

    private void CardSearch()
    {
        string _searchInfo = SearchInfo.text;
        List<Card> SearchCardList = new List<Card>();
        foreach (Card card in cardList)
        {
            if (
                card.CardName.Contains(_searchInfo)
                || card.CardType.Contains(_searchInfo)
                || card.Id.Contains(_searchInfo)
                || card.CardColor.Contains(_searchInfo)
                || card.CardDescription.Contains(_searchInfo)
            )
            {
                SearchCardList.Add(card);
            }
        }
        CardCollectionDisplay(SearchCardList);
    }

    private void RefreshCollection()
    {
        showArea.transform.DestroyChildren();
        CardSearch();
    }
}
