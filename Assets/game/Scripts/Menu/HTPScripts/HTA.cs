using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HTA : MonoBehaviour
{
    public TMP_Text buttonExpl;
    public TMP_Text rangeExpl;
    public TMP_Text attackExpl;
    // Start is called before the first frame update
    void Start()
    {
        buttonExpl.text = Lang.Fields["buttonExpl"];
        rangeExpl.text = Lang.Fields["rangeExpl"];
        attackExpl.text = Lang.Fields["attackExpl"];

    }

}
