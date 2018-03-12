using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace UI
{
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

        public void HideMenuOnClick(GameObject menu)
        {
            menu.SetActive(false);
        }

        public void ShowMenuOnClick(GameObject menu)
        {
            menu.SetActive(true);
        }
    }
}