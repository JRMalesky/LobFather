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
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if (sceneName != "Splash_Screen" && sceneName != "Main_Menu")
        {
            Destroy(this.gameObject);
        }

    }
    IEnumerator TransitionToMenu()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(1);
    }
}
