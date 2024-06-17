using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class headnormalatack : MonoBehaviour
{

    [SerializeField] private float normalAtackInterval = 1f;
    private float normalAtackIntervalTime = 0f;

    public Transform[] normals;
    int normalCount;
    int i;
    public GameObject obj;


    public bool active = false;


    public float angle, ang;
    // Start is called before the first frame update
    void Start()
    {
        normalCount = normals.Length;
        i = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            transform.localPosition = new Vector3(0, 1.5f, 0);
            if (i < normalCount)
            {
                if (normalAtackIntervalTime == 0f)
                {
                    normals[i].transform.position = transform.position;
                    normals[i].gameObject.SetActive(true);
                    normals[i].gameObject.GetComponent<headnormals>().obj = obj;
                    normals[i].gameObject.GetComponent<headnormals>().resposition = obj.transform.localPosition;
                    ang = (transform.position.x - obj.transform.localPosition.x) / (transform.position.y - obj.transform.localPosition.y);
                    if (transform.position.y - obj.transform.localPosition.y > 0)
                    {
                        angle = -Mathf.Atan(ang) * 180 / Mathf.PI;
                    }
                    else
                    {
                        angle = 180 - Mathf.Atan(ang) * 180 / Mathf.PI;
                    }
                    normals[i].gameObject.GetComponent<headnormals>().angle = angle;
                }
                normalAtackIntervalTime += Time.deltaTime;
                if (normalAtackIntervalTime >= normalAtackInterval)
                {
                    i++;
                    normalAtackIntervalTime = 0f;
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
