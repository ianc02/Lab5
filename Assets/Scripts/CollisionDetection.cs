using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CollisionDetection : MonoBehaviour
{
    public float pauseTime;
    public int health;
    public float invulnerableTime;
    private bool dead;
    private bool frozen;
    private float timeSinceLastHit;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        timeSinceLastHit = invulnerableTime;
        animator = GetComponent<Animator>();
        frozen = false;
        dead = false;
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastHit += Time.deltaTime;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (tag.Equals("Enemy") && collision.collider.tag.Equals("LightCone"))
        {
            StartCoroutine(wait(pauseTime));
        }

        if (tag.Equals("Enemy") && collision.collider.tag.Equals("LightSaber") && timeSinceLastHit > invulnerableTime)
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
                dead = true;
                GameManager.Instance.checkEnemyCount();
                Destroy(gameObject, 10.0f);
            }

        }
    }
   
    public bool isFrozen() { return frozen; }

    public bool isDead() { return dead; }

    IEnumerator wait(float time)
    {
        frozen = true;
        Debug.Log("Waiting");
        gameObject.GetComponent<NavMeshAgent>().isStopped = true;
        gameObject.GetComponent<NavMeshAgent>().velocity = Vector3.zero;
        yield return new WaitForSeconds(time);
        gameObject.GetComponent<NavMeshAgent>().isStopped = false;
        frozen = false;
        Debug.Log("Done waiting");
    }
}
