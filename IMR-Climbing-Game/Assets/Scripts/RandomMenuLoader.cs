using UnityEngine;
using UnityEngine.SceneManagement;

public class RandomMenuLoader : MonoBehaviour
{
    void Start()
    {
        // Randomly load one of the menu scenes
        string[] menuScenes = {
            "MountainMenu",
            "CityMenu"
        };
        string selectedScene = menuScenes[Random.Range(0, menuScenes.Length)];
        SceneManager.LoadScene(selectedScene);
    }
}
