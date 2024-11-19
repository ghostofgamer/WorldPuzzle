using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseSettingsButton : MonoBehaviour
{
    [SerializeField]private MenuManager menuManager;
    [SerializeField]private GameObject pausePopup;
    
    public void Close()
    {
        menuManager.ClosePopUpMenu(pausePopup);
    }
}
