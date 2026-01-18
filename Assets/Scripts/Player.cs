using UnityEngine;
public class Player : MonoBehaviour
{
    public int playerHP;
    public int playerMaxHP;
    public int[] childHP = new int[5];
    public int[] childMaxHP = new int[5];
    public int minChildHP = 75;
    public int maxChildHP = 125;
    public int minPlayerHP = 75;
    public int maxPlayerHP = 125;
    public GameObject deathLight;
    public GameObject regularLight;
    private void Start()
    {
        playerMaxHP = Random.Range(minPlayerHP, maxPlayerHP + 1);
        playerHP = playerMaxHP;

        for (int i = 0; i < childHP.Length; i++)
        {
            int randomHP = Random.Range(minChildHP, maxChildHP + 1);
            childMaxHP[i] = randomHP;
            childHP[i] = randomHP;
        }
    }
    public void DamagePlayer(int amount)
    {
        playerHP -= amount;
        if (playerHP < 0) playerHP = 0;
    }
    public void DamageChild(int index, int amount)
    {
        if (index < 0 || index >= childHP.Length) return;

        childHP[index] -= amount;
        if (childHP[index] < 0) childHP[index] = 0;

    }
    public bool AllChildrenDead()
    {
        foreach (int hp in childHP)
        {
            if (hp > 0) return false;
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