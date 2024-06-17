using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class headatack : MonoBehaviour
{

    [SerializeField] private float manaTimerMax = 0.5f;
    [SerializeField] private float healthTimerMax = 2f;

    float TimerMax = 3f;

    [SerializeField] private float normalAtackInterval = 1f;
    [SerializeField] private float followAtackInterval = 2f;

    [SerializeField] private float normalAtackCooldown = 7f;
    [SerializeField] private float followAtackCooldown = 15f;
    [SerializeField] private float blockCooldown = 20f;

    [SerializeField] private int normalAtackMana = 7;
    [SerializeField] private int followAtackMana = 30;
    [SerializeField] private int blockMana = 50;


    bool atack = false;
    bool block = false;
    bool normal = false;
    bool follow = false;

    public int maxMana = 200;
    public int maxHealth = 150;
    int currentHealth;
    public int currentMana;


    private float manaTime;
    private float healthTime;

    private float coolTime;

    private float normalAtackTime;
    private float followAtackTime;
    private float blockTime;
    

    GameObject objc;
    public Transform[] child;
    void Start()
    {
        currentHealth = maxHealth;
        currentMana = maxMana;
        normalAtackTime = 0f;
        followAtackTime = 0f;
        coolTime = 0f;
        blockTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        atack = transform.gameObject.GetComponent<EnemyController>().atacking;

        
        if ( atack)
        {
            objc = transform.gameObject.GetComponent<EnemyController>().obj;
            
                if (coolTime == 0)
                {
                    int p = Point();
                    Debug.Log(p);
                    ChooseAtack(p);
                }
                coolTime += Time.deltaTime;
                if (coolTime >= TimerMax)
                {
                    coolTime = 0f;
                }
        }
        else
        {
            if (currentHealth < maxHealth)
            {
                healthTime -= Time.deltaTime;
                if (healthTime < 0)
                {
                    currentHealth += 1;
                    healthTime = healthTimerMax;
                }
            }
            else
            {
                healthTime = healthTimerMax;
            }
        }
        if (currentMana < maxMana)
        {
            manaTime -= Time.deltaTime;
            if (manaTime < 0)
            {
                currentMana += 1;
                manaTime = manaTimerMax;
            }
        }
        else
        {
            manaTime = manaTimerMax;
        }
        if (block)
        {
            blockTime += Time.deltaTime;
            if (blockTime >= blockCooldown)
            {
                blockTime = 0f;
                block = false;
            }
        }

        if (follow)
        {
            followAtackTime += Time.deltaTime;
            if (followAtackTime >= followAtackCooldown)
            {
                followAtackTime = 0f;
                follow = false;
            }
        }
        if (normal)
        {
            normalAtackTime += Time.deltaTime;
            if (normalAtackTime >= normalAtackCooldown)
            {
                normalAtackTime = 0f;
                normal = false;
            }
        }
    }

    int Point()
    {
        int points = 0;
        if (followAtackTime == 0 && currentMana>=followAtackMana)
        {
            points += 50;
        }
        if(normalAtackTime == 0 && currentMana>=normalAtackMana)
        {
            points += 10;
        }
        if (blockTime == 0 && currentMana >= blockMana)
        {
            points += 100;
        }
        return points;
    }
    void ChooseAtack(int point)
    {
        switch (point)
        {
            case 0:
                {
                    break;
                }
            case <50:
                {
                    currentMana -= normalAtackMana;
                    NormalAtack();
                    TimerMax = normalAtackInterval * 6+1f;
                    break;
                }
            case <100:
                {
                    currentMana -= followAtackMana;
                    FollowAtack();
                    TimerMax = followAtackInterval * 2+1f;
                    break;
                }
            case >=100:
                {
                    currentMana -= blockMana;
                    Block();
                    TimerMax = 2f;
                    break;
                }

                
        }
        return;
    }


    void NormalAtack()
    {
        if(!normal)
        {
            normal = true;
            child[1].gameObject.GetComponent<headnormalatack>().active = true;
            child[1].gameObject.GetComponent<headnormalatack>().obj = objc;
        }
    }
    void FollowAtack()
    {
        follow = true;
        child[2].gameObject.GetComponent<headfollow>().active = true;
        child[2].gameObject.GetComponent<headfollow>().obj = objc;
    }
    void Block()
    {
        block = true;
        child[0].transform.localScale = objc.transform.localScale;
        child[0].gameObject.SetActive(true);
        child[0].gameObject.GetComponent<headblock>().active = true;
        child[0].gameObject.GetComponent<headblock>().obj = objc;
    }
}
