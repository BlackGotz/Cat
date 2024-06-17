using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class headfollow : MonoBehaviour
{

    [SerializeField] private float followAtackInterval = 2f;
    private float followAtackIntervalTime = 0f;

    public Transform[] follows;
    int followCount;
    int i;

    public GameObject obj;

    public bool active = false;
    // Start is called before the first frame update
    void Start()
    {
        followCount = follows.Length;
        i = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(active)
        {
            transform.localPosition = new Vector3(0, 1.5f, 0);
            if (i < followCount)
            {
                if (followAtackIntervalTime == 0f)
                {
                    follows[i].transform.position = transform.position;
                    follows[i].gameObject.SetActive(true);
                    follows[i].gameObject.GetComponent<headfollows>().obj = obj;
                }
                followAtackIntervalTime += Time.deltaTime;
                if (followAtackIntervalTime >= followAtackInterval)
                {
                    i++;
                    followAtackIntervalTime = 0f;
                }
            }
            else
            {
                active = false;
                i = 0;
            }
        }
    }
}
