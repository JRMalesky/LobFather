using UnityEngine;
using System.Collections;

public class scr_ThrowCane : MonoBehaviour
{
    public Vector2 start;
    public Vector2 end;
    public float speed;
    public float timer;

    public bool bl_hitback = false;
    bool bl_bounceOut = false;
    void Start ()
    {

	}
	
	void Update ()
    {
        GetComponent<Rigidbody2D>().AddForce((end - start).normalized * speed);

        start = transform.position;

        if (bl_hitback && !bl_bounceOut)
        {
            GetComponent<Rigidbody2D>().velocity = -GetComponent<Rigidbody2D>().velocity;
            Vector2 copyEnd = end;
            Vector2 copyStart = start;

            end = copyStart;
            start = copyEnd;

            bl_bounceOut = true;
        }
        Destroy(this.gameObject, 8);

        if (transform.position.y >= 130.7 || transform.position.x <= 17)
        {
            Destroy(gameObject);
        }
	}

    public void SetCane(Vector2 v3_start, Vector2 v3_end, float fl_speed)
    {
        start = v3_start;
        end = v3_end;
        speed = fl_speed;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            if (!bl_hitback)
            {
                coll.gameObject.GetComponent<Player>().TakeDamage(1);
                Destroy(gameObject); 
            }
        }

        if (coll.gameObject.tag == "Enemy")
        {
            if (bl_hitback)
            {
                coll.gameObject.GetComponent<scr_Lobfather>().BossTakeDamage();
                Destroy(gameObject);
            }
        }

        //if (coll.gameObject.layer == 11)
        //{
        //    Destroy(gameObject);
        //}
    }
}
