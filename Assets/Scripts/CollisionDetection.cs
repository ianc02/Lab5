using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CollisionDetection : MonoBehaviour
{
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
        Debug.Log("://");
        Debug.Log(tag);
        Debug.Log(other.tag);
        if (tag.Equals("Enemy") && other.tag.Equals("LightCone"))
        {
            Debug.Log("THis works");
            StartCoroutine(wait());
        }
    }

    IEnumerator wait()
    {
        gameObject.GetComponent<NavMeshAgent>().isStopped = true;
        yield return new WaitForSeconds(3);
        gameObject.GetComponent<NavMeshAgent>().isStopped = false;
    }
}
