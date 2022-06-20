using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MCcontroller : MonoBehaviour
{
    public int maxSpeed = 1000;
    public int maxMana = 20;
    public int maxHealth = 5;

    int currentHealth;
    public int health { get { return currentHealth; } }

    int currentSpeed;
    int currentMana;

    Rigidbody2D rigidbody2d;
    float horizontal;
    float vertical;

    float speed;
    bool Running;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();

        currentHealth = maxHealth;
        currentSpeed = maxSpeed;
        speed = 2.5f;
        Running = false;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        if ((Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)) && (horizontal != 0 || vertical != 0))
        {
            speed = 5.0f;
            Running = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
        {
            speed = 2.5f;
            Running = false;
        }
    }

    void FixedUpdate()
    {
        if (currentSpeed <= 0) //���� ���� ������������ �����������, ��������� �� �����
        {
            speed = 2.5f;
        }

        Vector2 position = rigidbody2d.position;

        if ((position.x == (position.x + horizontal * Time.deltaTime * speed)) && (position.y == position.y + vertical * Time.deltaTime * speed))      //���� ������ shift, ���� �� �����, �� ������������ �� ����� �������
        {
            Running = false;
        }

        if ((Running == false))
        {
            ChangeSpeed(1);
        }
        else
        {
            ChangeSpeed(-2);
        }

        position.x = position.x + horizontal * Time.deltaTime * speed;
        position.y = position.y + vertical * Time.deltaTime * speed;

        rigidbody2d.MovePosition(position);
    }

    public void ChangeHealth(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log(currentHealth + "/" + maxHealth);
    }

    void ChangeSpeed(int amount)
    {
        currentSpeed = Mathf.Clamp(currentSpeed + amount, 0, maxSpeed);
        Debug.Log(currentSpeed + "/" + maxSpeed);
    }

    void ChangeMana(int amount)
    {
        currentMana = Mathf.Clamp(currentMana + amount, 0, maxMana);
        Debug.Log(currentMana + "/" + maxMana);
    }

}