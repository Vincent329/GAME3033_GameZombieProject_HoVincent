using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireHazard : MonoBehaviour
{
    [Range(1,10),SerializeField] private float damageInterval;
    [SerializeField] private float damageTimer;
    [SerializeField] private float damageValue;

    private void OnTriggerEnter(Collider other)
    {
        PlayerHealthComponent playerHealth = other.GetComponent<PlayerHealthComponent>();
        if (playerHealth != null)
        {
            damageTimer += Time.deltaTime;
            if (damageTimer > damageInterval)
            {
                playerHealth.GetComponent<IDamageable>()?.TakeDamage(damageValue);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        PlayerHealthComponent playerHealth = other.GetComponent<PlayerHealthComponent>();
        if (playerHealth != null)
        {
            damageTimer = 0;
        }
    }
}
