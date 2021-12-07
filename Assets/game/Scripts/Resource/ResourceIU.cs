using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


[ExecuteInEditMode]
public class ResourceIU : MonoBehaviour
{
    #region Variables

    public GameObject popupPanel; //Panel for mouseOver popUp
    public TextMeshProUGUI resourceQuantityText; //Edit canvas text
    //public ResourceSource resource; //ler a quantidade de recursos

    #endregion

    #region Methods

    private void Awake()
    {
        popupPanel.SetActive(false);
    }
    
    public void ActivatePopUp()
    {
        popupPanel.SetActive(true);
    }

    public void DeactivatePopUp()
    {
        popupPanel.SetActive(false);
    }

    //public void OnResourceQuantityChange()
    //{
    //    resourceQuantityText.text = resource.quantity.ToString();

    //}

    #endregion
}
