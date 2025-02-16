using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy1AnimationHandler : MonoBehaviour
{
    private NavMeshAgent agent; 
    private Animator animator;
    
    public enum directionEnum { none, forward, backward, left, right };
    // public Dictionary<string, int> directions = new Dictionary<string, int> { { "none", 0 }, { "forward", 1 }, { "backward", 2 }, { "left", 3 }, { "right", 4 } };
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }
    
    void Update()
    {
        // if (Input.GetMouseButtonDown(0))
        // {
        //     RaycastHit hit;
        //     
        //     if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100)) {
        //         agent.destination = hit.point;
        //     }
        // }
        //
        // if (Input.GetKey(KeyCode.LeftShift))
        // {
        //     agent.speed = runSpeed;
        // } 
        // else
        // {
        //     agent.speed = walkSpeed;
        // }
        //
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     anim.SetTrigger("Jump");
        // }
        //
        // SetAnimationParameters();
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
}
