using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private Slider healthSlider; // Reference to the UI slider for health

    [SerializeField] GameObject lose;

    private float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();

        lose.SetActive(false);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth); // Ensure health doesn't go below 0 or above maxHealth
        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Handle player death, such as respawning
        //Debug.Log("Player died!");

        lose.SetActive(true);
        Cursor.lockState = CursorLockMode.None; // Unlock the cursor
        Cursor.visible = true;                  // Make the cursor visible
    }

    void UpdateHealthUI()
    {
        // Update the UI slider to reflect the current health
        if (healthSlider != null)
            healthSlider.value = currentHealth / maxHealth;
    }
}
