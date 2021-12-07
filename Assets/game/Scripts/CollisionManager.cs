using System;
using UnityEngine;
using UnityEngine.Events;

public class CollisionManager : MonoBehaviour
{
    [SerializeField] private UnityEvent onCollisionEnter = new UnityEvent();
    [SerializeField] private UnityEvent onCollisionExit = new UnityEvent();
    [SerializeField] private UnityEvent onCollisionStay = new UnityEvent();

    private void OnCollisionEnter(Collision other)
    {
        onCollisionEnter.Invoke();
    }

    private void OnCollisionExit(Collision other)
    {
        onCollisionExit.Invoke();
    }

    private void OnCollisionStay(Collision other)
    {
        onCollisionStay.Invoke();
    }
}
