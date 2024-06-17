using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class headblock : MonoBehaviour
{
    public bool active = false;
    public GameObject obj;

    [SerializeField] private float blockLife = 10f;

    private float blockLifeTime = 0f;

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            blockLifeTime += Time.deltaTime;
            transform.position = obj.transform.localPosition;
            transform.localScale=obj.transform.localScale/8;
            obj.GetComponent<MCcontroller>().block = true;
            if (blockLifeTime > blockLife)
            {
                obj.GetComponent<MCcontroller>().block = false;
                active = false;
                blockLifeTime = 0f;
                gameObject.SetActive(false);
            }
        }
    }
}
