using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollisionDamage : MonoBehaviour
{
    [SerializeField] int killCount = 10;

    private int hitCount;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            hitCount++;

            if (hitCount > killCount)
            {
                Destroy(gameObject);
            }
        }
    }
}
