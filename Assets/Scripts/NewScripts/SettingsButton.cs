using UnityEngine;

public class SettingsButton : MonoBehaviour
{
    [SerializeField]private MenuManager menuManager;
    [SerializeField]private GameObject pausePopup;
    
    public void Open()
    {
        menuManager.ShowPopUpMenu(pausePopup);
    }
}
