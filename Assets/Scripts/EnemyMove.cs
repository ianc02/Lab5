using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject player;
    Animator animator;
    void Start()
    {

        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        agent.destination = player.transform.position;
        animator.SetFloat("Speed", GetComponent<Rigidbody>().velocity.magnitude);
        if ((float)Vector3.Distance(transform.position,player.transform.position)<3.2)
        {
            animator.SetBool("Reaching", true);
            agent.isStopped=true;
            agent.velocity = Vector3.zero;
        }
        else
        {
            animator.SetBool("Reaching", false);
            agent.isStopped = false;
        }
    }
}
