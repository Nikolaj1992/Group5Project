using System.Collections.Generic;
using UnityEngine;

public class SpellManager : MonoBehaviour
{
    public Dictionary<KeyCode, string> equippedSpells = new Dictionary<KeyCode, string>();

    void Update()
    {
        // Listen for spell key presses
        foreach (KeyValuePair<KeyCode, string> spell in equippedSpells)
        {
            if (Input.GetKeyDown(spell.Key))
            {
                CastSpell(spell.Value);
            }
        }
    }

    public void EquipSpell(string spellName, KeyCode keybind)
    {
        if (equippedSpells.ContainsKey(keybind))
        {
            Debug.Log("Keybind already in use!");
            return;
        }

        equippedSpells[keybind] = spellName;
        Debug.Log(spellName + " equipped to " + keybind.ToString());
    }

    void CastSpell(string spellName)
    {
        Debug.Log("Casting " + spellName);
        // Here you would instantiate and cast the spell
    }
}
