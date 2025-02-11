using System.Collections.Generic;
using UnityEngine;

public struct Debuff
{
    public string name;
    public bool isActive;
    public float duration;
    public bool stackable;
    public bool affectsSpeed;
    public float speedMultiplier;
    
    public static Dictionary<string, Debuff> premadeDebuffs = new Dictionary<string, Debuff>();

    static Debuff()
    {
        premadeDebuffs["frozen"] = new Debuff("frozen", true, 5, false, true, 0.7f);
    }
    
    public Debuff(bool isActive, float duration, bool stackable, bool affectsSpeed, float speedMultiplier)
    {
        this.name = string.Empty;
        this.isActive = isActive;
        this.duration = duration;
        this.stackable = stackable;
        this.affectsSpeed = affectsSpeed;
        this.speedMultiplier = speedMultiplier;
    }

    private Debuff(string name, bool isActive, float duration, bool stackable, bool affectsSpeed, float speedMultiplier)
    {
        this.name = name;
        this.isActive = isActive;
        this.duration = duration;
        this.stackable = stackable;
        this.affectsSpeed = affectsSpeed;
        this.speedMultiplier = speedMultiplier;
    }
}
