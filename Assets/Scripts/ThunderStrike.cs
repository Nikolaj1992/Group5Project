using UnityEngine;

public class ThunderStrike : MonoBehaviour
{
    [SerializeField] private LightningSpawner lightningSpawner;
    private Camera playerCamera;
    
    // public GameObject thunderStrikePrefab;
    // private Camera playerCamera;
    // private GameObject player;
    // private int originalLayer;
    // private int ignoreRaycastLayer;

    // private void Update()
    // {
    //     if (Input.GetMouseButtonDown(0))
    //     {
    //         Vector3 targetPosition = GetMouseWorldPosition();
    //         Debug.Log("Mouse click on position: " + targetPosition);
    //         TriggerThunderStrike(targetPosition);
    //     }
    // }
    //
    // private Vector3 GetMouseWorldPosition()
    // {
    //     Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
    //     RaycastHit hit;
    //
    //     if (Physics.Raycast(ray, out hit))
    //     {
    //         return hit.point;
    //     }
    //
    //     return Vector3.zero;
    // }
    //
    // private void TriggerThunderStrike(Vector3 position)
    // {
    //     Debug.Log("Triggering strike at: " + position);
    //     if (thunderStrikeEffectPrefab != null)
    //     {
    //         GameObject strikeEffect = Instantiate(thunderStrikeEffectPrefab, position, Quaternion.identity);
    //         Debug.Log("Thunder instantiated");
    //         
    //         ParticleSystem ps = strikeEffect.GetComponent<ParticleSystem>();
    //         if (ps != null)
    //         {
    //             ps.Play();
    //             Debug.Log("Lightning system played");
    //         }
    //         
    //         ThunderStrikeEffect strikeScript = strikeEffect.GetComponent<ThunderStrikeEffect>();
    //         if (strikeScript != null)
    //         {
    //             strikeScript.StartStrike();
    //         }
    //     }
    // }
    
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
            Debug.Log("Mouse Clicked");
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
