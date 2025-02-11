using System;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class UIDummyStats : MonoBehaviour
{
    public TextMeshProUGUI uiText;
    public GameObject dummy;
    private ChasePlayer script;

    private void Start()
    {
        script = dummy.GetComponent<ChasePlayer>();
    }

    void FixedUpdate()
    {
        if (script.debuffs.ContainsKey("frozen"))
        {
            uiText.text = "Speed: " + script.speed + "\n" + "Debuff: " + script.debuffs["frozen"].isActive + "\n" + "Time: " + script.debuffs["frozen"].duration;
        }
        else
        {
            uiText.text = "Speed: " + script.speed + "\n" + "Debuff: " + false + "\n" + "Time: " + 0;
        }
    }
}
