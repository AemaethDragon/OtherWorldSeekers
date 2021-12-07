using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//executa alguns codigos que nao sejam percetiveis em modo de ediçao
[ExecuteInEditMode]
public class LookAtCamera : MonoBehaviour
{

    //aceder a camara
    private Camera cam;

    void Awake()
    {
        //indicar a camara
        cam = Camera.main;
    }

    void Update()
    {

        //rodar canvas para a camara
        transform.eulerAngles = cam.transform.eulerAngles;

    }


}
