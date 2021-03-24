using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelIntroDialogue : MonoBehaviour
{
    public string signText;
    public float typingSpeed;
    public GameObject enemies;

    // Start is called before the first frame update
    void Start()
    {
        enemies.SetActive(false); // Keeps enemies from attacking player until done
        GameManager.Instance.StartDialog(signText, typingSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            GameManager.Instance.HideDialog();
            enemies.SetActive(true);
            Destroy(gameObject);
        }
    }
}
