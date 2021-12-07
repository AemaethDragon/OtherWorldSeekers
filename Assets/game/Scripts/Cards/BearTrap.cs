using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearTrap : MonoBehaviour
{

    public Vector2 hexId;
    public Vector3 worldPos;
    public int damage;

    private void Start()
    {
        transform.localScale *= 35;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            Enemy temp = collision.transform.GetComponent<Enemy>();
            temp.StopAllCoroutines();
            List<Vector2> tempRange = Utils.CreateRangeList(temp.gameManager.fieldManager.graph, temp.iTargetable.hexID, temp.iEnemy.range, ListType.ATTACK);
            temp.iTargetable.hexID = hexId;
            collision.transform.position = new Vector3(worldPos.x, collision.transform.position.y, worldPos.z);
            temp.iTargetable.TakeDamage(damage);


            if (tempRange.Contains(temp.iEnemy.currentTarget.hexID))
            {
                temp.iEnemy.currentTarget.Heal(temp.iEnemy.power);
            }

            Destroy(this.gameObject);
        }
    }

}
