using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform FirePoint; 

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Instantiate(prefab, position, rotation)
            Instantiate(projectilePrefab, FirePoint.position, FirePoint.rotation);
        }
    }
}