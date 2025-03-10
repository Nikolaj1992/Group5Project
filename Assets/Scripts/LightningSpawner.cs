using System;
using UnityEngine;

public class LightningSpawner : MonoBehaviour
{
    [SerializeField] private GameObject thunderStrikePrefab;
    [SerializeField] private Camera playerCamera;

    private int originalLayer;
    private int ignoreRaycastLayer;
    private GameObject player;

    private void Start()
    {
        playerCamera = Camera.main;
        player = GameObject.FindWithTag("Player");
        originalLayer = gameObject.layer;
        ignoreRaycastLayer = LayerMask.NameToLayer("Ignore Raycast");
    }

    public void CastLightning(string statusEffectName, float damage, HealthHandler.DamageType damageType)
    {
        player.layer = ignoreRaycastLayer;
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit))
        {
            // If the ray hits something, get the hit position and spawn the lightning on that point
            Vector3 lightningPosition = hit.point;
            Debug.Log(hit.point);

            // Instantiate the lightning
            Instantiate(thunderStrikePrefab, lightningPosition, player.transform.rotation);
        }
        
        HealthHandler npcHealth = hit.transform.gameObject.GetComponent<HealthHandler>();
        StatusEffectHandler npcScript = hit.transform.gameObject.GetComponent<StatusEffectHandler>(); //can get other scripts if needed
        ThunderStrikeEffect strikeScript = thunderStrikePrefab.GetComponent<ThunderStrikeEffect>();
        Debug.Log(hit.transform.name);
        if (!npcHealth) return;
        if (!npcScript) return;
        if (statusEffectName != "" && !StatusEffect.premadeStatusEffects.ContainsKey(statusEffectName)) return;
        if (strikeScript != null)
        { 
            strikeScript.StartStrike();
        }
        npcHealth.DealDamage(damage, damageType);
        npcScript.ApplyStatusEffect(StatusEffect.premadeStatusEffects[statusEffectName]);

        player.layer = originalLayer;
    }
    public void CastLightning(Vector3 position, GameObject target, string statusEffectName, float damage, HealthHandler.DamageType damageType)
    {
        // player.layer = ignoreRaycastLayer;
        // Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        // RaycastHit hit;
        //
        // if (Physics.Raycast(ray, out hit))
        // {
        //     // If the ray hits something, get the hit position and spawn the lightning on that point
        //     Vector3 lightningPosition = hit.point;
        //     Debug.Log(hit.point);
        //
        //     // Instantiate the lightning
        //     Instantiate(thunderStrikePrefab, lightningPosition, player.transform.rotation);
        //     Instantiate(thunderStrikePrefab, lightningPosition, player.transform.rotation);
        // }
        
        HealthHandler npcHealth = target.transform.gameObject.GetComponent<HealthHandler>();
        StatusEffectHandler npcScript = target.transform.gameObject.GetComponent<StatusEffectHandler>(); //can get other scripts if needed
        ThunderStrikeEffect strikeScript = thunderStrikePrefab.GetComponent<ThunderStrikeEffect>();
        Debug.Log(target.transform.name);
        Debug.Log(target.transform.position);
        Debug.Log(position);
        if (!npcHealth) return;
        if (!npcScript) return;
        if (statusEffectName != "" && !StatusEffect.premadeStatusEffects.ContainsKey(statusEffectName)) return;
        position.y += thunderStrikePrefab.transform.localScale.y + 2.5f; // 2.5 is a manuel adjustment
        Instantiate(thunderStrikePrefab, position, player.transform.rotation); // rotation doesn't seem to matter
        if (strikeScript != null)
        { 
            strikeScript.StartStrike();
        }
        npcHealth.DealDamage(damage, damageType);
        npcScript.ApplyStatusEffect(StatusEffect.premadeStatusEffects[statusEffectName]);

        // player.layer = originalLayer;
    }
}
