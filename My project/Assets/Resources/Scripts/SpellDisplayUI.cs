using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpellDisplayUI : MonoBehaviour
{
    // These lists must be fully assigned in the Inspector
    public List<Spell> playerSpells = new List<Spell>();         // Spell ScriptableObjects (e.g., Fireball)
    public List<GameObject> spellSlots = new List<GameObject>();   // SpellSlot GameObjects (e.g., SpellSlot1, SpellSlot2, etc.)

    void Start()
    {
        try
        {
            DisplaySpells();
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error in DisplaySpells: " + e);
        }
    }

    public void DisplaySpells()
    {
        // Loop through every SpellSlot in the list.
        for (int i = 0; i < spellSlots.Count; i++)
        {
            Debug.Log("Processing SpellSlot index: " + i);
            GameObject slot = spellSlots[i];
            if (slot == null)
            {
                Debug.LogError($"SpellSlot {i} is null! Ensure that all spell slots are assigned in the Inspector.");
                continue;
            }

            // If there's an assigned spell for this slot index, update the slot.
            if (i < playerSpells.Count)
            {
                slot.SetActive(true); // Activate the slot

                // Find the SpellIcon child directly under the slot.
                Transform iconTransform = slot.transform.Find("SpellIcon");
                if (iconTransform == null)
                {
                    Debug.LogError($"SpellSlot {i} is missing a child named 'SpellIcon'.");
                    continue;
                }
                Image icon = iconTransform.GetComponent<Image>();
                if (icon == null)
                {
                    Debug.LogError($"SpellSlot {i} - 'SpellIcon' child does not have an Image component.");
                    continue;
                }

                // Find the SpellNameText child directly under the slot.
                Transform nameTransform = slot.transform.Find("SpellNameText");
                if (nameTransform == null)
                {
                    Debug.LogError($"SpellSlot {i} is missing a child named 'SpellNameText'.");
                    continue;
                }
                TextMeshProUGUI nameText = nameTransform.GetComponent<TextMeshProUGUI>();
                if (nameText == null)
                {
                    Debug.LogError($"SpellSlot {i} - 'SpellNameText' child does not have a TextMeshProUGUI component.");
                    continue;
                }

                // Get the spell from the list.
                Spell spell = playerSpells[i];
                if (spell == null)
                {
                    Debug.LogError($"playerSpells[{i}] is null!");
                    continue;
                }

                // Assign the spell data.
                icon.sprite = spell.spellIcon;
                nameText.text = spell.spellName;
                Debug.Log($"Displaying spell: {spell.spellName} in slot {i}");
            }
            else
            {
                // No spell assigned for this slot: disable it.
                slot.SetActive(false);
                Debug.Log($"Disabling SpellSlot {i} as no spell is assigned.");
            }
        }
    }
}
