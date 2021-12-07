using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LosingSceneTransl : MonoBehaviour
{
    public TMP_Text lose;
    public TMP_Text score;
    public TMP_Text restart;
    public TMP_Text main;
    // Start is called before the first frame update
    void Start()
    {
        lose.text = Lang.Fields["lose"];
        score.text = Lang.Fields["score"] + " " + Points.points;
        restart.text = Lang.Fields["restart"];
        main.text = Lang.Fields["main"];
    }

}
