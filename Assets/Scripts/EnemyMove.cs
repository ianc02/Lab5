using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    public NavMeshAgent agent;
    private GameObject player;
    Animator animator;
    public float damage;
    public float secondsBetweenDamage;
    private float secondsSinceLastDamage;
    private PlayerHealth playerHealth;
    private CollisionDetection collisionDetection;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        playerHealth = player.GetComponent<PlayerHealth>();
        collisionDetection = GetComponent<CollisionDetection>();
        secondsSinceLastDamage = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!collisionDetection.isDead())
        {
            agent.destination = player.transform.position;
            animator.SetFloat("Speed", GetComponent<Rigidbody>().velocity.magnitude);
            if ((float)Vector3.Distance(transform.position, player.transform.position) < 3.2)
            {
                animator.SetBool("Reaching", true);
                agent.isStopped = true;
                agent.velocity = Vector3.zero;
                if (secondsSinceLastDamage > secondsBetweenDamage && !collisionDetection.isFrozen())
                {
                    secondsSinceLastDamage = 0;
                    playerHealth.decreaseHealth(damage);
                }
                else
                {
                    secondsSinceLastDamage += Time.deltaTime;
                }
            }
            else
            {
                animator.SetBool("Reaching", false);
                agent.isStopped = false;
            }
        } else
        {
            agent.isStopped = true;
        }
    }
}
