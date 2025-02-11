using System;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class UIDummyStats : MonoBehaviour
{
    public TextMeshProUGUI uiText;
    public GameObject dummy;
    private StatusEffectHandler script;

    private void Start()
    {
        script = dummy.GetComponent<StatusEffectHandler>();
    }

    void FixedUpdate()
    {
        if (script.StatusEffects.ContainsKey("frozen") && script.StatusEffects["frozen"].isActive)
        {
            uiText.text = "Debuff: " + "frozen" + "\n" + "Speed: " + script.speed + "\n" + "Debuffed: " + script.StatusEffects["frozen"].isActive + "\n" + "Time: " + script.StatusEffects["frozen"].duration;
        }
        else
        {
            uiText.text = "Debuff: " + "none" + "\n" + "Speed: " + script.speed + "\n" + "Debuffed: " + false + "\n" + "Time: " + 0;
        }
    }
}
