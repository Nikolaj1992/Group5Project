using UnityEngine;

public class Ability3Frozen : MonoBehaviour
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
            FrozenDebuffAttack();
        }
    }
    
    void FrozenDebuffAttack()
    {
        player.layer = ignoreRaycastLayer;
        Ray ray = m_Camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            ChasePlayer npcScript = hit.transform.gameObject.GetComponent<ChasePlayer>(); //can get other scripts if needed
            Debug.Log(hit.transform.name);
            if (!npcScript) return;
            npcScript.ApplyStatusEffect(StatusEffect.premadeStatusEffects["frozen"]);

        }
        player.layer = originalLayer;
    }
    
}
