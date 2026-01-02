using UnityEngine;
using UnityEngine.SceneManagement;

public class EventHandler : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject optionsPanel;
    public void PlayGame()
    {
        SceneManager.LoadScene("Level"); // Your first level
    }

    public void OpenOptions()
    {
        mainMenuPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }
    public void CloseOptions()
    {
        mainMenuPanel.SetActive(true);
        optionsPanel.SetActive(false);
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
