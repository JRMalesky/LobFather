using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour {

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
	// Use this for initialization
	void Start ()
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.Play();
        StartCoroutine("TransitionToMenu");
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}
    IEnumerator TransitionToMenu()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(1);
    }
}
