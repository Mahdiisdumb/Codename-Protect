using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public class DamageZone : MonoBehaviour
{
    public Player player;
    public float radius = 5f;
    public float damagePerSecondMin = 20f;
    public float damagePerSecondMax = 50f;
    public string sceneToLoad = "GameOver";
    public float deathDelay = 1f;
    private bool isDying = false;
    private float[] childDamageAccumulator;
    private float playerDamageAccumulator = 0f;
    void Start()
    {
        if (player == null)
            player = FindObjectOfType<Player>();

        childDamageAccumulator = new float[player.childHP.Length];
    }
    void Update()
    {
        if (player == null || isDying) return;
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance <= radius)
        {
            bool allChildrenDead = true;
            for (int i = 0; i < player.childHP.Length; i++)
            {
                if (player.childHP[i] > 0)
                {
                    float damageThisFrame = Random.Range(damagePerSecondMin, damagePerSecondMax) * Time.deltaTime;
                    childDamageAccumulator[i] += damageThisFrame;
                    int intDamage = Mathf.FloorToInt(childDamageAccumulator[i]);
                    if (intDamage > 0)
                    {
                        player.DamageChild(i, intDamage);
                        childDamageAccumulator[i] -= intDamage;
                    }
                    allChildrenDead = false;
                    break;
                }
            }
            if (allChildrenDead)
            {
                float damageThisFrame = Random.Range(damagePerSecondMin, damagePerSecondMax) * Time.deltaTime;
                playerDamageAccumulator += damageThisFrame;
                int intDamage = Mathf.FloorToInt(playerDamageAccumulator);
                if (intDamage > 0)
                {
                    player.DamagePlayer(intDamage);
                    playerDamageAccumulator -= intDamage;
                }
            }
            if (player.IsPlayerDead() && !isDying)
            {
                isDying = true;
                StartCoroutine(HandlePlayerDeath());
            }
        }
    }
    private IEnumerator HandlePlayerDeath()
    {
        yield return new WaitForSeconds(deathDelay);
        SceneManager.LoadScene(sceneToLoad);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}