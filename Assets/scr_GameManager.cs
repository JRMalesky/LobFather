using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class scr_GameManager : MonoBehaviour {

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
        if(other.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
