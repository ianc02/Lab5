using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawWeapons : MonoBehaviour
{
    public GameObject lightsaber;
    private AudioSource lightsaberSound;
    public GameObject flashlight;
    private AudioSource flashlightSound;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        lightsaberSound = lightsaber.GetComponent<AudioSource>();
        flashlightSound = flashlight.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            flashlight.SetActive(false);
            if (!lightsaber.activeSelf)
            {
                lightsaber.SetActive(true);
                lightsaberSound.Play();
            }
            else if(lightsaber.activeSelf)
            {
                anim.Play("Lightsaber Animation", 0, 0);
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            lightsaber.SetActive(false);
            if (!flashlight.activeSelf)
            {
                flashlight.SetActive(true);
                flashlightSound.Play();
            }
        }
    }
}
