using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class DiagforWin : MonoBehaviour
{
    public TMP_Text textBox;
    public AudioSource typeSound;
    public string[] dialogueLines;
    public float charDelay = 0.05f;
    private int currentLine = 0;
    private bool isTyping = false;
    private float lastEscapeTime = 0f;
    private float doubleTapThreshold = 0.3f;
    private bool showEndMessage = false;
    void Start()
    {
        if (dialogueLines.Length > 0)
            StartCoroutine(TypeLine(dialogueLines[currentLine]));
    }
    void Update()
    {
        if (showEndMessage)
        {
            if (Keyboard.current.enterKey.wasPressedThisFrame || Touchscreen.current?.primaryTouch.press.wasPressedThisFrame == true)
                Application.Quit();

            if (Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                if (Time.time - lastEscapeTime < doubleTapThreshold)
                {
                    Application.Quit();
#if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
#endif
                }
                lastEscapeTime = Time.time;
            }
            return;
        }
        if (!isTyping && (Keyboard.current.enterKey.wasPressedThisFrame || Touchscreen.current?.primaryTouch.press.wasPressedThisFrame == true))
        {
            currentLine++;
            if (currentLine < dialogueLines.Length)
                StartCoroutine(TypeLine(dialogueLines[currentLine]));
            else
            {
                textBox.text = "Enter, tap or double-press Escape and double tap to exit.";
                showEndMessage = true;
            }
        }
    }
    private IEnumerator TypeLine(string line)
    {
        isTyping = true;
        textBox.text = "";
        foreach (char c in line)
        {
            textBox.text += c;
            if (typeSound) typeSound.PlayOneShot(typeSound.clip);
            yield return new WaitForSeconds(charDelay);
        }
        isTyping = false;
    }
}