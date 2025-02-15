using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SpellDisplayUI : MonoBehaviour
{
    [System.Serializable]
    public class SpellSlot
    {
        public Image spellIcon;
        public Text spellNameText;
    }

    public List<SpellSlot> spellSlots;  // List of UI slots to display spells

    private List<Spell> equippedSpells = new List<Spell>();

    // Add a spell to the display
    public void AddSpellToDisplay(Spell spell)
    {
        if (equippedSpells.Count < spellSlots.Count)
        {
            equippedSpells.Add(spell);
            UpdateSpellDisplay();
        }
    }

    private void UpdateSpellDisplay()
    {
        for (int i = 0; i < spellSlots.Count; i++)
        {
            if (i < equippedSpells.Count)
            {
                spellSlots[i].spellIcon.sprite = equippedSpells[i].spellIcon;
                spellSlots[i].spellNameText.text = equippedSpells[i].spellName;
            }
            else
            {
                // Clear the slot if there's no spell
                spellSlots[i].spellIcon.sprite = null;
                spellSlots[i].spellNameText.text = "";
            }
        }
    }
}
