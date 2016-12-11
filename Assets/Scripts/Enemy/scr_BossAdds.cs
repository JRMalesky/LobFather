using UnityEngine;
using System.Collections;

public class scr_BossAdds : MonoBehaviour
{
    public Vector2 V2_rightSide;
    public Vector2 V2_leftside;
    public float fl_enemySpeed;

    public bool bl_movingRight;

    public float fl_GoTimer;
    private float fl_GoTimerCount;

	void Start ()
    {
        bl_movingRight = false;
        fl_GoTimerCount = 0;
	}
	
	void Update ()
    {
        fl_GoTimerCount += Time.deltaTime;
        if (fl_GoTimerCount >= fl_GoTimer)
        {
            if (bl_movingRight)
            {
                transform.position = Vector2.MoveTowards(transform.position, V2_rightSide, fl_enemySpeed);
        
                if (transform.position.x >= V2_rightSide.x - 0.1f)
                    bl_movingRight = false;
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, V2_leftside, fl_enemySpeed);
                
                if (transform.position.x <= V2_leftside.x + 0.1f)
                    bl_movingRight = true;
            } 
        }
	}

    public void SetGoTimer(float fl_timer)
    {
        fl_GoTimer = fl_timer;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Player>().TakeDamage(1);
        }
    }
}
