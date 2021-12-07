using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FloatingDamage : MonoBehaviour
{
    //used also in floatingarrow
    void Awake()
    {
        Destroy(gameObject, 1f);
        transform.localPosition += new Vector3(0, 1.5f, 0);
    }
}
