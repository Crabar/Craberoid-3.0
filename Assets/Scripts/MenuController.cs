using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void OnStartGameButtonClick()
    {
        SceneManager.LoadScene("Main");
    }

    public void OnExitGameButtonClick()
    {
        Application.Quit();
    }

    public void OnSettingsButtonClick()
    {
        //
    }
}