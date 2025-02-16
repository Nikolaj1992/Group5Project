using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask setIsGround, setIsPlayer;  // Remember to give the player and ground the correct layers in Unity!
    
    public float health;    // Set health in the nav mesh agent
    
    // Enemy patrolling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
    
    // Enemy attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    
    // Enemy states
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    // Patrol delay
    public float patrolDelay = 9f;
    private float patrolTimer;
    private bool isWaiting = false;
    
    // Attack type - for specific enemy
    public AttackType attackType;
    
    // Bool for logging only
    private bool hasSeenPlayer = false;
    
    // status effect handler, needed here due to speed changes
    private StatusEffectHandler statusEffectHandler;
    // handles walking animations
    private Enemy1AnimationHandler animationHandler1;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        statusEffectHandler = GetComponent<StatusEffectHandler>();
        animationHandler1 = GetComponentInChildren<Enemy1AnimationHandler>();
    }

    private void Update()
    {
        agent.speed = statusEffectHandler.speed;

        if (agent.velocity.magnitude > 0.3)
        {
            animationHandler1.SetDirection(Enemy1AnimationHandler.directionEnum.forward);
        }
        else if (agent.velocity.magnitude < 0.3)
        {
            animationHandler1.SetDirection(Enemy1AnimationHandler.directionEnum.none);
        }
        Debug.Log("SPEED: " + agent.velocity.magnitude);
        
        // Check logic for playerInSightRange and playerInAttackRange
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, setIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, setIsPlayer);
        
        // Log when the enemy sees the player first time - this can be outcommented or safely deleted later
        if (playerInSightRange && !playerInAttackRange && !hasSeenPlayer)
        {
            hasSeenPlayer = true;
            Debug.Log($"Enemy saw the player at sightRange {sightRange}");
        }

        if (!playerInSightRange && hasSeenPlayer)
        {
            hasSeenPlayer = false;
        }
        
        if (!playerInSightRange && !playerInAttackRange) Patrolling();
        if (playerInSightRange && !playerInAttackRange) Chasing();
        if (playerInSightRange && playerInAttackRange) Attacking();
    }

    private void Patrolling()
    {
        if (isWaiting)
        {
            patrolTimer -= Time.deltaTime;
            if (patrolTimer <= 0)
            {
                isWaiting = false;
                SearchForWalkPoint();
            }
            return;
        }
        
        if (!walkPointSet) SearchForWalkPoint();
        
        if (walkPointSet)
            agent.SetDestination(walkPoint);
        
        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        
        // Reached the walkPoint, time to find another
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
            isWaiting = true;   // Wait once we reach the walkPoint
            patrolTimer = patrolDelay;
    }

    private void SearchForWalkPoint()
    {
        // Calculate random point in range for enemy to walk to on patrol
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        
        // Check if walkPoint is on the ground/map
        if (Physics.Raycast(walkPoint, -transform.up, 2f, setIsGround)) 
            walkPointSet = true;
    }
    
    private void Chasing()
    {
        agent.SetDestination(player.position);
    }
    
    private void Attacking()
    {
        // agent.SetDestination(transform.position);    // Stop moving when attacking. Enemy stands still.
        
        // transform.LookAt(player);
        Vector3 direction = player.transform.position - transform.position; // Get direction to player
        direction.y = 0; // Ignore vertical movement (only rotate around Y-axis)
        transform.rotation = Quaternion.LookRotation(direction); // Create rotation only in horizontal plane
        
        if (!alreadyAttacked)
        {
            // We need attack code here, once we have the attack/attacks we want. Slash/shoot/etc.
            // Logs the attack type
            switch (attackType)
            {
                case AttackType.Physical:
                    Debug.Log($"Enemy is using a Physical Attack at attackRange {attackRange}");
                    break;
                case AttackType.Fireball:
                    Debug.Log($"Enemy is using a Fireball Attack at attackRange {attackRange}");
                    break;
                case AttackType.Lightning:
                    Debug.Log($"Enemy is using a Lightning Attack at attackRange {attackRange}");
                    break;
                case AttackType.Frozen:
                    Debug.Log($"Enemy is using a Frozen Attack at attackRange {attackRange}");
                    break;
                default:
                    Debug.Log("Enemy attack type not set");
                    break;
            }
            
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
            MoveDuringCooldown();
        }
    }
    
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    // Move the enemy during cooldown - set in timeBetweenAttacks in Unity for specific enemy
    private void MoveDuringCooldown()
    {
        // Move randomly while waiting for the next attack
        Vector3 randomMovement = new Vector3(
            Random.Range(-2f, 2f), // Random X movement
            0, // Stay on the map!
            Random.Range(-2f, 2f)  // Random Z movement
        );
        
        Vector3 newPosition = transform.position + randomMovement;
        
        agent.SetDestination(newPosition);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        
        if (health <= 0) Invoke(nameof(Destroyed), 0.5f);
    }

    public void Destroyed()
    {
        Destroy(gameObject);
    }
    
    // Visualize the enemy's sight and attack range in our editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
    
}

public enum AttackType
{
    Physical,
    Fireball,
    Lightning,
    Frozen
}
