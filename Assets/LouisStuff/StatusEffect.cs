using System.Collections.Generic;
using UnityEngine;

public struct StatusEffect
{
    public string name;
    public float duration;
    public bool stackable;
    // speed boost/debuff fields
    public bool affectsSpeed;
    public float speedMultiplier; // set to 1f if you don't want a speed multiplier
    // damage debuff fields
    public bool dealsDamage;
    public HealthHandler.DamageType damageType;
    public bool damageOverTime;
    public float damage;
    // confusion debuff fields
    public bool confuses;
    public float confusionType; // 1 = input scramble, 2 = messes with gui, 3 = disables gui
    
    // map with premade status effects
    public static Dictionary<string, StatusEffect> premadeStatusEffects = new Dictionary<string, StatusEffect>();

    static StatusEffect()
    {
        premadeStatusEffects["frozen"] = new StatusEffect("frozen", 5, false, true, 0.7f);
        premadeStatusEffects["aflame"] = new StatusEffect("aflame", 6, true, false, 1f, true, HealthHandler.DamageType.Elemental, true, 4);
    }
    
    public StatusEffect(string name, float duration, bool stackable, bool affectsSpeed = false, float speedMultiplier = 1f, bool dealsDamage = false, HealthHandler.DamageType damageType = HealthHandler.DamageType.Impact,bool damageOverTime = false, float damage = 0, bool confuses = false, float confusionType = 0)
    {
        this.name = name;
        this.duration = duration;
        this.stackable = stackable;
        this.affectsSpeed = affectsSpeed;
        this.speedMultiplier = speedMultiplier;
        this.dealsDamage = dealsDamage;
        this.damageType = damageType;
        this.damageOverTime = damageOverTime;
        this.damage = damage;
        this.confuses = confuses;
        this.confusionType = confusionType;
    }
}
