using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CollisionDetection : MonoBehaviour
{


    public float pauseTime;
    public int health;
    public float invulnerableTime;
    private bool frozen;
    private float timeSinceLastHit;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        timeSinceLastHit = invulnerableTime;
        animator = GetComponent<Animator>();
        frozen = false;
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastHit += Time.deltaTime;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (tag.Equals("Enemy") && other.tag.Equals("LightCone"))
        {
            StartCoroutine(wait(pauseTime));
        }

        if (tag.Equals("Enemy") && other.tag.Equals("LightSaber") && timeSinceLastHit>invulnerableTime)
        {
            timeSinceLastHit = 0;
            health -= 10;
            Debug.Log(health);
            animator.SetBool("Hit", true);
            gameObject.GetComponent<NavMeshAgent>().velocity = Vector3.zero;
            gameObject.GetComponent<NavMeshAgent>().isStopped = true;
            
            animator.SetInteger("Health", health);
            if (health <= 0)
            {
                animator.SetBool("Dead", true);
                gameObject.GetComponent<NavMeshAgent>().isStopped = true;
                gameObject.GetComponent<NavMeshAgent>().velocity = Vector3.zero;
                // ADD FADE AND DESTROY OBJECT
            }

        }
    }

    public bool isFrozen() { return frozen; }

    IEnumerator wait(float time)
    {
        gameObject.GetComponent<NavMeshAgent>().isStopped = true;
        gameObject.GetComponent<NavMeshAgent>().velocity = Vector3.zero;
        frozen = true;
        yield return new WaitForSeconds(time);
        gameObject.GetComponent<NavMeshAgent>().isStopped = false;
        frozen = false;
    }
}
