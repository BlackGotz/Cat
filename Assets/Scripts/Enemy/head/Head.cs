using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour
{
    Animator animator;

    Vector3 lookDirection = new Vector3(1, 0);
    bool atack = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        lookDirection = transform.parent.gameObject.GetComponent<EnemyController>().lookDirection;
    }

    // Update is called once per frame
    void Update()
    {
        lookDirection = transform.parent.gameObject.GetComponent<EnemyController>().lookDirection;
        atack = transform.parent.gameObject.GetComponent<EnemyController>().atacking;

        animator.SetFloat("MoveX", lookDirection.x);
        animator.SetFloat("MoveY", lookDirection.y);
        animator.SetBool("Atacking", atack);
    }
}
