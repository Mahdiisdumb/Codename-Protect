using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public class DamageZone : MonoBehaviour
{
    public Player player;
    public float radius = 5f;
    public float damagePerSecond = 10f;
    public string sceneToLoad = "GameOver";
    public float damageMin = 20f;
    public float damageMax = 50f;
    public float deathDelay = 1f;
    private bool isDying = false;
    void Start()
    {
        if (player == null)
            player = FindObjectOfType<Player>();
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
                    float damage = Random.Range(damageMin, damageMax) * Time.deltaTime;
                    player.DamageChild(i, damage);
                    allChildrenDead = false;
                    break;
                }
            }

            if (allChildrenDead)
            {
                float damage = Random.Range(damageMin, damageMax) * Time.deltaTime;
                player.DamagePlayer(damage);
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