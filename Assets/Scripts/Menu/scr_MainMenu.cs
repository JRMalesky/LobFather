using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class scr_MainMenu : MonoBehaviour
{
    public GameObject CreditsPanel;
    public GameObject MainPanel;
    public GameObject LevelSelect;

    public void SwitchScenes(int sceneselction)
    {
        switch (sceneselction)
        {
            case 1:
                LevelSelect.SetActive(true);
                MainPanel.SetActive(false);
                CreditsPanel.SetActive(false);
                break;
            case 2:
                CreditsPanel.SetActive(true);
                MainPanel.SetActive(false);
                LevelSelect.SetActive(false);
                break;
            case 3:
                CreditsPanel.SetActive(false);
                MainPanel.SetActive(true);
                LevelSelect.SetActive(false);
                break;
            case 4:
                SceneManager.LoadScene("Main_Menu");
                break;
            case 5:
                Application.Quit();
                break;
            default:
                break;
        }
    }
}
