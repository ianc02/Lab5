using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CollisionDetection : MonoBehaviour
{


    public float pauseTime;
    public int health;
    private int timeSinceLastHit;
    // Start is called before the first frame update
    void Start()
    {
        timeSinceLastHit = 60;
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastHit++;
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

        }
    }

    IEnumerator wait(float time)
    {
        gameObject.GetComponent<NavMeshAgent>().isStopped = true;
        yield return new WaitForSeconds(time);
        gameObject.GetComponent<NavMeshAgent>().isStopped = false;
    }
}
