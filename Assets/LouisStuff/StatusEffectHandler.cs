using System;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffectHandler : MonoBehaviour
{
    [SerializeField] private float baseSpeed;
    [HideInInspector] public float speed;
    [HideInInspector] public Dictionary<string, StatusEffect> statusEffects = new Dictionary<string, StatusEffect>();
    private HealthHandler healthHandler;

    void Awake()
    {
        healthHandler = gameObject.GetComponent<HealthHandler>();
    }

    void Update()
    {
        if (!healthHandler.alive) statusEffects.Clear();
        
        List<string> keys = new List<string>(statusEffects.Keys); // To avoid modifying dictionary while iterating
        
        bool speedModified = false;
        float speedModifier = 1f; // Default multiplier

        foreach (string key in keys)
        {
            StatusEffect statusEffect = statusEffects[key];

            if (statusEffect.isActive)
            {
                statusEffect.duration -= Time.deltaTime;

                if (statusEffect.duration <= 0)
                {
                    RemoveStatusEffect(key);
                }
                else
                {
                    statusEffects[key] = statusEffect; // Update the dictionary with the reduced timer
                }
                
                if (statusEffect.affectsSpeed)
                {
                    speedModified = true;
                    speedModifier *= statusEffect.speedMultiplier;
                }
            }
        }
        speed = speedModified ? baseSpeed * speedModifier : baseSpeed;
    }
    
    public void ApplyStatusEffect(StatusEffect statusEffect)
    {
        if (!healthHandler.alive) return;
        ApplyStatusEffect(statusEffect.name, statusEffect.duration, statusEffect.stackable, statusEffect.affectsSpeed, statusEffect.speedMultiplier, statusEffect.dealsDamage, statusEffect.damageType, statusEffect.damageOverTime, statusEffect.damage, statusEffect.confuses, statusEffect.confusionType);
    }
    
    public void ApplyStatusEffect(string statusEffectName, float duration, bool stackable, bool affectsSpeed = false, float speedMultiplier = 1f, bool dealsDamage = false, HealthHandler.DamageType damageType = HealthHandler.DamageType.Impact, bool damageOverTime = false, float damage = 0, bool confuses = false, float confusionType = 0)
    {
        if (!healthHandler.alive) return;
        if (statusEffects.ContainsKey(statusEffectName))
        {
            // If the debuff is already active, refresh its timer instead of stacking it
            StatusEffect existingStatusEffect = statusEffects[statusEffectName];
            if (existingStatusEffect.stackable)
            {
            existingStatusEffect.duration = Mathf.Max(existingStatusEffect.duration, duration); // Keep the longest duration
            statusEffects[statusEffectName] = existingStatusEffect; // Update the dictionary
            Debug.Log($"{statusEffectName} refreshed with {existingStatusEffect.duration} seconds remaining.");
            }
            if (existingStatusEffect.duration == 0)
            {
                statusEffects[statusEffectName] = new StatusEffect(statusEffectName, true, duration, stackable, affectsSpeed, speedMultiplier, dealsDamage, damageType, damageOverTime, damage, confuses, confusionType);
                if (dealsDamage)
                {
                    if (damageOverTime)
                    {
                        healthHandler.dealDamageOverTime(damage, damageType, duration);
                    }
                }
                Debug.Log($"Applied {statusEffectName} for {duration} seconds. (already existed)");
            }
            else
            {
                Debug.Log($"Status effect {statusEffectName} is already applied, and is not stackable.");
            }
        }
        else
        {
            // Otherwise, add a new debuff
            statusEffects[statusEffectName] = new StatusEffect(statusEffectName, true, duration, stackable, affectsSpeed, speedMultiplier, dealsDamage, damageType, damageOverTime, damage, confuses, confusionType);
            if (dealsDamage)
            {
                if (damageOverTime)
                {
                    healthHandler.dealDamageOverTime(damage, damageType, duration);
                }
            }
            Debug.Log($"Applied {statusEffectName} for {duration} seconds.");
        }
    }

    private void RemoveStatusEffect(string statusEffectName)
    {
        if (statusEffects.ContainsKey(statusEffectName))
        {
            statusEffects.Remove(statusEffectName);
            Debug.Log($"{statusEffectName} has expired.");
        }
    }
}
