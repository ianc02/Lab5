using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawWeapons : MonoBehaviour
{
    public GameObject lightsaber;
    private AudioSource lightsaberSound;
    public GameObject flashlight;
    private AudioSource flashlightSound;

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
            else
            {
                lightsaber.SetActive(false);
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
            else
            {
                flashlight.SetActive(false);
            }
        }
    }
}
