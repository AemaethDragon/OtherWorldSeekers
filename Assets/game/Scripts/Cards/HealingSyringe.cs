using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingSyringe : MonoBehaviour
{
    public int healingAmount;
    public GameObject healingEffect;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            collision.transform.GetComponent<TeamCharacter>().Heal(healingAmount);
            Instantiate(healingEffect, collision.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
