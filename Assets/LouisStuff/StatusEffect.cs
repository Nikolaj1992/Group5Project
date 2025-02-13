using System.Collections.Generic;
using UnityEngine;

public struct StatusEffect
{
    public string name;
    public bool isActive;
    public float duration;
    public bool stackable;
    // speed boost/debuff fields
    public bool affectsSpeed;
    public float speedMultiplier;
    // damage debuff fields
    public bool dealsDamage;
    public bool damageOverTime;
    public float damage;
    // confusion debuff fields
    public bool confuses;
    public float confusionType; // 1 = input scramble, 2 = messes with gui, 3 = disables gui
    
    
    public static Dictionary<string, StatusEffect> premadeStatusEffects = new Dictionary<string, StatusEffect>();

    static StatusEffect()
    {
        premadeStatusEffects["frozen"] = new StatusEffect("frozen", true, 5, false, true, 0.7f);
    }
    
    public StatusEffect(bool isActive, float duration, bool stackable, bool affectsSpeed = false, float speedMultiplier = 1f, bool dealsDamage = false, bool damageOverTime = false, float damage = 0, bool confuses = false, float confusionType = 0)
    {
        this.name = string.Empty;
        this.isActive = isActive;
        this.duration = duration;
        this.stackable = stackable;
        this.affectsSpeed = affectsSpeed;
        this.speedMultiplier = speedMultiplier;
        this.dealsDamage = dealsDamage;
        this.damageOverTime = damageOverTime;
        this.damage = damage;
        this.confuses = confuses;
        this.confusionType = confusionType;
    }

    private StatusEffect(string name, bool isActive, float duration, bool stackable, bool affectsSpeed = false, float speedMultiplier = 1f, bool dealsDamage = false, bool damageOverTime = false, float damage = 0, bool confuses = false, float confusionType = 0)
    {
        this.name = name;
        this.isActive = isActive;
        this.duration = duration;
        this.stackable = stackable;
        this.affectsSpeed = affectsSpeed;
        this.speedMultiplier = speedMultiplier;
        this.dealsDamage = dealsDamage;
        this.damageOverTime = damageOverTime;
        this.damage = damage;
        this.confuses = confuses;
        this.confusionType = confusionType;
    }
}
