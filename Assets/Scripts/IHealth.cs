using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealth
{
    int currentHealth { get; }
    int maxHealth { get; }
    void TakeDamage(int amount);
    void CheckIfDead();
}
