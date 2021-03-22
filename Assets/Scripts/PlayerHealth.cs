using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth;
    private float currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void decreaseHealth(float deltaHealth)
    {
        Debug.Log("Took damage");
        currentHealth -= deltaHealth;
        Debug.Log(currentHealth);
        GameManager.Instance.adjustHealth(currentHealth, maxHealth);
        if (currentHealth <= 0)
        {
            GameManager.Instance.GameOver();
        }
    }
}
