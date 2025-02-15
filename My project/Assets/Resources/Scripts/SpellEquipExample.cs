using UnityEngine;

public class SpellEquipExample : MonoBehaviour
{
    public SpellDisplayUI spellDisplayUI;
    public Spell FireballSpell;

    void Start()
    {
        // Add the spell to the display
        spellDisplayUI.AddSpellToDisplay(FireballSpell);
    }
}
