using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Magic1 : MonoBehaviour
{
    public float speed;
    public float destroyTime;
    public int AtackDamage;
    Animator animator;
    bool collision = false;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        Invoke("DestroyMagic", destroyTime);

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 14)
        {
            collision = true;
            speed = 0f;
            other.GetComponent<Atacked>().ChangeHealth(-AtackDamage);
            animator.SetBool("Collision", collision);
        }
    }
    void DestroyMagic()
    {
        collision = true;
        speed = 0;
        animator.SetBool("Collision", collision);
    }
}
