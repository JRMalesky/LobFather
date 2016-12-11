using UnityEngine;
using System.Collections;

public class Ink : MonoBehaviour {

    float killTime = 10;
    void Start()
    {
        Destroy(this.gameObject, killTime);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            Destroy(this.gameObject);
        }

        if (other.transform.gameObject.layer == LayerMask.NameToLayer("Enemy") && (other.transform.gameObject.name != "Lobfather" || other.transform.gameObject.name != "Throw Cane"))
        {
            other.transform.gameObject.GetComponent<scr_enemyHealth>().ApplyDamage(10);
            Destroy(this.gameObject);
        }
    }
}
