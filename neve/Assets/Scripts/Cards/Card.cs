<<<<<<< HEAD
﻿using System.Collections;
using System.Collections.Generic;
[System.Serializable]

public class Card
{

    public int BluePoint;
    public int RedPoint;
    public int ChangeBluePoint;
    public int ChangeRedPoint;
    public string Id;
    public string CardName;
    public string CardColor;
    public string CardRarity;
    public string CardType;
    public string CardDescription;

    public bool IsInStack = false;
    public bool IsTop = false;
    public bool IsButtom = false;

    public Card(string id, string cardName, string cardColor, string cardType, string cardRarity, int bluePoint, int redPoint, string cardDescription)
    {
        Id = id;
        CardName = cardName;
        CardColor = cardColor;
        CardType = cardType;
        CardRarity = cardRarity;
        BluePoint = bluePoint;
        RedPoint = redPoint;
        CardDescription = cardDescription;
        ChangeBluePoint = BluePoint;
        ChangeRedPoint = RedPoint;
    }
}
=======
﻿using System.Collections;
using System.Collections.Generic;
[System.Serializable]

public class Card
{

    public int BluePoint;
    public int RedPoint;
    public int ChangeBluePoint;
    public int ChangeRedPoint;
    public string Id;
    public string CardName;
    public string CardColor;
    public string CardRarity;
    public string CardType;
    public string CardDescription;

    public bool IsInStack = false;
    public bool IsTop = false;
    public bool IsButtom = false;

    public Card(string id, string cardName, string cardColor, string cardType, string cardRarity, int bluePoint, int redPoint, string cardDescription)
    {
        Id = id;
        CardName = cardName;
        CardColor = cardColor;
        CardType = cardType;
        CardRarity = cardRarity;
        BluePoint = bluePoint;
        RedPoint = redPoint;
        CardDescription = cardDescription;
        ChangeBluePoint = BluePoint;
        ChangeRedPoint = RedPoint;
    }
}
>>>>>>> 0803b0379f9681b21ef2903850e2288c306242e9
