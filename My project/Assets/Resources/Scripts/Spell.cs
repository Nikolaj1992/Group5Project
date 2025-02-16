using UnityEngine;

[CreateAssetMenu(fileName = "New Spell", menuName = "Spell")]
public class Spell : ScriptableObject
{
    public string spellName;      // Name of the spell
    public Sprite spellIcon;      // Icon to display in the UI
    public float cooldownTime;    // Cooldown time for the spell
    public GameObject spellPrefab;    // Reference to the spell prefab
}
