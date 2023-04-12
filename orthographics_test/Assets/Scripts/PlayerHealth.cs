using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    public HealthBar healthBar;
    public CharacterStats characterStats;

    // Start is called before the first frame update
    void Start()
    {
        characterStats = GetComponent<CharacterStats>();
        currentHealth = characterStats.currentHp; 
        healthBar.SetMaxHealth(characterStats.maxHp);
        healthBar.SetHealth(currentHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))  {
            TakeDamage(20);
            }
    }

    void TakeDamage(int damage)
    {
        characterStats = GetComponent<CharacterStats>();
        characterStats.currentHp -= damage;
        currentHealth = characterStats.currentHp;
        healthBar.SetHealth(currentHealth);
    }
}
