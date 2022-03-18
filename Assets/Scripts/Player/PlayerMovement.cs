using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{

    NavMeshAgent agent;
    Shooting shooting; 

    Animator animator;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        shooting = GetComponent<Shooting>();
    }

    public void MovePlayer (Vector3 target)
    {
        shooting.StopShooting();
        agent.SetDestination(target);
        animator.SetBool("isRunning", true);
        StartCoroutine(PathCheking());
    }

    private void StopPlayer()
    {
        if (agent.remainingDistance < 1f)
        {
            animator.SetBool("isRunning", false);
        }
        shooting.ResetShot();
    }
    IEnumerator PathCheking()
    {
        yield return null;
        while (agent.remainingDistance >= 1f)
        {
            Debug.Log(agent.remainingDistance);
            yield return null;
        }
        StopPlayer();
    }
}
