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

    public void CastLightning(Vector3 position)
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
            Instantiate(thunderStrikePrefab, lightningPosition, Quaternion.identity);
        }
        
        ThunderStrikeEffect strikeScript = thunderStrikePrefab.GetComponent<ThunderStrikeEffect>();
        if (strikeScript != null)
        {
            strikeScript.StartStrike();
        }

        player.layer = originalLayer;
    }
}
