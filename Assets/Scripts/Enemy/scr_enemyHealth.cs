using UnityEngine;
using System.Collections;

public class scr_enemyHealth : MonoBehaviour {

    float maxHealth = 1;
    public float currentHealth; 

	// Use this for initialization
	void Start ()
    {
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
