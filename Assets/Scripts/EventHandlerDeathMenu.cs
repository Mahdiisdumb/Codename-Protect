using UnityEngine;
using UnityEngine.SceneManagement;
public class EventHandlerDeathMenu : MonoBehaviour
{
    public string sceneToLoad = "Level";
    public void PlayGame()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
    public void ExitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}