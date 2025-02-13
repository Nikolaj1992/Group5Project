using UnityEngine;

public class ThunderStrike : MonoBehaviour
{
    [SerializeField] private LightningSpawner lightningSpawner;
    private Camera playerCamera;
    
    void Start()
    {
        playerCamera = Camera.main;
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
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit))
        {
            // If the ray hits something, pass the hit point to LightningSpawner
            lightningSpawner.CastLightning(hit.point);
        }
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 1f);
    }
    
}
