using UnityEngine;

public class CardBehaviour : MonoBehaviour
{
    #region Variables
    public GameObject floatingNoEnergyText;
    
    //Private
    private Transform _transform;
    private CardView _cardView;


    #endregion

    #region Methods

    private void Awake()
    {
        _transform = transform;
        _cardView = GetComponent<CardView>();
    }

    /// <summary>
    /// Used to transform the card in scale and position when entering it
    /// </summary>
    public void MouseEnter()
    {
        if (Utils.cardUseAbility != null) return;
        if (transform.parent.name == "BigCard") return;
        _transform.position += new Vector3(0f, 0f, 0.5f);
        _transform.localScale *= 2.5f;
    }

    /// <summary>
    /// Used to transform the card in scale and position when exiting it
    /// </summary>
    public void MouseExit()
    {
        if (Utils.cardUseAbility != null) return;
        if (transform.parent.name == "BigCard") return;
        _transform.position -= new Vector3(0f, 0f, 0.5f);
        _transform.localScale /= 2.5f;
    }

    /// <summary>
    /// Used to add a card to the replaceCard and destroying the object
    /// </summary>
    public void MouseDown()
    {
        if (Utils.cardUseAbility != null) return;
        if (Utils.cardInUse != null) return;
        Utils.cardInUse = _cardView.thisCard;
        Utils.cardUseAbility = _cardView.thisCard;
        if (_cardView.thisCard.cost <= SelectionManager.SelectedPlayer.energy)
        {
            _cardView.thisCard.gameManager.abilityManager.DoAbility(_cardView.thisCard.cardType, _cardView.thisCard);
            _transform.SetParent(SelectionManager.SelectedPlayer.myPlayerDeck.SelectedCardCardView.transform);
            SelectionManager.SelectedPlayer.energy -= _cardView.thisCard.cost;
            ChangeDeckCards(_cardView.thisCard);
        }
        else
        {
            Instantiate(floatingNoEnergyText, SelectionManager.SelectedPlayer.transform.position, SelectionManager.SelectedPlayer.transform.rotation);
            Utils.cardInUse = null;
            Utils.cardUseAbility = null;
        }
        
    }

    private void ChangeDeckCards(Card card)
    {
        SelectionManager.SelectedPlayer.myPlayerDeck.hand.Remove(card);
        SelectionManager.SelectedPlayer.myPlayerDeck.deck.Add(card);
    }

    #endregion
}
