using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class headnormals : MonoBehaviour
{

    [SerializeField] private float normalAtackLife = 2f;
    [SerializeField] private float normalAtackSpeed = 3.5f;
    [SerializeField] private int normalAtackDamage = 5;

    private float normalAtackLifeTime = 0f;
    public GameObject obj;
    public Vector3 resposition;
    public float angle;

    // Update is called once per frame
    void Update()
    {
        normalAtackLifeTime += Time.deltaTime;
        if (normalAtackLifeTime >= normalAtackLife )
        {
            normalAtackLifeTime = 0f;
            gameObject.SetActive( false );
        }
        transform.position = Vector3.MoveTowards(transform.position, resposition, Time.deltaTime*normalAtackSpeed);
        transform.localEulerAngles = new Vector3(0, 0, angle);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 8)
        {
            obj.GetComponent<MCcontroller>().ChangeHealth(-normalAtackDamage);
            gameObject.SetActive(false);
        }
    }
}
