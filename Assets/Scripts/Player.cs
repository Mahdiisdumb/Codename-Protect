using UnityEngine;
public class Player : MonoBehaviour
{
    public float playerHP;
    public float playerMaxHP;
    public float[] childHP = new float[5];
    public float[] childMaxHP = new float[5];
    public float minChildHP = 75f;
    public float maxChildHP = 125f;
    public float minPlayerHP = 75f;
    public float maxPlayerHP = 125f;
    public GameObject deathLight;
    public GameObject regularLight;
    private void Start()
    {
        playerMaxHP = Random.Range(minPlayerHP, maxPlayerHP);
        playerHP = playerMaxHP;
        for (int i = 0; i < childHP.Length; i++)
        {
            float randomHP = Random.Range(minChildHP, maxChildHP);
            childMaxHP[i] = randomHP;
            childHP[i] = randomHP;
        }
    }
    public void DamagePlayer(float amount)
    {
        playerHP -= amount;
        if (playerHP < 0) playerHP = 0;
    }
    public void DamageChild(int index, float amount)
    {
        if (index < 0 || index >= childHP.Length) return;
        childHP[index] -= amount;
        if (childHP[index] < 0) childHP[index] = 0;
        Debug.Log("Child " + index + " HP: " + childHP[index]);
    }
    public bool AllChildrenDead()
    {
        foreach (float hp in childHP)
        {
            if (hp > 0) return false;
            Debug.Log("Player HP: " + playerHP);
        }
        return true;
    }
    public bool IsPlayerDead()
    {
        if (playerHP <= 0)
        {
            deathLight.SetActive(true);
            regularLight.SetActive(false);
        }
        else
        {
            regularLight.SetActive(true);
            deathLight.SetActive(false);
        }
        return playerHP <= 0;
    }
}