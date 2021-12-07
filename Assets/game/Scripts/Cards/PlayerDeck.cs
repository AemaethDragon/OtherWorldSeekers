using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class PlayerDeck : MonoBehaviour
{
    #region Variables

    public List<Card> deck;
    public List<Card> hand;
    
    private List<GameObject> showingCards = new List<GameObject>();

    public GameObject SelectedCardCardView;
    public GameObject cardsPanel;
    public GameObject cloneCard;


    private int maxNumberCardsInHandStartGame;
    private int maxNumberCardsinHamdDuringGame;

    #endregion

    #region Methods

    private void Start()
    {
        SelectedCardCardView = FindObjectOfType<BigCard>().gameObject;
        cardsPanel = FindObjectOfType<CardViewUI>().gameObject;
        hand = new List<Card>();
        deck = new List<Card>();
        maxNumberCardsinHamdDuringGame = 4;
        maxNumberCardsInHandStartGame = 2;
        AddCardsToDeck();
        Shuffle();
        AddCardsStartOfGame();

    }

    private void AddCardsToDeck()
    {
        foreach (Card card in CardsDatabase.cardList)
        {
            if (card.playerType != transform.GetComponent<TeamCharacter>().aSquad) continue;
            deck.Add(card);
            deck.Add(card);
        }
    }

    /// <summary>
    /// Method to shuffle the deck
    /// </summary>
    private void Shuffle()
    {
        for (int i = 0; i < deck.Count; i++)
        {
            var tempCard = deck[i];
            int randomIndex = Random.Range(i, deck.Count);
            deck[i] = deck[randomIndex];
            deck[randomIndex] = tempCard;
        }
    }

    /// <summary>
    /// Coroutine that adds the cards in the beginning of the game
    /// </summary>
    /// <returns>waiting of 0.1 seconds</returns>
    private void AddCardsStartOfGame()
    {
        for (int i = 0; i < maxNumberCardsInHandStartGame; i++)
        {
            FindObjectOfType<AudioManager>().PlayAudio(("cardSwipe"));
            hand.Add(deck[i]);
            deck.RemoveAt(0);
        }
    }

    /// <summary>
    /// Add a card to the player view in the beginning of the turn
    /// </summary>
    public void AddCardNextTurn()
    {
        if (hand.Count < maxNumberCardsinHamdDuringGame)
        {
            if (cardsPanel != null)
            {
                FindObjectOfType<AudioManager>().PlayAudio(("cardSwipe"));
                hand.Add(deck[0]);
            }
            deck.RemoveAt(0);
        }
    }

    /// <summary>
    /// Method that is used when you click on the card to transport to the big view
    /// </summary>
    
    
    public void AddSelectePlayerCardsToTheView()
    {
        DestroyCardsFromTheView();
        AddHandCardsToGameCoroutine();
    }

    public void DestroyCardsFromTheView()
    {
        for (int i = cardsPanel.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(cardsPanel.transform.GetChild(i).gameObject);
        }
    }

    private void AddHandCardsToGameCoroutine()
    {
        for (int i = 0; i < hand.Count; i++)
        {
            GameObject addCloneCard = (GameObject)Instantiate(cloneCard, cardsPanel.transform, false);
            addCloneCard.GetComponent<CardView>().thisCard = hand[i];
        }
    }

    #endregion
}
