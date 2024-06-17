using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MCcontroller : MonoBehaviour
{
    public int maxSpeed = 100;
    public int maxMana = 200;
    public int maxHealth = 300;

    Animator animator;
    Vector2 lookDirection = new Vector2(1, 0);
    Vector2 move;


    public int currentHealth;
    public int health { get { return currentHealth; } }

    public float currentSpeed;
    public int currentMana;

    Rigidbody2D rigidbody2d;
    float horizontal;
    float vertical;

    float speed;
    public bool block = false;
    bool Running;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();

        
        speed = 1.5f;
        Running = false;
        currentHealth = Data.currentHealth;
        currentMana = Data.currentMana;
        currentSpeed = Data.currentSpeed;
        if (Data.first)
        {
            currentHealth = maxHealth;
            currentSpeed = maxSpeed;
            currentMana = maxMana;
        }
        UIHandler.instance.SetManaValue(currentMana / (float)maxMana);
        UIHandler.instance.SetSpeedValue(currentSpeed / (float)maxSpeed);
        UIHandler.instance.SetHealthValue(currentHealth / (float)maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        move = new Vector2(horizontal, vertical);
        if (block)
        {
            ChangeSpeed(1 * Time.deltaTime);
            speed = 0f;
        }
        else
        {
            if ((Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)) && (horizontal != 0 || vertical != 0))
            {
                speed = 3.0f;
                Running = true;
            }

            if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
            {
                speed = 1.5f;
                Running = false;
            }

            if ((Running == false))  //Если ускорения нет то выносливость восстанавливается на один за единицу времени, если есть то тратиться на 2 за единицу времени
            {
                ChangeSpeed(1 * Time.deltaTime);
                speed = 1.5f;
            }
            else
            {
                ChangeSpeed(-2 * Time.deltaTime);
                speed = 3.0f;
            }
        }
           


        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }
        animator.SetFloat("MoveX", lookDirection.x);
        animator.SetFloat("MoveY", lookDirection.y);

    }

    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        animator.SetFloat("MoveX", lookDirection.x);
        animator.SetFloat("MoveY", lookDirection.y);

        if ((position.x == (position.x + horizontal * Time.deltaTime * speed)) && (position.y == position.y + vertical * Time.deltaTime * speed))
        { 
            Running = false; 
            speed = 0f;
        }
        else if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            Running = true;
        }

        position.x = position.x + horizontal * Time.deltaTime * speed;
        position.y = position.y + vertical * Time.deltaTime * speed;
        animator.SetFloat("Speed", speed);
        rigidbody2d.MovePosition(position);
    }

    public void ChangeHealth(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log(currentHealth + "/" + maxHealth);
        UIHandler.instance.SetHealthValue(currentHealth / (float)maxHealth);
    }

    void ChangeSpeed(float amount)
    {
        currentSpeed = Mathf.Clamp(currentSpeed + amount, 0, maxSpeed);
        if (currentSpeed <= 0) //Если наша выносливость закончилась, ускорения не будет
        {
            speed = 1.5f;
            Running = false;
        }
        UIHandler.instance.SetSpeedValue(currentSpeed / (float)maxSpeed);
    }

    public void ChangeMana(int amount)
    {
        currentMana = Mathf.Clamp(currentMana + amount, 0, maxMana);
        Debug.Log(currentMana + "/" + maxMana);
        UIHandler.instance.SetManaValue(currentMana / (float)maxMana);
    }

}