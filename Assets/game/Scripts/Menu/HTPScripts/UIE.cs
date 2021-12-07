using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIE : MonoBehaviour
{
    public TMP_Text cardName;
    public TMP_Text cardDescription;

    public TMP_Text mouseExpl;
    public TMP_Text cameraMov;
    public TMP_Text characterInt;
    public TMP_Text cardExpl;
    public TMP_Text gameExpl;
    

    // Start is called before the first frame update
    void Start()
    {
        cardName.text = Lang.Fields["cardName"];
        cardDescription.text = Lang.Fields["cardDescription"];

        mouseExpl.text = Lang.Fields["mouseExpl"];
        cameraMov.text = Lang.Fields["cameraMov"];
        characterInt.text = Lang.Fields["characterInt"];
        cardExpl.text = Lang.Fields["cardExpl"];
        gameExpl.text = Lang.Fields["gameExpl"];


    }
}
