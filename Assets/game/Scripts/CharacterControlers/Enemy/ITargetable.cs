using UnityEngine;

public interface ITargetable
{
    Vector2 hexID { get; set; }
    int currentHealth { get; set; }
    int maxHealth { get; set; }

    void TakeDamage(int damage);

    Vector3 ReturnPos();
}
