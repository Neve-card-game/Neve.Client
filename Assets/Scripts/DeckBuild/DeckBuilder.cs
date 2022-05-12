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

    public List<Decks> Mydeck = new List<Decks>();

    public Decks CurrentEditDeck = new Decks(null, null);

    public GameObject Decks;
    public GameObject CardList;
    private GameObject DeckList_Layout;

    private VerticalLayoutGroup decklayout;
    public Button AffirmBuildButton;


    public bool IsCardListDispaly = false;
    public bool IsDeckEdit = false;
    public bool IsLayoutChange = false;
    public bool IsDeakRepeatName = false;

    void OnEnable()
    {
        CardEnlage.OnClick += OnClickHandler;
        DeckEditor.DeckOnClick += DeckOnClickHandler;
        DeckEditor.EditOnClick += DeckEdit;
        DeckEditor.DeletOnClick += DeletDeck;
        CardListEditor.CardOnClick += CardListDelet;
    }
    void OnDisable()
    {
        CardEnlage.OnClick -= OnClickHandler;
        DeckEditor.DeckOnClick -= DeckOnClickHandler;
        DeckEditor.EditOnClick -= DeckEdit;
        DeckEditor.DeletOnClick -= DeletDeck;
        CardListEditor.CardOnClick -= CardListDelet;
    }
    private async void Start()
    {
        decklistSl = FindObjectOfType<ServerConnector>();

        DeckList_Layout = GameObject.FindGameObjectWithTag("DeckList");

        decklayout = DeckList_Layout.GetComponent<VerticalLayoutGroup>();

        if (decklistSl.LoadDeckList() != null)
        {
            Player player = await decklistSl.LoadDeckList();
            Mydeck = player.PlayerDecks;
            IsrepeatNameInDeckList();
            DeckSelectReset();
        }

        foreach (var deck in Mydeck)
        {

            DeckListDisplay(deck);
        }
    }

    void DeckSelectReset()
    {
        foreach (var deck in Mydeck)
        {
            deck.IsDeckOnClick = false;
        }
    }
    void IsRepectName(string DeckName)
    {
        bool i = true;

        foreach (var deck in Mydeck)
        {
            if (deck.DeckName == DeckName)
            {
                IsDeakRepeatName = true;
                i = false;
            }
        }
        if (i)
        {
            IsDeakRepeatName = false;
        }
    }
    void IsrepeatNameInDeckList()
    {
        int counts = Mydeck.Count;
        for (int i = 0; i < counts; i++)
        {
            for (int j = i + 1; j < counts; j++)
            {
                if (Mydeck[i].DeckName == Mydeck[j].DeckName)
                {
                    if (Mydeck[i].EditTime >= Mydeck[j].EditTime)
                    {
                        Mydeck.RemoveAt(j);
                        counts = Mydeck.Count;
                        i--;
                        break;

                    }
                    else
                    {
                        Mydeck.RemoveAt(i);
                        counts = Mydeck.Count;
                        i--;
                        break;
                    }
                }
            }
        }

    }
    void DeckListDisplay(Decks mydeck)
    {
        try
        {
            GameObject Deck = Instantiate(Decks);
            Deck.GetComponentInChildren<Text>().text = mydeck.DeckName;
            if (mydeck.IsDeckOnClick == false)
            {
                if (mydeck.DeckColor == "红")
                {
                    Deck.transform.Find("Image").gameObject.GetComponent<Image>().color = new Color(200 / 255f, 35 / 255f, 35 / 255f, 100 / 255f);
                }
                else if (mydeck.DeckColor == "蓝")
                {
                    Deck.transform.Find("Image").gameObject.GetComponent<Image>().color = new Color(35 / 255f, 35 / 255f, 156 / 255f, 100 / 255f);
                }
            }
            else
            {
                if (mydeck.DeckColor == "红")
                {
                    Deck.transform.Find("Image").gameObject.GetComponent<Image>().color = new Color(255 / 255f, 35 / 255f, 35 / 255f, 100 / 255f);

                }
                else if (mydeck.DeckColor == "蓝")
                {
                    Deck.transform.Find("Image").gameObject.GetComponent<Image>().color = new Color(35 / 255f, 35 / 255f, 255 / 255f, 100 / 255f);
                }
            }
            Deck.name = mydeck.DeckName;
            Deck.transform.SetParent(DeckList_Layout.transform);
        }
        catch
        {
            Debug.Log("卡组列表显示错误");
        }
    }

    void SetDeckColor(Decks mydeck)
    {
        GameObject Deck = DeckList_Layout.transform.Find(mydeck.DeckName).gameObject;
        if (mydeck.IsDeckOnClick == false)
        {
            if (mydeck.DeckColor == "红")
            {
                Deck.transform.Find("Image").gameObject.GetComponent<Image>().color = new Color(200 / 255f, 35 / 255f, 35 / 255f, 100 / 255f);
            }
            else if (mydeck.DeckColor == "蓝")
            {
                Deck.transform.Find("Image").gameObject.GetComponent<Image>().color = new Color(35 / 255f, 35 / 255f, 156 / 255f, 100 / 255f);
            }
        }
        else
        {
            if (mydeck.DeckColor == "红")
            {
                Deck.transform.Find("Image").gameObject.GetComponent<Image>().color = new Color(255 / 255f, 35 / 255f, 35 / 255f, 100 / 255f);

            }
            else if (mydeck.DeckColor == "蓝")
            {
                Deck.transform.Find("Image").gameObject.GetComponent<Image>().color = new Color(35 / 255f, 35 / 255f, 255 / 255f, 100 / 255f);
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
        foreach (var deck in Mydeck)
        {
            DeckListDisplay(deck);
        }
    }

    void DeckEdit(string DeckName)
    {
        if (IsDeckEdit == false)
        {
            foreach (var deck in Mydeck)
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

    async void DeletDeck(string DeckName)
    {
        for (int i = 0; i < Mydeck.Count; i++)
        {
            if (Mydeck[i].DeckName == DeckName)
            {
                Mydeck.RemoveAt(i);
                decklistSl.Loginplayer.PlayerDecks = Mydeck;
                if(await decklistSl.SaveDeckList(decklistSl.Loginplayer))
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

    void CardListDisplay(Decks mydeck)
    {
        GameObject.FindGameObjectWithTag("DeckTitle").GetComponent<Text>().text = mydeck.DeckName;

        if (IsCardListDispaly == false)
        {
            foreach (var card in mydeck.Deck)
            {

                GameObject cardindeck = Instantiate(CardList);
                cardindeck.transform.SetParent(DeckList_Layout.transform, true);
                cardindeck.name = card.Id;
                cardindeck.GetComponentInChildren<Text>().text = card.CardName;
            }
            IsCardListDispaly = true;
        }
    }

    void CardListHide(Decks mydeck)
    {
        DeckList_Layout.transform.DestroyChildren();
        GameObject.FindGameObjectWithTag("DeckTitle").GetComponent<Text>().text = "你的卡组";

        IsCardListDispaly = false;
    }

    public void BuildDeck()
    {
        if (IsDeckEdit == false)
        {
            string DeckName = GameObject.FindGameObjectWithTag("DeckName").GetComponent<Text>().text;
            string DeckColor = GameObject.FindGameObjectWithTag("DeckColor").GetComponent<Text>().text;

            IsRepectName(DeckName);

            if (IsDeakRepeatName == false)
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

    public void CardListDelet(PointerEventData eventData)
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
            Decks decks = new Decks(CurrentEditDeck.DeckName, CurrentEditDeck.DeckId, CurrentEditDeck.DeckColor, CurrentEditDeck.Deck, CurrentEditDeck.CardsConuts, CurrentEditDeck.IsDeckOnClick, CurrentEditDeck.EditTime);
            Mydeck.Add(decks);
            IsrepeatNameInDeckList();
            decklistSl.Loginplayer.PlayerDecks = Mydeck;
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
                    Debug.Log(card.CardName);
                    CurrentEditDeck.Deck.Add(card);
                    CardListHide(CurrentEditDeck);
                    CardListDisplay(CurrentEditDeck);
                    break;
                }
            }
        }
    }

    void DeckOnClickHandler(PointerEventData eventData)
    {

        foreach (var deck in Mydeck)
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
