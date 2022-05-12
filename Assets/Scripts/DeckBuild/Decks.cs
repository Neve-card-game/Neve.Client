using System.Collections;
using System.Collections.Generic;
[System.Serializable]

public class Decks
{
    public string DeckName;
    public string DeckId;
    public string DeckColor;
    public List<Card> Deck;
    public int CardsConuts;

    public bool IsDeckOnClick = false;
    public int EditTime = 0;

    public Decks() { }
    public Decks(string deckName, string deckColor)
    {
        DeckName = deckName;
        DeckColor = deckColor;
    }
    public Decks(string deckName, string deckId, string deckColor)
    {
        DeckName = deckName;
        DeckId = deckId;
        DeckColor = deckColor;
    }

    public Decks(string deckName, string deckId, string deckColor, List<Card> deck, int cardsConuts, bool isDeckOnClick, int editTime) : this(deckName, deckId, deckColor)
    {
        Deck = deck;
        CardsConuts = cardsConuts;
        IsDeckOnClick = isDeckOnClick;
        EditTime = editTime;
    }

    public int GetCardsConuts()
    {
        this.CardsConuts = this.Deck.Count;
        return this.CardsConuts;
    }

    public void DecksClear()
    {
        DeckName = null;
        DeckId = null;
        DeckColor = null;
        Deck.Clear();
        CardsConuts = 0;
        EditTime = 0;

        IsDeckOnClick = false;
    }
}



