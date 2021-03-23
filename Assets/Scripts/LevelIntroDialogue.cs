using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelIntroDialogue : MonoBehaviour
{
    public string signText;
    public float typingSpeed;
    public bool singleUse; // If the sign can only be read once
    public GameObject enemies;
    private bool read;

    // Start is called before the first frame update
    void Start()
    {
        read = false;
        enemies.SetActive(false); // Keeps enemies from attacking player until done
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && (!singleUse || !read))
        {
            GameManager.Instance.StartDialog(signText, typingSpeed);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.HideDialog();
            read = true;
            enemies.SetActive(true);
        }
    }
}
