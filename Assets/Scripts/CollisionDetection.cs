using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CollisionDetection : MonoBehaviour
{


    public float pauseTime;
    public int health;
    private int timeSinceLastHit;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        timeSinceLastHit = 60;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastHit++;
        if (animator.GetBool("Hit"))
        {
            animator.SetBool("Hit", false);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (tag.Equals("Enemy") && other.tag.Equals("LightCone"))
        {
            StartCoroutine(wait(pauseTime));
        }

        if (tag.Equals("Enemy") && other.tag.Equals("LightSaber") && timeSinceLastHit>60)
        {
            timeSinceLastHit = 0;
            health -= 10;
            Debug.Log(health);
            animator.SetBool("Hit", true);
            animator.SetInteger("Health", health);
            if (health <= 0)
            {
                animator.SetBool("Dead", true);
                // ADD FADE AND DESTROY OBJECT
            }

        }
    }

    IEnumerator wait(float time)
    {
        gameObject.GetComponent<NavMeshAgent>().isStopped = true;
        yield return new WaitForSeconds(time);
        gameObject.GetComponent<NavMeshAgent>().isStopped = false;
    }
}
