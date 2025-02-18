using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy1AnimationHandler : MonoBehaviour
{
    private Animator animator;
    
    public enum directionEnum { none, forward, backward, left, right };

    [HideInInspector] public GameObject handMuzzle;
    [SerializeField] private GameObject projectilePrefab;
    private EnemyBehavior enemyBehaviorScript;
    
    void Awake()
    {
        animator = GetComponent<Animator>();
        handMuzzle = GameObject.Find("ProjectileHandMuzzle");
        enemyBehaviorScript = GetComponentInParent<EnemyBehavior>();
    }

    public void SetDirection(directionEnum direction)
    {
        // anim.SetInteger("Direction", 1);
        switch (direction)
        {
            case directionEnum.none:
                animator.SetInteger("Direction", 0);
                animator.SetBool("Idle", true);
                break;
            case directionEnum.forward:
                animator.SetInteger("Direction", 1);
                animator.SetBool("Idle", false);
                break;
            case directionEnum.backward:
                animator.SetInteger("Direction", 2);
                animator.SetBool("Idle", false);
                break;
            case directionEnum.left:
                animator.SetInteger("Direction", 3);
                animator.SetBool("Idle", false);
                break;
            case directionEnum.right:
                animator.SetInteger("Direction", 4);
                animator.SetBool("Idle", false);
                break;
        }
    }

    public void Attack()
    {
        animator.SetTrigger("Attack");
    }


    public bool IsAttacking()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ProjectileThrow()
    {
        // Debug.Log("Projectile throw");
        // Transform playerTransform = enemyBehaviorScript.player.transform;
        // Position thing = new Vector3(playerTransform.position.x, playerTransform.position.y * 2f, playerTransform.position.z);
        // Quaternion rotationTowardsPlayer = Quaternion.LookRotation(playerTransform.position - handMuzzle.transform.position);
        // GameObject projectile = Instantiate(projectilePrefab, handMuzzle.transform.position, rotationTowardsPlayer) as GameObject;
        // projectile.GetComponent<ProjectileScript>().IgnoreCollisionWith(enemyBehaviorScript.gameObject);
        
        // chat gpt idea
        Transform playerTransform = enemyBehaviorScript.player.transform;
        Vector3 targetPosition = playerTransform.position;
        
        // Adjust target to be slightly above the player for better arc
        targetPosition.y += 2.0f;
        
        Vector3 startPos = handMuzzle.transform.position;
        Vector3 direction = targetPosition - startPos;
        
        // Extract horizontal distance (XZ plane)
        Vector3 horizontalDirection = new Vector3(direction.x, 0, direction.z);
        float horizontalDistance = horizontalDirection.magnitude;
        
        // Desired time to reach the target (adjust for tuning)
        float projectileSpeed = 20f; // Adjust based on your projectile speed
        float timeToTarget = horizontalDistance / projectileSpeed;
        
        // Calculate vertical velocity to compensate for gravity
        float gravity = Physics.gravity.y;
        float verticalVelocity = (direction.y - 0.5f * gravity * timeToTarget * timeToTarget) / timeToTarget;
        
        // Compute final velocity vector
        Vector3 launchVelocity = horizontalDirection.normalized * projectileSpeed + Vector3.up * verticalVelocity;
        
        // Instantiate projectile
        GameObject projectile = Instantiate(projectilePrefab, startPos, Quaternion.identity);
        projectile.GetComponent<ProjectileScript>().IgnoreCollisionWith(enemyBehaviorScript.gameObject);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        
        if (rb != null)
        {
            rb.linearVelocity = launchVelocity;
        }
    }
    
    private void OnDrawGizmosSelected()
    {
        if (handMuzzle)
        { 
            Gizmos.color = Color.blue; 
            Gizmos.DrawWireSphere(handMuzzle.transform.position, 0.5f);
        }
    }
}
