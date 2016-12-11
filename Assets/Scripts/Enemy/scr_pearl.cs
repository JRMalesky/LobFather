using UnityEngine;
using System.Collections;

public class scr_pearl : MonoBehaviour
{
    float killTime = 10;
    void Start()
    {
        Destroy(this.gameObject, killTime);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Player>().TakeDamage(1);
            Destroy(this.gameObject);
        }

        if (other.transform.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
} 
