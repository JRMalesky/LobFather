using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class scr_ExtraMenu : MonoBehaviour
{
    public void ButtonSelection(int whichButton)
    {
        switch (whichButton)
        {
            case 1:
                SceneManager.LoadScene("Main_Menu");
                
                break;
            default:
                break;
        }
    }
}
