using System.Collections.Generic;
using UnityEngine;

public class SpellCooldownManager : MonoBehaviour
{
    private Dictionary<string, float> spellCooldowns = new Dictionary<string, float>();
    private Dictionary<string, float> spellTimers = new Dictionary<string, float>();

    public void SetCooldown(string spellName, float cooldownTime)
    {
        spellCooldowns[spellName] = cooldownTime;
    }

    void Update()
    {
        // Decrease timers for spells on cooldown
        foreach (var spell in new List<string>(spellTimers.Keys))
        {
            spellTimers[spell] -= Time.deltaTime;
            if (spellTimers[spell] <= 0)
            {
                Debug.Log(spell + " is ready!");
                spellTimers.Remove(spell);
            }
        }
    }

    public bool CanCastSpell(string spellName)
    {
        return !spellTimers.ContainsKey(spellName);
    }

    public void StartCooldown(string spellName)
    {
        if (spellCooldowns.ContainsKey(spellName))
        {
            spellTimers[spellName] = spellCooldowns[spellName];
        }
    }

    // Helper method to check if a spell is on cooldown and output its remaining time.
    public bool IsSpellOnCooldown(string spellName, out float remainingCooldown)
    {
        if (spellTimers.ContainsKey(spellName))
        {
            remainingCooldown = spellTimers[spellName];
            return true;
        }
        remainingCooldown = 0f;
        return false;
    }
}
