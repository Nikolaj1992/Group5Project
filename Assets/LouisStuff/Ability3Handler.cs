using UnityEngine;

public class Ability3Handler : MonoBehaviour
{
    private Camera m_Camera;
    private GameObject player;
    private int originalLayer; // Store original layer
    private int ignoreRaycastLayer; // Layer to ignore raycast

    void Start()
    {
        m_Camera = Camera.main;
        player = GameObject.FindWithTag("Player");
        originalLayer = gameObject.layer; 
        ignoreRaycastLayer = LayerMask.NameToLayer("Ignore Raycast"); // Get IgnoreRaycast layer index
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        { 
            Ability3Attack("frozen", 20, HealthHandler.DamageType.Elemental);
        }
        if (Input.GetKeyDown(KeyCode.R))
        { 
            Ability3Attack("aflame", 30, HealthHandler.DamageType.Elemental);
        }
        
    }

    void Ability3Attack(string statusEffectName, float damage, HealthHandler.DamageType damageType)
    {
        player.layer = ignoreRaycastLayer;
        Ray ray = m_Camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            HealthHandler npcHealth = hit.transform.gameObject.GetComponent<HealthHandler>();
            StatusEffectHandler npcScript = hit.transform.gameObject.GetComponent<StatusEffectHandler>(); //can get other scripts if needed
            Debug.Log(hit.transform.name);
            if (!npcHealth) return;
            if (!npcScript) return;
            if (statusEffectName != "" && !StatusEffect.premadeStatusEffects.ContainsKey(statusEffectName)) return;
            npcHealth.dealDamage(damage, damageType);
            npcScript.ApplyStatusEffect(StatusEffect.premadeStatusEffects[statusEffectName]);
        }
        player.layer = originalLayer;
    }
}
