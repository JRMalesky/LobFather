using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class scr_HUDManager : MonoBehaviour {

    public Image Heart1;
    public Image Heart2;
    public Image Heart3;

    public Sprite fullHeart;
    public Sprite emptyHeart;

    Player playerCharacter;
	// Use this for initialization
	void Start ()
    {
        playerCharacter = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (playerCharacter.currentHealth == 3)
        {
            Heart1.sprite = fullHeart;
            Heart2.sprite = fullHeart;
            Heart3.sprite = fullHeart;
        }
        else if (playerCharacter.currentHealth == 2)
        {
            Heart1.sprite = fullHeart;
            Heart2.sprite = fullHeart;
            Heart3.sprite = emptyHeart;
        }
        else if (playerCharacter.currentHealth == 1)
        {
            Heart1.sprite = fullHeart;
            Heart2.sprite = emptyHeart;
            Heart3.sprite = emptyHeart;
        }
    }
}
