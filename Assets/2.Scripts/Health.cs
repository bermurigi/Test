using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Health : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;
    public Slider healthSlider;

    private void Start()
    {
        healthSlider = GetComponentInChildren<Slider>();
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthSlider.value = currentHealth;

        if (currentHealth <= 0)
        {
            Die();
        }
    }


    void Die()
    {
        Destroy(gameObject);
    }
}
