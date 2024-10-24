using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BaddiePig : MonoBehaviour
{
    [SerializeField] private float MaxHealth = 3f;
    [SerializeField] private float damageTreshhold = 0.2f;
    [SerializeField] private GameObject baddyParticleEffect;


    private float currentHealth;

    private void Awake()
    {
        currentHealth = MaxHealth;
    }

    public void DamageBaddie(float damageAmound)

    {
        currentHealth -= damageAmound;

        if(currentHealth <= 0)
        {

            Instantiate(baddyParticleEffect, transform.position, quaternion.identity);

            Die();

        }
    }

    private void Die()
    {
        GameManager.Instance.RemoveBaddies(this);
        Destroy(gameObject);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        float impactvelocity = collision.relativeVelocity.magnitude;

        if(impactvelocity > damageTreshhold) 
        {
         
        DamageBaddie(impactvelocity);

        }
    }
}
