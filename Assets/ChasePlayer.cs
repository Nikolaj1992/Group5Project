using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChasePlayer : MonoBehaviour
{
    private Transform player;
    private NavMeshAgent agent;
    [SerializeField] private float baseSpeed;
    [HideInInspector] public float speed;
    [HideInInspector] public bool debuffed;
    [HideInInspector] public float debuffTimer;
    [HideInInspector] public Dictionary<string, Debuff> debuffs = new Dictionary<string, Debuff>();
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        speed = baseSpeed;
    }
    
    void Update()
    {
        List<string> keys = new List<string>(debuffs.Keys); // To avoid modifying dictionary while iterating
        
        bool speedModified = false;
        float speedModifier = 1f; // Default multiplier

        foreach (string key in keys)
        {
            Debuff debuff = debuffs[key];

            if (debuff.isActive)
            {
                debuff.duration -= Time.deltaTime;

                if (debuff.duration <= 0)
                {
                    RemoveDebuff(key);
                }
                else
                {
                    debuffs[key] = debuff; // Update the dictionary with the reduced timer
                }

                // If the debuff affects speed, apply it
                if (debuff.affectsSpeed)
                {
                    speedModified = true;
                    speedModifier *= debuff.speedMultiplier;
                }
            }
        }
        
        speed = speedModified ? baseSpeed * speedModifier : baseSpeed;
        agent.speed = speed;
        
        if (Vector3.Distance(player.position, transform.position) > 1.5f)
        { 
            agent.SetDestination(player.position);
            agent.isStopped = false;
        }
        else
        {
            agent.SetDestination(gameObject.transform.position);
            agent.isStopped = true;
        }
    }

    public void ApplyDebuff(Debuff debuff)
    {
        ApplyDebuff(debuff.name, debuff.duration, debuff.stackable, debuff.affectsSpeed, debuff.speedMultiplier);
    }
    
    public void ApplyDebuff(string debuffName, float duration, bool stackable, bool affectsSpeed = false, float speedMultiplier = 1f)
    {
        if (debuffs.ContainsKey(debuffName))
        {
            // If the debuff is already active, refresh its timer instead of stacking it
            Debuff existingDebuff = debuffs[debuffName];
            if (existingDebuff.stackable)
            {
            existingDebuff.duration = Mathf.Max(existingDebuff.duration, duration); // Keep the longest duration
            debuffs[debuffName] = existingDebuff; // Update the dictionary
            Debug.Log($"{debuffName} refreshed with {existingDebuff.duration} seconds remaining.");
            }
            if (existingDebuff.duration == 0)
            {
                debuffs[debuffName] = new Debuff(true, duration, stackable, affectsSpeed, speedMultiplier);
                Debug.Log($"Applied {debuffName} for {duration} seconds. (already existed)");
            }
        }
        else
        {
            // Otherwise, add a new debuff
            debuffs[debuffName] = new Debuff(true, duration, stackable, affectsSpeed, speedMultiplier);
            Debug.Log($"Applied {debuffName} for {duration} seconds.");
        }
    }

    private void RemoveDebuff(string debuffName)
    {
        if (debuffs.ContainsKey(debuffName))
        {
            debuffs[debuffName] = new Debuff(false, 0, false, false, 1f);
            Debug.Log($"{debuffName} has expired.");
        }
    }

    // public void ApplyFrozenDebuff(bool activate, float duration)
    // {
    //     if (activate && !debuffed)
    //     {
    //         debuffed = true;
    //         speed *= 0.7f;
    //         debuffTimer = duration;
    //     }
    //     if (!activate && debuffed)
    //     {
    //         debuffed = false;
    //         speed = baseSpeed;
    //         debuffTimer = 0f;
    //     }
    // }
}
