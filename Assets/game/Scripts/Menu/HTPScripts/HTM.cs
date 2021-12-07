using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HTM : MonoBehaviour
{
    public TMP_Text pathfindExpl;
    public TMP_Text movementrangeExpl;
    public TMP_Text goToExpl;
    public TMP_Text playerSpeedExpl;

    // Start is called before the first frame update
    void Start()
    {
        pathfindExpl.text = Lang.Fields["pathfindExpl"];
        movementrangeExpl.text = Lang.Fields["movementrangeExpl"];
        goToExpl.text = Lang.Fields["goToExpl"];
        playerSpeedExpl.text = Lang.Fields["playerSpeedExpl"];


    }
}
