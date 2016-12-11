using UnityEngine;
using System.Collections;

public class scr_crab : MonoBehaviour {

    private float flt_speed = 1.25f;
    private float flt_damage = 1; 
    private int int_direction = 1;
    float defaultScale;

    SpriteRenderer spriteRenderer;
    Animator anim;
	
    // Use this for initialization
	void Start()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        //defaultScale = this.transform.localScale.x;
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Move crab to the right
        if (int_direction != 0)
        {
            transform.Translate((Vector3.right * flt_speed * Time.deltaTime) * int_direction);

        }
    }
   
    //Collision event
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Wall")
        {
            int_direction = -int_direction;
        }
        else if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Player>().TakeDamage(flt_damage);
        }
    }
}

