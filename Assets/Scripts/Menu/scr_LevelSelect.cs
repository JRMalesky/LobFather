using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class scr_LevelSelect : MonoBehaviour
{
    public void LevelSelect(int Level)
    {
        switch (Level)
        {
            case 1:
                SceneManager.LoadScene("Level1");
                break;
            case 2:
                SceneManager.LoadScene("Level2");
                break;
            case 3:
                SceneManager.LoadScene("Level3");
                break;
            case 4:
                SceneManager.LoadScene("Level4");
                break;
            case 5:
                SceneManager.LoadScene("Level5");
                break;
            case 6:
                SceneManager.LoadScene("Main_Menu");
                break;
            default:
                break;
        }
    }
}
