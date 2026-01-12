using UnityEngine;
using UnityEngine.SceneManagement;
public class EventHandler : MonoBehaviour
{
    public string sceneToLoad = "Level";
    public GameObject mainMenuPanel;
    public GameObject optionsPanel;
    public GameObject creditsPanel;
    public GameObject changelogPanel;
    public void PlayGame()
    {
        SceneManager.LoadScene(sceneToLoad);
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
    public void OpenCredits()
    {
        mainMenuPanel.SetActive(false);
        creditsPanel.SetActive(true);
    }
    public void CloseCredits()
    {
        mainMenuPanel.SetActive(true);
        creditsPanel.SetActive(false);
    }
    public void OpenChangelog()
    {
        mainMenuPanel.SetActive(false);
        changelogPanel.SetActive(true);
    }
    public void CloseChangelog()
    {
        mainMenuPanel.SetActive(true);
        changelogPanel.SetActive(false);
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