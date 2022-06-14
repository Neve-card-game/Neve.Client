using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MatchManager : MonoBehaviour
{
    [SerializeField]
    public TextAsset cardData;

    private ServerConnector connector;
    public GameObject CardPrefab;
    public GameObject EnemyCardPrefab;
    public GameObject UICardPrefab;
    public GameObject Hand;
    public GameObject Hand_Enemy;
    public GameObject DeckPosition;
    public GameObject BattleFelid;
    public GameObject UICardEnlargePanel;

    public Transform HandTrans;
    public Transform HandTrans_Enemy;

    public Canvas canvas;

    public Button button_Ready;
    public Button TestButton;
    public Button TestButton_MixDeck;
    public Button BackToGame;

    public List<Card> CardList;
    public Sprite[] CardFace;

    public TMPro.TextMeshProUGUI Player_Self;
    public TMPro.TextMeshProUGUI Player_Enemy;

    private string CardName_Enemy;
    private bool IsEnemyDraw = false;

    public static Vector3 OriginalPos = new Vector3(0f, -8.23f, -11.58f);
    public static Vector3 OriginalPos_Enemy = new Vector3(0f, 13f, -3f);
    public static Vector3 CardOffset = new Vector3(4f, 2f, 0.2f);

    public static float RotationAngle = 8f;

    private HandAreaLayout handAreaLayout = new HandAreaLayout(
        OriginalPos,
        CardOffset,
        RotationAngle
    );
    private HandAreaLayout handAreaLayout_Enemy = new HandAreaLayout(
        OriginalPos_Enemy,
        CardOffset,
        RotationAngle
    );

    public List<Card> CardsOfHand = new List<Card>();
    public List<Card> EnemyCardsOfHand = new List<Card>();
    public List<Card> MainDeck = new List<Card>();

    public delegate void DrawCardBroadCast(string cardName);
    public static event DrawCardBroadCast OnDrawCard;

    private void OnEnable()
    {
        CardPlay.CardLeaveHand += (string cardname, CardState cardState) =>
        {
            if (handAreaLayout != null && CardsOfHand != null && Hand != null)
            {
                for (int i = 0; i < CardsOfHand.Count; i++)
                {
                    if (CardsOfHand[i].CardName == cardname)
                    {
                        CardsOfHand.RemoveAt(i);
                        break;
                    }
                }
                handAreaLayout.UpdateCardPosition(CardsOfHand, Hand);
            }
        };

        CardPlay_Enemy.CardLeaveHand_Enemy += (string cardname, CardState cardState) =>
        {
            if (handAreaLayout_Enemy != null && EnemyCardsOfHand != null && Hand_Enemy != null)
            {
                for (int i = 0; i < EnemyCardsOfHand.Count; i++)
                {
                    if (EnemyCardsOfHand[i].CardName == cardname)
                    {
                        EnemyCardsOfHand.RemoveAt(i);
                        break;
                    }
                }
                handAreaLayout_Enemy.UpdateCardPosition_Enemy(EnemyCardsOfHand, Hand_Enemy);
            }
        };

        CardPlay.CardOnClick_BF += UICardEnlarge;
        CardPlay_Enemy.CardOnClick_BF_Enemy += UICardEnlarge;
        SignalRConnector.OnDrawCard += (string cardName) =>
        {
            CardName_Enemy = cardName;
            IsEnemyDraw = true;
        };
    }

    private void OnDisable()
    {
        CardPlay.CardLeaveHand -= (string cardname, CardState cardState) =>
        {
            if (handAreaLayout != null && CardsOfHand != null && Hand != null)
            {
                for (int i = 0; i < CardsOfHand.Count; i++)
                {
                    if (CardsOfHand[i].CardName == cardname)
                    {
                        CardsOfHand.RemoveAt(i);
                        break;
                    }
                }
                handAreaLayout.UpdateCardPosition(CardsOfHand, Hand);
            }
        };

        CardPlay_Enemy.CardLeaveHand_Enemy -= (string cardname, CardState cardState) =>
        {
            if (handAreaLayout_Enemy != null && EnemyCardsOfHand != null && Hand_Enemy != null)
            {
                for (int i = 0; i < EnemyCardsOfHand.Count; i++)
                {
                    if (EnemyCardsOfHand[i].CardName == cardname)
                    {
                        EnemyCardsOfHand.RemoveAt(i);
                        break;
                    }
                }
                handAreaLayout_Enemy.UpdateCardPosition_Enemy(EnemyCardsOfHand, Hand_Enemy);
            }
        };

        CardPlay.CardOnClick_BF -= UICardEnlarge;
        CardPlay_Enemy.CardOnClick_BF_Enemy -= UICardEnlarge;
        SignalRConnector.OnDrawCard -= (string cardName) =>
        {
            CardName_Enemy = cardName;
            IsEnemyDraw = true;
        };
    }

    private void Start()
    {
        HandTrans = Hand.GetComponent<Transform>();
        HandTrans_Enemy = Hand_Enemy.GetComponent<Transform>();
        connector = FindObjectOfType<ServerConnector>();

        CardFace = CardDataBase.LoadCardFace();
        CardList = CardDataBase.LoadCardData(cardData);

        Player_Self.text = connector.Loginplayer.Username;

        button_Ready.onClick.AddListener(() => connector.StartMatch());
        TestButton_MixDeck.onClick.AddListener(() => connector.PreMatch());
        TestButton.onClick.AddListener(DrawCard);

        BackToGame.onClick.AddListener(UIBackToGame);

        StartCoroutine(GetEnemyName());
        StartCoroutine(EnemyDrawCard());
    }

    private void DrawCard()
    {
        int i = 0;
        Card card = new Card();
        GameObject newCard = Instantiate(CardPrefab, HandTrans);
        newCard.name = connector.MixDeck[0].Id;
        OnDrawCard(newCard.name);

        foreach (var cardId in CardList)
        {
            if (newCard.name == cardId.Id)
            {
                newCard.GetComponentInChildren<SpriteRenderer>().sprite = CardFace[i];
                break;
            }
            i++;
        }
        connector.MixDeck.RemoveAt(0);
        connector.RefreshMainDeck();

        card.CardName = newCard.name;
        CardsOfHand.Add(card);
        handAreaLayout.UpdateCardPosition(CardsOfHand, Hand);
        newCard.GetComponent<CardPlay>().HandPosition = newCard.transform.position;
        newCard.GetComponent<CardPlay>().HandRotation = newCard.transform.rotation;
        newCard.transform.position = DeckPosition.transform.position;
        newCard.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        newCard.GetComponent<CardPlay>().cardState = CardState.MoveToHand;
    }

    IEnumerator EnemyDrawCard()
    {
        while (true)
        {
            yield return new WaitUntil(() => IsEnemyDraw);
            IsEnemyDraw = false;
            int i = 0;
            Card card = new Card();
            GameObject newCard = Instantiate(EnemyCardPrefab, HandTrans_Enemy);
            newCard.name = CardName_Enemy;
            newCard.GetComponent<CardPlay_Enemy>().CardName = newCard.name;

            foreach (var cardId in CardList)
            {
                if (newCard.name == cardId.Id)
                {
                    newCard.GetComponentInChildren<SpriteRenderer>().sprite = CardFace[i];
                    break;
                }
                i++;
            }

            card.CardName = newCard.name;
            EnemyCardsOfHand.Add(card);
            handAreaLayout_Enemy.UpdateCardPosition_Enemy(EnemyCardsOfHand, Hand_Enemy);
            newCard.GetComponent<CardPlay_Enemy>().HandPosition = newCard.transform.position;
            newCard.GetComponent<CardPlay_Enemy>().HandRotation = newCard.transform.rotation;
            newCard.transform.position = DeckPosition.transform.position;
            newCard.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            newCard.GetComponent<CardPlay_Enemy>().cardState = CardState.MoveToHand;
        }
    }

    public void UICardEnlarge(string cardname, CardState cardState)
    {
        UICardEnlargePanel.SetActive(true);
        Transform Position = UICardEnlargePanel.transform.Find("Position");
        GameObject CardEnlarge = Instantiate(UICardPrefab, Position);
        CardEnlarge.name = cardname;
        Transform cardOnClick = BattleFelid.transform.Find(cardname);
        Vector3 position = Position.position;
        CardEnlarge.GetComponent<Image>().sprite = cardOnClick
            .GetComponentInChildren<SpriteRenderer>()
            .sprite;
        CardEnlarge.transform.position = new Vector3(position.x, position.y, position.z);
        CardEnlarge.transform.localScale = new Vector3(
            CardEnlarge.transform.localScale.x * 2f,
            CardEnlarge.transform.localScale.y * 2f,
            CardEnlarge.transform.localScale.z
        );
    }

    public void UIBackToGame()
    {
        UICardEnlargePanel.SetActive(false);
        Transform Position = UICardEnlargePanel.transform.Find("Position");
        Position.DestroyChildren();
    }

    //
    IEnumerator GetEnemyName()
    {
        while (true)
        {
            yield return new WaitUntil(() => connector.RefreshRoomPlayerName());
            yield return new WaitForSeconds(0.1f);
            if (connector.EnemyPlayerName == null || connector.EnemyPlayerName.Length == 0)
            {
                Player_Enemy.text = "空位";
            }
            else
            {
                Player_Enemy.text = connector.EnemyPlayerName;
            }
        }
    }
}
