using UnityEngine;
using System.Collections;

public class scr_clam : MonoBehaviour {

    // Use this for initialization
    public GameObject pearl;
    float shotTimer;
    float shotChecker;
    GameObject Player;
    void Start ()
    {
        shotTimer = 0f;
        shotChecker = 3.0f;
        Player = GameObject.FindGameObjectWithTag("Player");
	}

    // Update is called once per frame
    void Update()
    {
        shotTimer += Time.deltaTime;

        if (shotTimer >= shotChecker)
        {
            shotTimer = 0f;

            if(Player.GetComponent<Player>())
            {
                if (!Player.GetComponent<Player>().bIsStealth)
                {
                    GameObject Pearl = Instantiate(pearl, this.transform.position, Quaternion.identity) as GameObject;
                    Rigidbody2D rB = Pearl.GetComponent<Rigidbody2D>();
                    rB.AddForce((Player.transform.position - this.transform.position).normalized * 100);
                }
            }
            else
            {
                GameObject Pearl = Instantiate(pearl, this.transform.position, Quaternion.identity) as GameObject;
                Rigidbody2D rB = Pearl.GetComponent<Rigidbody2D>();
                rB.AddForce((Player.transform.position - this.transform.position).normalized * 100);
            }
           
            
        }
	}
}
