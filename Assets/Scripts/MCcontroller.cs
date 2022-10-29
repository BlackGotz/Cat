using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MCcontroller : MonoBehaviour
{
    public int maxSpeed = 100;
    public int maxMana = 20;
    public int maxHealth = 5;

    int currentHealth;
    public int health { get { return currentHealth; } }

    float currentSpeed;
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
        if (currentSpeed <= 0) //Если наша выносливость закончилась, ускорения не будет
        {
            speed = 2.5f;
        }

        Vector2 position = rigidbody2d.position;

        if ((position.x == (position.x + horizontal * Time.deltaTime * speed)) && (position.y == position.y + vertical * Time.deltaTime * speed))
        { 
            Running = false; 
        }
        else if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            Running = true;
        }

        if ((Running == false))  //Если ускорения нет то выносливость восстанавливается на один за единицу времени, если есть то тратиться на 2 за единицу времени
        {
            ChangeSpeed(1 * Time.deltaTime);
            speed = 2.5f;
        }
        else
        {
            ChangeSpeed(-2* Time.deltaTime);
            speed = 5.0f;
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

    void ChangeSpeed(float amount)
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