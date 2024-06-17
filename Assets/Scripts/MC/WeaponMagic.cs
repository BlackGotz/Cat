using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMagic : MonoBehaviour
{
    public GameObject manaPrefab;
    public Transform shotDir;
    public GameObject obj;
    private float timeShot;
    public float startTime;
    public int Mana;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 resposition = Input.mousePosition;
        resposition.z = 10;
        resposition = Camera.main.ScreenToWorldPoint(resposition) - transform.position;
        float rotateZ = Mathf.Atan2(resposition.y, resposition.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotateZ);
        if (timeShot <= 0)
        {

            if (Input.GetMouseButtonDown(0))
            {
                if (obj.GetComponent<MCcontroller>().currentMana >= Mana)
                {
                    obj.GetComponent<MCcontroller>().ChangeMana(-Mana);
                    Instantiate(manaPrefab, shotDir.position, transform.rotation);
                    timeShot = startTime;
                }
            }
            
        }
        else
        {
            timeShot -= Time.deltaTime;
        }
    }
}
