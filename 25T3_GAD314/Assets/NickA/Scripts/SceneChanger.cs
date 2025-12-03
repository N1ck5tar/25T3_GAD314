using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void LoadSceneByName(string sceneName)
    {
        Debug.Log("Loading [" + sceneName + "] scene");
        SceneManager.LoadScene(sceneName); // change the scene
    }

    public void LoadSceneByNumber(int sceneNum) 
    {
        Debug.Log("Loading scene [" + sceneNum + "]");
        SceneManager.LoadScene(sceneNum); // change the scene
    }

    public void QuitTheGame() // quits the application
    {
        Application.Quit(); // quit game
        Debug.Log("Quit Game");
    }
}
