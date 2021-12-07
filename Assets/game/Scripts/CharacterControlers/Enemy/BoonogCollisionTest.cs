using UnityEngine;

public class BoonogCollisionTest : MonoBehaviour
{
    private Boonog _boonog;

    private void Awake()
    { 
        _boonog = GetComponentInParent<Boonog>();
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.transform.CompareTag("Spawn"))
        {
            Nest temp = other.gameObject.GetComponent<Nest>();
            _boonog.spawn = temp;
            temp.boonog = _boonog;
            Destroy(gameObject);
        }
    }
}
