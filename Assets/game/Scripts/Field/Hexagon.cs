using UnityEngine;

public class Hexagon : MonoBehaviour
{
    #region Variables
    //Public 
    public FieldManager fieldManager;
    public Vector2 matrixPos;
    public Vector3 worldPos;
    public bool isVisible;

    //Private
    private HexagonCollision _hexagonCollision;
    private HexagonBehaviour _hexagonBehaviour;
    private Renderer _myRenderer;

    #endregion

    private void Start()
    {
        _hexagonBehaviour = GetComponent<HexagonBehaviour>();
        _hexagonCollision = GetComponent<HexagonCollision>();
        _myRenderer = GetComponent<Renderer>();
        _hexagonBehaviour.drawMovement = fieldManager.drawMovementLine;
        _hexagonBehaviour.hexagon = this;
        _hexagonCollision.hexagon = this;
        isVisible = false;
    }

    #region Methods
    //Public

    //Returns obstacle value
    public bool IsFree()
    {
        return _hexagonCollision.free;
    }

    public void SetFree(bool myFree)
    {
        _hexagonCollision.free = myFree;
    }

    /// <summary>
    /// This method turns visibility of the hexagon on and off.
    /// </summary>
    /// <param name="visible">True if visible, False if invisible</param>
    public void IsVisible(bool visible)
    {
        _myRenderer.enabled = visible;
        isVisible = visible;
    }
    #endregion
}
