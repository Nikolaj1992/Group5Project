using System.Collections.Generic;
using UnityEngine;

public class StatusEffectHandler : MonoBehaviour
{
    [SerializeField] private float baseSpeed;
    [HideInInspector] public float speed;
    [HideInInspector] public Dictionary<string, StatusEffect> StatusEffects = new Dictionary<string, StatusEffect>();

    void Update()
    {
        List<string> keys = new List<string>(StatusEffects.Keys); // To avoid modifying dictionary while iterating
        
        bool speedModified = false;
        float speedModifier = 1f; // Default multiplier

        foreach (string key in keys)
        {
            StatusEffect statusEffect = StatusEffects[key];

            if (statusEffect.isActive)
            {
                statusEffect.duration -= Time.deltaTime;

                if (statusEffect.duration <= 0)
                {
                    RemoveStatusEffect(key);
                }
                else
                {
                    StatusEffects[key] = statusEffect; // Update the dictionary with the reduced timer
                }

                // If the debuff affects speed, apply it
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
        ApplyStatusEffect(statusEffect.name, statusEffect.duration, statusEffect.stackable, statusEffect.affectsSpeed, statusEffect.speedMultiplier, statusEffect.dealsDamage, statusEffect.damageOverTime, statusEffect.damage, statusEffect.confuses, statusEffect.confusionType);
    }
    
    public void ApplyStatusEffect(string statusEffectName, float duration, bool stackable, bool affectsSpeed = false, float speedMultiplier = 1f, bool dealsDamage = false, bool damageOverTime = false, float damage = 0, bool confuses = false, float confusionType = 0)
    {
        if (StatusEffects.ContainsKey(statusEffectName))
        {
            // If the debuff is already active, refresh its timer instead of stacking it
            StatusEffect existingStatusEffect = StatusEffects[statusEffectName];
            if (existingStatusEffect.stackable)
            {
            existingStatusEffect.duration = Mathf.Max(existingStatusEffect.duration, duration); // Keep the longest duration
            StatusEffects[statusEffectName] = existingStatusEffect; // Update the dictionary
            Debug.Log($"{statusEffectName} refreshed with {existingStatusEffect.duration} seconds remaining.");
            }
            if (existingStatusEffect.duration == 0)
            {
                StatusEffects[statusEffectName] = new StatusEffect(true, duration, stackable, affectsSpeed, speedMultiplier, dealsDamage, damageOverTime, damage, confuses, confusionType);
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
            StatusEffects[statusEffectName] = new StatusEffect(true, duration, stackable, affectsSpeed, speedMultiplier, dealsDamage, damageOverTime, damage, confuses, confusionType);
            Debug.Log($"Applied {statusEffectName} for {duration} seconds.");
        }
    }

    private void RemoveStatusEffect(string statusEffectName)
    {
        if (StatusEffects.ContainsKey(statusEffectName))
        {
            StatusEffects[statusEffectName] = new StatusEffect(false, 0, false);
            Debug.Log($"{statusEffectName} has expired.");
        }
    }
}
