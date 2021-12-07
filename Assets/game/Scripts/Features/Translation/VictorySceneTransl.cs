using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VictorySceneTransl : MonoBehaviour
{
    public TMP_Text win;
    public TMP_Text score;
    public TMP_Text restart;
    public TMP_Text main;
    // Start is called before the first frame update
    void Start()
    {
        win.text = Lang.Fields["you_win"];
        score.text = Lang.Fields["score"] + " " + Points.points;
        restart.text = Lang.Fields["restart"];
        main.text = Lang.Fields["main"];
    }

   
}
