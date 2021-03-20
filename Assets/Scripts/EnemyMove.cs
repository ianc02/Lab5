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
        if (transform.position.magnitude - player.transform.position.magnitude < 6)
        {
            animator.SetBool("Reaching", true);
        }
    }
}
