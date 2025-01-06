using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void LoadCityScene()
    {
        SceneManager.LoadScene("City");
    }

    public void LoadMountainScene()
    {
        SceneManager.LoadScene("Mountain");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
