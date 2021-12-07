using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Card
{
    #region Variables

    public GameManager gameManager;
    public int id;
    public string cardName;
    public int cost;
    public int aoeRange;
    public int range;
    public int effectValue;
    public string cardDescription;
    public CardType cardType;
    public TheASquad playerType;
    
    public Sprite thisImage;
    #endregion

    #region Constructor

    /// <summary>
    /// Generate a card 
    /// </summary>
    /// <param name="Id">Id of the Card</param>
    /// <param name="CardName">Cards Name</param>
    /// <param name="Cost">Cost of the card</param>
    /// <param name="Range">Range of the card</param>
    /// <param name="EffectValue">The value of the eefect of a card EX: 5 Damage</param>
    /// <param name="AoeRange">AOe Range if The card has one if not put 0</param>
    /// <param name="CardDescription">Description of what the card does</param>
    /// <param name="ThisImage">Image of the card</param>
    /// <param name="CardType">Tipe of function that card its</param>
    /// <param name="PlayerType">The player tha use this card</param>
    public Card(int Id,string CardName, int Cost, int Range, int EffectValue,int AoeRange, string CardDescription, Sprite ThisImage,CardType CardType,TheASquad PlayerType,GameManager GameManager)
    {
        id = Id;
        cardName = CardName;
        cost = Cost;
        range = Range;
        effectValue = EffectValue;
        aoeRange = AoeRange;
        cardDescription = CardDescription;
        thisImage = ThisImage;
        cardType = CardType;
        playerType = PlayerType;
        gameManager = GameManager;
    }
    

    #endregion
}
