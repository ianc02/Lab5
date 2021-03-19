using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CollisionDetection : MonoBehaviour
{


    public float pauseTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (tag.Equals("Enemy") && other.tag.Equals("LightCone"))
        {
            StartCoroutine(wait());
        }
    }

    IEnumerator wait()
    {
        gameObject.GetComponent<NavMeshAgent>().isStopped = true;
        yield return new WaitForSeconds(pauseTime);
        gameObject.GetComponent<NavMeshAgent>().isStopped = false;
    }
}
