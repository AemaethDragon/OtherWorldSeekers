using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager
{
    private static GameObject lastMouseOneClick;

    public static GameObject ReturnMouseOver()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit rayHit;

        if (Physics.Raycast(ray, out rayHit))
        {
            return rayHit.transform.gameObject;
        }
        return null;
    }

    public static GameObject ReturnMouseOne()
    {
        return lastMouseOneClick;
    }

    public static GameObject ReturnMouseTwo()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            return ReturnMouseOver();
        }
        return null;
    }

    public static bool MouseOnePressed()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            lastMouseOneClick = ReturnMouseOver();
            return true;
        }
        return false;
    }

    public static bool MouseTwoPressed()
    {
        if (Input.GetButtonDown("Fire2")) return true;
        return false;
    }
}
