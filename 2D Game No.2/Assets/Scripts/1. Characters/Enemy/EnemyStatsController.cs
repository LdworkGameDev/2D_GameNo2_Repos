using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyStatsController : MonoBehaviour
{
    public UnityEvent OnAttackedEvent;
    public UnityEvent OnDeathEvent;

    public EnemyStats_SO charStats_SOTemplate;
    public EnemyStats_SO charStats;

    private void Start()
    {
        if (charStats_SOTemplate != null)
        {
            charStats = Instantiate(charStats_SOTemplate);
        }
    }

    public void InscreaseHealth(int amount)
    {
        charStats.currentHealth = (charStats.currentHealth + amount > charStats.maxHealth) ? charStats.maxHealth : charStats.currentHealth + amount;
    }

    public void DescreaseHealth(int amount)
    {
        charStats.currentHealth = (charStats.currentHealth - amount < 0) ? 0 : charStats.currentHealth - amount;

        if (OnAttackedEvent != null)
        {
            OnAttackedEvent.Invoke();
        }

        if (charStats.currentHealth <= 0 && OnDeathEvent != null)
        {
            OnDeathEvent.Invoke();
        }
    }

    public int GetCurrentHealth()
    {
        return charStats.currentHealth;
    }

    public int GetMaxHealth()
    {
        return charStats.maxHealth;
    }

    public int GetCurrentDamage()
    {
        return charStats.baseDamage;
    }
}
