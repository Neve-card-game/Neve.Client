using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;

public class DeckBuilder : MonoBehaviour
{
    private ServerConnector decklistSl;
    private CardDataBase CardData;

    public List<Decks> MyDeck = new List<Decks>();

    public Decks CurrentEditDeck = new Decks(null, null);

    public GameObject Decks;
    public GameObject CardList;
    private GameObject DeckList_Layout;

    private VerticalLayoutGroup decklayout;
    public Button AffirmBuildButton;

    public bool IsCardListDispaly = false;
    public bool IsDeckEdit = false;
    public bool IsLayoutChange = false;
    public bool IsDeckRepeatName = false;

    void OnEnable()
    {
        CardEnlage.OnClick += OnClickHandler;
        DeckEditor.DeckOnClick += DeckOnClickHandler;
        DeckEditor.EditOnClick += DeckEdit;
        DeckEditor.DeleteOnClick += DeleteDeck;
        CardListEditor.CardOnClick += CardListDelete;
    }

    void OnDisable()
    {
        CardEnlage.OnClick -= OnClickHandler;
        DeckEditor.DeckOnClick -= DeckOnClickHandler;
        DeckEditor.EditOnClick -= DeckEdit;
        DeckEditor.DeleteOnClick -= DeleteDeck;
        CardListEditor.CardOnClick -= CardListDelete;
    }

    private async void Start()
    {
        decklistSl = FindObjectOfType<ServerConnector>();

        DeckList_Layout = GameObject.FindGameObjectWithTag("DeckList");

        decklayout = DeckList_Layout.GetComponent<VerticalLayoutGroup>();

        if (decklistSl.LoadDeckList() != null)
        {
            Player player = await decklistSl.LoadDeckList();
            MyDeck = player.PlayerDecks;
            IsRepeatNameInDeckList();
            DeckSelectReset();
        }

        foreach (var deck in MyDeck)
        {
            DeckListDisplay(deck);
        }
    }

    void DeckSelectReset()
    {
        foreach (var deck in MyDeck)
        {
            deck.IsDeckOnClick = false;
        }
    }

    void IsRepeatName(string DeckName)
    {
        bool i = true;

        foreach (var deck in MyDeck)
        {
            if (deck.DeckName == DeckName)
            {
                IsDeckRepeatName = true;
                i = false;
            }
        }
        if (i)
        {
            IsDeckRepeatName = false;
        }
    }

    void IsRepeatNameInDeckList()
    {
        int counts = MyDeck.Count;
        for (int i = 0; i < counts; i++)
        {
            for (int j = i + 1; j < counts; j++)
            {
                if (MyDeck[i].DeckName == MyDeck[j].DeckName)
                {
                    if (MyDeck[i].EditTime >= MyDeck[j].EditTime)
                    {
                        MyDeck.RemoveAt(j);
                        counts = MyDeck.Count;
                        i--;
                        break;
                    }
                    else
                    {
                        MyDeck.RemoveAt(i);
                        counts = MyDeck.Count;
                        i--;
                        break;
                    }
                }
            }
        }
    }

    void DeckListDisplay(Decks MyDeck)
    {
        try
        {
            GameObject Deck = Instantiate(Decks);
            Deck.GetComponentInChildren<Text>().text = MyDeck.DeckName;
            if (MyDeck.IsDeckOnClick == false)
            {
                if (MyDeck.DeckColor == "红")
                {
                    Deck.transform.Find("Image").gameObject.GetComponent<Image>().color = new Color(
                        200 / 255f,
                        35 / 255f,
                        35 / 255f,
                        100 / 255f
                    );
                }
                else if (MyDeck.DeckColor == "蓝")
                {
                    Deck.transform.Find("Image").gameObject.GetComponent<Image>().color = new Color(
                        35 / 255f,
                        35 / 255f,
                        156 / 255f,
                        100 / 255f
                    );
                }
            }
            else
            {
                if (MyDeck.DeckColor == "红")
                {
                    Deck.transform.Find("Image").gameObject.GetComponent<Image>().color = new Color(
                        255 / 255f,
                        35 / 255f,
                        35 / 255f,
                        100 / 255f
                    );
                }
                else if (MyDeck.DeckColor == "蓝")
                {
                    Deck.transform.Find("Image").gameObject.GetComponent<Image>().color = new Color(
                        35 / 255f,
                        35 / 255f,
                        255 / 255f,
                        100 / 255f
                    );
                }
            }
            Deck.name = MyDeck.DeckName;
            Deck.transform.SetParent(DeckList_Layout.transform);
        }
        catch
        {
            Debug.Log("卡组列表显示错误");
        }
    }

    void SetDeckColor(Decks MyDeck)
    {
        GameObject Deck = DeckList_Layout.transform.Find(MyDeck.DeckName).gameObject;
        if (MyDeck.IsDeckOnClick == false)
        {
            if (MyDeck.DeckColor == "红")
            {
                Deck.transform.Find("Image").gameObject.GetComponent<Image>().color = new Color(
                    200 / 255f,
                    35 / 255f,
                    35 / 255f,
                    100 / 255f
                );
            }
            else if (MyDeck.DeckColor == "蓝")
            {
                Deck.transform.Find("Image").gameObject.GetComponent<Image>().color = new Color(
                    35 / 255f,
                    35 / 255f,
                    156 / 255f,
                    100 / 255f
                );
            }
        }
        else
        {
            if (MyDeck.DeckColor == "红")
            {
                Deck.transform.Find("Image").gameObject.GetComponent<Image>().color = new Color(
                    255 / 255f,
                    35 / 255f,
                    35 / 255f,
                    100 / 255f
                );
            }
            else if (MyDeck.DeckColor == "蓝")
            {
                Deck.transform.Find("Image").gameObject.GetComponent<Image>().color = new Color(
                    35 / 255f,
                    35 / 255f,
                    255 / 255f,
                    100 / 255f
                );
            }
        }
    }

    void DeckListHide()
    {
        DeckList_Layout.transform.DestroyChildren();
    }

    void DeckListRefresh()
    {
        DeckListHide();
        foreach (var deck in MyDeck)
        {
            DeckListDisplay(deck);
        }
    }

    void DeckEdit(string DeckName)
    {
        if (IsDeckEdit == false)
        {
            foreach (var deck in MyDeck)
            {
                if (DeckName == deck.DeckName)
                {
                    IsDeckEdit = true;
                    deck.IsDeckOnClick = false;
                    CurrentEditDeck = deck;
                    CurrentEditDeck.EditTime++;
                    DeckListHide();
                    CardListDisplay(CurrentEditDeck);
                    break;
                }
            }
        }
    }

    async void DeleteDeck(string DeckName)
    {
        for (int i = 0; i < MyDeck.Count; i++)
        {
            if (MyDeck[i].DeckName == DeckName)
            {
                MyDeck.RemoveAt(i);
                decklistSl.Loginplayer.PlayerDecks = MyDeck;
                if (await decklistSl.SaveDeckList(decklistSl.Loginplayer))
                {
                    Debug.Log("储存成功");
                }
                else
                {
                    Debug.Log("储存失败");
                }
                DeckListRefresh();
                break;
            }
        }
    }

    void CardListDisplay(Decks MyDeck)
    {
        GameObject.FindGameObjectWithTag("DeckTitle").GetComponent<Text>().text = MyDeck.DeckName;

        if (IsCardListDispaly == false)
        {
            foreach (var card in MyDeck.Deck)
            {
                GameObject cardindeck = Instantiate(CardList);
                cardindeck.transform.SetParent(DeckList_Layout.transform, true);
                cardindeck.name = card.Id;
                cardindeck.GetComponentInChildren<Text>().text = card.CardName;
            }
            IsCardListDispaly = true;
        }
    }

    void CardListHide(Decks MyDeck)
    {
        DeckList_Layout.transform.DestroyChildren();
        GameObject.FindGameObjectWithTag("DeckTitle").GetComponent<Text>().text = "你的卡组";

        IsCardListDispaly = false;
    }

    public void BuildDeck()
    {
        if (IsDeckEdit == false)
        {
            string DeckName = GameObject
                .FindGameObjectWithTag("DeckName")
                .GetComponent<Text>()
                .text;
            string DeckColor = GameObject
                .FindGameObjectWithTag("DeckColor")
                .GetComponent<Text>()
                .text;

            IsRepeatName(DeckName);

            if (IsDeckRepeatName == false)
            {
                CurrentEditDeck.DecksClear();
                CurrentEditDeck.DeckName = DeckName;
                CurrentEditDeck.DeckColor = DeckColor;

                DeckListHide();
                CardListDisplay(CurrentEditDeck);

                IsDeckEdit = true;
            }
            else
            {
                Debug.Log("生成卡组失败");
            }
        }
    }

    public void CardListDelete(PointerEventData eventData)
    {
        foreach (var card in CurrentEditDeck.Deck)
        {
            if (eventData.pointerClick.name == card.Id)
            {
                CurrentEditDeck.Deck.Remove(card);
                CardListHide(CurrentEditDeck);
                CardListDisplay(CurrentEditDeck);
                break;
            }
        }
    }

    public async void DeckSave()
    {
        if (IsDeckEdit == true)
        {
            CardListHide(CurrentEditDeck);
            Decks decks = new Decks(
                CurrentEditDeck.DeckName,
                CurrentEditDeck.DeckId,
                CurrentEditDeck.DeckColor,
                CurrentEditDeck.Deck,
                CurrentEditDeck.CardsConuts,
                CurrentEditDeck.IsDeckOnClick,
                CurrentEditDeck.EditTime
            );
            MyDeck.Add(decks);
            IsRepeatNameInDeckList();
            decklistSl.Loginplayer.PlayerDecks = MyDeck;
            if (await decklistSl.SaveDeckList(decklistSl.Loginplayer))
            {
                Debug.Log("储存成功");
            }
            else
            {
                Debug.Log("储存失败");
            }
            IsDeckEdit = false;
            DeckListRefresh();
        }
    }

    void OnClickHandler(PointerEventData eventData)
    {
        CardData = FindObjectOfType<CardDataBase>();
        List<Card> cardList = CardData.LoadCardData();
        if (IsDeckEdit == true)
        {
            foreach (var card in cardList)
            {
                if (card.Id == eventData.pointerClick.name)
                {
                    Debug.Log(card.CardName + card.CardColor);
                    if (card.CardColor == CurrentEditDeck.DeckColor)
                    {
                        CurrentEditDeck.Deck.Add(card);
                        CardListHide(CurrentEditDeck);
                        CardListDisplay(CurrentEditDeck);
                        break;
                    }
                    else
                    {
                        Debug.Log("卡牌必须和卡组同色！");
                    }
                }
            }
        }
    }

    void DeckOnClickHandler(PointerEventData eventData)
    {
        foreach (var deck in MyDeck)
        {
            if (deck.DeckName == eventData.pointerClick.name)
            {
                if (deck.IsDeckOnClick == false)
                {
                    deck.IsDeckOnClick = true;
                    SetDeckColor(deck);
                    GameObject ThisDeck = DeckList_Layout.transform.Find(deck.DeckName).gameObject;
                    if (IsLayoutChange == false)
                    {
                        //decklayout.spacing = new Vector2(decklayout.spacing.x, decklayout.spacing.y + 56f);
                        IsLayoutChange = true;
                    }
                    ThisDeck.GetComponent<Animator>().SetBool("deckdisplay", true);
                    ThisDeck.GetComponent<Animator>().SetBool("deckhide", false);
                    break;
                }
                else
                {
                    deck.IsDeckOnClick = false;
                    SetDeckColor(deck);
                    GameObject ThisDeck = DeckList_Layout.transform.Find(deck.DeckName).gameObject;

                    ThisDeck.GetComponent<Animator>().SetBool("deckdisplay", false);
                    ThisDeck.GetComponent<Animator>().SetBool("deckhide", true);

                    if (IsLayoutChange == true)
                    {
                        //decklayout.spacing = new Vector2(decklayout.spacing.x, decklayout.spacing.y - 56f);
                        IsLayoutChange = false;
                    }

                    break;
                }
            }
        }
    }
}
