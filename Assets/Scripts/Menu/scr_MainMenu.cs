using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class scr_MainMenu : MonoBehaviour
{
    public void SwitchScenes(int sceneselction)
    {
        switch (sceneselction)
        {
            case 1:
                SceneManager.LoadScene("Level_Selection");
                break;
            case 2:
                SceneManager.LoadScene("Credits");
                break;
            case 3:
                SceneManager.LoadScene("Extra");
                break;
            case 4:
                Application.Quit();
                break;
            default:
                break;
        }
    }
}
