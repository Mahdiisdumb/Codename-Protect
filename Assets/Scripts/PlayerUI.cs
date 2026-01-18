using UnityEngine;
using UnityEngine.UI;
public class PlayerUI : MonoBehaviour
{
    public Player player;
    public Slider playerSlider;
    public Slider[] childSliders;
    [Header("Death Sounds")]
    public AudioSource audioSource;
    public AudioClip childDeathClip;
    public AudioClip playerDeathClip;
    private bool[] childDeadPlayed;
    private bool playerDeathPlayed = false;
    void Start()
    {
        if (player == null)
            player = FindObjectOfType<Player>();

        childDeadPlayed = new bool[player.childHP.Length];
    }
    void Update()
    {
        if (player == null) return;

        for (int i = 0; i < player.childHP.Length; i++)
        {
            if (i < childSliders.Length && childSliders[i] != null)
                childSliders[i].value = (float)player.childHP[i] / player.childMaxHP[i];
            if (player.childHP[i] <= 0 && !childDeadPlayed[i])
            {
                childDeadPlayed[i] = true;
                if (childDeathClip != null && audioSource != null)
                    audioSource.PlayOneShot(childDeathClip);
            }
        }
        if (player.AllChildrenDead())
        {
            if (playerSlider != null)
                playerSlider.value = (float)player.playerHP / player.playerMaxHP;
            if (player.IsPlayerDead() && !playerDeathPlayed)
            {
                playerDeathPlayed = true;
                if (playerDeathClip != null && audioSource != null)
                    audioSource.PlayOneShot(playerDeathClip);
            }
        }
        else
        {
            if (playerSlider != null)
                playerSlider.value = 1f;
        }
    }
}