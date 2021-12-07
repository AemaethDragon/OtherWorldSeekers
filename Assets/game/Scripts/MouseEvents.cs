using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MouseEvents : MonoBehaviour
{
    [SerializeField] private UnityEvent onMouseEnter = new UnityEvent();
    [SerializeField] private UnityEvent onMouseExit = new UnityEvent();
    [SerializeField] private UnityEvent onMouseDown = new UnityEvent();
    [SerializeField] private UnityEvent onMouseOver = new UnityEvent();


    private void OnMouseEnter()
    {
        onMouseEnter?.Invoke();
    }

    private void OnMouseExit()
    {
        onMouseExit?.Invoke();
    }

    private void OnMouseDown()
    {
        onMouseDown?.Invoke();
    }
    private void OnMouseOver()
    {
        onMouseOver?.Invoke();
    }
}
