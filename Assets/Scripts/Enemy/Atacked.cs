using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atacked : MonoBehaviour
{
    public int maxHealth = 100;
    int currentHealth;
    public GameObject goldCoinPrefab, silverCoinPrefab, redCoinPrefab, expPrefab, Resource;
    public int minCoinDrops = 50;
    public int maxCoinDrops = 105;
    public int expDrop = 80;
    public int resourceDrop = 5;
    public float dropForce = 1f;
    public GameObject obj,other;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            Vector3 pos = obj.GetComponent<Rigidbody2D>().position;
            pos.z = -0.07f;
            DropMana(pos);
        }
    }

    public void DropMana(Vector2 dropPosition)
    {
        other.GetComponent<MCcontroller>().block = false;
        int numberOfDrops = Random.Range(minCoinDrops, maxCoinDrops + 1);
        int gold = numberOfDrops / 100;
        int silver = (numberOfDrops - gold * 100) / 10;
        int red = numberOfDrops - gold * 100-silver*10;
        int item = Random.Range(0, resourceDrop + 1);

        for (int i = 0; i < gold; i++)
        {
            Vector2 pos = new Vector2(dropPosition.x + Random.Range(-0.3f, 0.3f), dropPosition.x + Random.Range(-0.2f, 0.2f));
            Instantiate(goldCoinPrefab, pos, Quaternion.identity);
        }
        for (int i = 0; i < silver; i++)
        {
            Vector2 pos = new Vector2(dropPosition.x + Random.Range(-0.3f, 0.3f), dropPosition.x + Random.Range(-0.2f, 0.2f));
            Instantiate(silverCoinPrefab, pos, Quaternion.identity);
        }
        for (int i = 0; i < red; i++)
        {
            Vector2 pos = new Vector2(dropPosition.x + Random.Range(-0.3f, 0.3f), dropPosition.x + Random.Range(-0.2f, 0.2f));
            Instantiate(redCoinPrefab, pos, Quaternion.identity);
        }
        for (int i = 0; i < expDrop/10; i++)
        {
            Vector2 pos = new Vector2(dropPosition.x + Random.Range(-0.3f, 0.3f), dropPosition.x + Random.Range(-0.2f, 0.2f));
            Instantiate(expPrefab, pos, Quaternion.identity);
        }
        for (int i = 0; i < item; i++)
        {
            Vector2 pos = new Vector2(dropPosition.x + Random.Range(-0.3f, 0.3f), dropPosition.x + Random.Range(-0.2f, 0.2f));
            Instantiate(Resource, pos, Quaternion.identity);
        }
        Destroy(obj);
    }
    public void ChangeHealth(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log(currentHealth + "/" + maxHealth);
    }
}
