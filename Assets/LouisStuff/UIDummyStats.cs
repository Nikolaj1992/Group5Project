using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class UIDummyStats : MonoBehaviour
{
    public TextMeshProUGUI uiText;
    public GameObject dummy;
    private StatusEffectHandler script;
    private HealthHandler script2;

    private void Start()
    {
        script = dummy.GetComponent<StatusEffectHandler>();
        script2 = dummy.GetComponent<HealthHandler>();
    }

    void FixedUpdate()
    {
        if (script.statusEffects.Count > 0)
        {
            uiText.text = "Health: " + script2.health + "\n" + "Speed: " + script.speed + "\n" + "Debuff: " + script.statusEffects.First().Value.name + "\n" + "Time: " + script.statusEffects.First().Value.duration;
        }
        else
        {
            uiText.text = "Health: " + script2.health + "\n" + "Speed: " + script.speed + "\n" + "Debuff: " + "none" + "\n" + "Time: " + 0;
        }
    }
}
