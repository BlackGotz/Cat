using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class headfollows : MonoBehaviour
{

    [SerializeField] private float followAtackLife = 5f;
    [SerializeField] private float followAtackSpeed = 5f;
    [SerializeField] private int followAtackDamage = 20;
    private float followAtackLifeTime = 0f;
    public GameObject obj;

    Vector3 resposition;

    public float angle, ang;

    // Update is called once per frame
    void Update()
    {
        resposition = obj.transform.localPosition;
        followAtackLifeTime += Time.deltaTime;
        if (followAtackLifeTime >= followAtackLife)
        {
            followAtackLifeTime = 0f;
            gameObject.SetActive(false);
        }
        transform.position = Vector3.MoveTowards(transform.position, resposition, Time.deltaTime * followAtackSpeed);
        ang = (transform.position.x - resposition.x) / (transform.position.y - resposition.y);
        if (transform.position.y - resposition.y > 0)
        {
            angle = -Mathf.Atan(ang) * 180 / Mathf.PI;
        }
        else
        {
            angle = 180 - Mathf.Atan(ang) * 180 / Mathf.PI;
        }
        transform.localEulerAngles = new Vector3(0, 0, angle);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 8)
        {
            obj.GetComponent<MCcontroller>().ChangeHealth(-followAtackDamage);
            gameObject.SetActive(false);
        }
    }
}
