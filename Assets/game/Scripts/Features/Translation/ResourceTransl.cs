using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceTransl : MonoBehaviour
{
    public TMP_Text resource;
    // Start is called before the first frame update
    void Start()
    {
        resource.text = Lang.Fields["resource"];
    }

   
}
