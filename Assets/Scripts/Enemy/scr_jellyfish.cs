using UnityEngine;
using System.Collections;

public class scr_jellyfish : MonoBehaviour
{
    public float amplitudeX = 1.0f;
    public float amplitudeY = .5f;
    public float omegaX = .5f;
    public float omegaY = 2.5f;
    float index;
    float jellySoundDelay;

    Player playerCharacter;
    AudioSource audio;
    void Start()
    {
        amplitudeX = Random.Range(.5f, 1.5f);
        amplitudeY = Random.Range(.3f, .7f);
        omegaX = Random.Range(.2f, .8f);
        omegaY = Random.Range(1.5f, 3.5f);
        jellySoundDelay = Random.Range(3f, 7f);

        audio = GetComponent<AudioSource>();

        StartCoroutine(JellySound(jellySoundDelay));
    }
    // Update is called once per frame
    void Update()
    {
        index += Time.deltaTime;
        float x = amplitudeX * Mathf.Cos(omegaX * index);
        float y = amplitudeY * Mathf.Sin(omegaY * index);
        transform.localPosition = new Vector3(x, y, 0);
    }
   IEnumerator JellySound(float delay)
    {
        yield return new WaitForSeconds(delay);
        audio.Play();
        StartCoroutine(JellySound(jellySoundDelay));
    }
    //Collision Event
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerCharacter = other.gameObject.GetComponent<Player>();

            if (playerCharacter)
            {
                if (!playerCharacter.bIsStealth)
                {
                    playerCharacter.TakeDamage(1);
                }
            }
        }
    }
}
