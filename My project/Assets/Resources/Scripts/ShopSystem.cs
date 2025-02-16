using UnityEngine;
using UnityEngine.UI;

public class ShopSystem : MonoBehaviour
{
    public GameObject shopPanel;
    public Button fireballButton;  // Example button for buying/equipping the fireball
    private bool hasFireball = false;

    void Start()
    {
        // Hide the shop panel initially
        shopPanel.SetActive(false);

        // Add a listener for the fireball button
        fireballButton.onClick.AddListener(() => BuyOrEquipSpell("Fireball"));
    }

    void Update()
    {
        // Open/close shop with the "S" key (example keybind)
        if (Input.GetKeyDown(KeyCode.S))
        {
            shopPanel.SetActive(!shopPanel.activeSelf);
        }
    }

    void BuyOrEquipSpell(string spellName)
    {
        if (spellName == "Fireball" && !hasFireball)
        {
            Debug.Log("Bought Fireball!");
            hasFireball = true;
            EquipSpell(spellName, KeyCode.Alpha1);  // Assign to key "1" as default
        }
    }

    void EquipSpell(string spellName, KeyCode keybind)
    {
        Debug.Log(spellName + " equipped with keybind " + keybind.ToString());
        // Store spell in player's spell inventory (to be implemented)
    }
}
