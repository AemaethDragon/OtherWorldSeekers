using UnityEngine;

public class EnemyMouseBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject _mouseOverSelection;

    public void MouseEnter()
    {
        _mouseOverSelection.SetActive(true);
    }

    public void MouseExit()
    {
        _mouseOverSelection.SetActive(false);
    }
    
}
