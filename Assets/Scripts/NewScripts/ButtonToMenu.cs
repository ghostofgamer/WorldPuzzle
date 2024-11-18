using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonToMenu : MonoBehaviour
{
    public const string Menu = "MainScene";
    
    public void LoadScene()
    {
        SceneManager.LoadScene(Menu);
    }
}
