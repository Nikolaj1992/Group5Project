using UnityEngine;

public class ThunderStrike : MonoBehaviour
{
    [SerializeField] private LightningSpawner lightningSpawner;
    private Camera playerCamera;
    private int originalLayer;
    private int ignoreRaycastLayer;
    private GameObject player;
    
    void Start()
    {
        playerCamera = Camera.main;
        player = GameObject.FindWithTag("Player");
        originalLayer = gameObject.layer;
        ignoreRaycastLayer = LayerMask.NameToLayer("Ignore Raycast");
    }
    
    private void Update()
    {
        // Left mouse click
        if (Input.GetMouseButtonDown(0))
        {
            CastLightning();
        }
    }

    void CastLightning()
    {
        player.layer = ignoreRaycastLayer;
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit))
        {
            // If the ray hits something, pass the hit point to LightningSpawner
            lightningSpawner.CastLightning("aflame", 25, HealthHandler.DamageType.Piercing);
        }
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 1f);
        player.layer = originalLayer;
    }
    
}
