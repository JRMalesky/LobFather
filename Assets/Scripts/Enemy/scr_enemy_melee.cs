using UnityEngine;
using System.Collections;

public class scr_enemy_melee : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Player") // TODO fix trigger call; player must be real close to call trigger first time
        {
            other.gameObject.GetComponent<Player>().TakeDamage(10);
        }
    }
}
