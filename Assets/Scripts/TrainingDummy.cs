using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TrainingDummy : MonoBehaviour, IHealth
{
    public int currentHealth { get; private set; }
    public int maxHealth { get; private set; }

    public TMP_Text lastDamageText;
    public TMP_Text healthText;
    void Start()
    {
        maxHealth = 1000;
        currentHealth = maxHealth;

        healthText.text = "Health: " + currentHealth;
    }
    private void OnTriggerEnter(Collider other)
    {
        IDamageDealer dealer = other.GetComponent<IDamageDealer>();
        if (dealer != null)
        {
            int damage = dealer.Damage;
            Debug.Log("attacked");
            HitStop.instance.Stop(.1f);
            lastDamageText.text = "Last Hit: " + damage;
            TakeDamage(damage);
            healthText.text = "Health: " + currentHealth;
        }
    }
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        CheckIfDead();
    }
    public void CheckIfDead()
    {
        if (currentHealth <= 0)
        {
            //make death logic
            Debug.Log("Im dead");

        }
    }
}
