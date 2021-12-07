using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardView : MonoBehaviour
{
    #region Variables
    //Public
    public Card thisCard;
    public Text nameText;
    public Text costText;
    public TMP_Text descriptionText;
    public Image image;
   
    #endregion

    #region Methods
    private void Start()
    {
        nameText.text = "" + thisCard.cardName;
        costText.text = "" + thisCard.cost;
        descriptionText.text = "" + thisCard.cardDescription;
        image.sprite = thisCard.thisImage;
    }
    #endregion
}
