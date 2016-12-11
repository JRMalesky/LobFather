using UnityEngine;
using System.Collections;

public class scr_SkyCane : MonoBehaviour
{
    public Vector2 V2_skyCaneUp;
    public Vector2 V2_skyCaneDown;
    public float fl_attackSpeed;
    private bool bl_dealtDamage;

    public GameObject player;

    public int in_attackCount;

    private float attackTimer;
    public float fl_attack;

    void Start ()
    {
        bl_dealtDamage = false;
        attackTimer = 0.0f;

	}
	
	void Update ()
    {
        attackTimer += Time.deltaTime;

        if (attackTimer < fl_attack)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.transform.position.x, transform.position.y), fl_attackSpeed);
        }

        if (attackTimer >= fl_attack && !bl_dealtDamage)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, V2_skyCaneDown.y), fl_attackSpeed);
            if (transform.position.y <= V2_skyCaneDown.y + 0.1f)
            {
                bl_dealtDamage = true;
            }
        }

        if (bl_dealtDamage)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, V2_skyCaneUp.y), fl_attackSpeed);

            if (transform.position.y >= V2_skyCaneUp.y - 0.1f)
            {
                attackTimer = 0;
                bl_dealtDamage = false;
                in_attackCount += 1;
            }
        }
	}

    public void SetSkyCane(GameObject player_S)
    {
        player = player_S;
        in_attackCount = 0;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            coll.gameObject.GetComponent<Player>().TakeDamage(1);
        }
    }

    public int NumOfAttacking()
    {
        return in_attackCount;
    }
}
