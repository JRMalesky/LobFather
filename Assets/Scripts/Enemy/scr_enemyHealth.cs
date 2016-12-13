using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class scr_enemyHealth : MonoBehaviour {

    float maxHealth = 1;
    public float currentHealth; 

	// Use this for initialization
	void Start ()
    {
        if (SceneManager.GetActiveScene().name == "Level5")
            maxHealth = 3;
        currentHealth = maxHealth; 
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void ApplyDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
