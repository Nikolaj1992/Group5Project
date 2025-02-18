using System;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    [SerializeField] private float speed; // is set in inspector
    private GameObject targetToIgnore;
    private Rigidbody rb;
    void Awake()
    {
        Destroy(gameObject, 3);
        rb = gameObject.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (rb.linearVelocity != Vector3.zero)  // Ensure there's movement
        {
            transform.rotation = Quaternion.LookRotation(rb.linearVelocity);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
            Destroy(gameObject);
    }

    public void IgnoreCollisionWith(GameObject other)
    {
        targetToIgnore = other;
        Physics.IgnoreCollision(targetToIgnore.gameObject.GetComponent<Collider>(), transform.GetComponent<Collider>());
    }
}
