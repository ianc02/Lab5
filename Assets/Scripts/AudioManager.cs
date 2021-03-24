using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource enemyScream;
    public AudioSource lightsaberSwing;

    // Start is called before the first frame update
    void Start()
    {
        enemyScream = gameObject.AddComponent<AudioSource>();
        lightsaberSwing = gameObject.AddComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playScream()
    {
        enemyScream.Play();
    }

    public void playSwing()
    {
        lightsaberSwing.Play();
    }
}
