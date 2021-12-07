using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarponParticle : MonoBehaviour
{

    LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (!(SelectionManager.SelectedPlayer.transform.position.x == SelectionManager.SelectedHexagon.transform.position.x && SelectionManager.SelectedPlayer.transform.position.z == SelectionManager.SelectedHexagon.transform.position.z))
        {
            StartGrapple();
        }
        else
        {
            StopGrapple();
        }
    }
    private void LateUpdate()
    {
        DrawLine();
    }

    private void DrawLine()
    {
        lineRenderer.SetPosition(0, SelectionManager.SelectedPlayer.transform.position);
        lineRenderer.SetPosition(1, SelectionManager.SelectedHexagon.transform.position);
    }
    public void StartGrapple()
    {
        lineRenderer.positionCount = 2;
    }
    public void StopGrapple()
    {
        lineRenderer.positionCount = 0;
    }
}
