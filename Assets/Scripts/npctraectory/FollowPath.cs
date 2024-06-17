using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MovementPath;
using UnityEngine.EventSystems;

public class FollowPath : MonoBehaviour
{
    public enum MoveType
    {
        Move,
        Loop
    }

    public MoveType Type = MoveType.Move;
    public MovementPath MyPath;
    public float Waittime;
    public float time = 0;
    public float speed;
    public float currentspeed;
    public float Maxdistance = .1f;
    public float distance = 0;
    public bool stop;
    Vector3 posdist;
    public float xdist, ydist=0;
    public int moveDirection = 1;
    public int moveTo = 0;

    public IEnumerator<Transform> pointInPath;
    public int GetNextPoint( int moveTo)
    {
        if (MyPath.PathElements == null || MyPath.PathElements.Length < 1)
        {
            return 0;
        }

        while (true)
        {

            if (MyPath.PathElements.Length == 1)
            {
                continue;
            }

            if (Type == MoveType.Move)
            {
                if (moveTo <= 0)
                {
                    moveDirection = 1;
                }
                else if (moveTo >= MyPath.PathElements.Length - 1)
                {
                    moveDirection = -1;
                }
            }
            moveTo += moveDirection;

            if (Type == MoveType.Loop)
            {
                if (moveTo >= MyPath.PathElements.Length)
                {
                    moveTo = 0;
                }
                if (moveTo < 0)
                {
                    moveTo = MyPath.PathElements.Length - 1;
                }
            }
            return moveTo;
        }

    }
        // Start is called before the first frame update
        void Start()
    {
        posdist = new Vector3(0.5f*distance, 0.1f*distance, 0);
        Vector3 dist = new Vector3(xdist, 2*ydist, 0);
        if (MyPath == null)
        {
            Debug.Log("Примени путь");
            return;
        }
        currentspeed = speed/2;

        if (MyPath.PathElements[0] == null)
        {
            Debug.Log("Нужны точки");
            return ;
        }

        transform.position = MyPath.PathElements[moveTo].position +posdist+2*dist;
        stop = false;
        moveTo = GetNextPoint(moveTo);
    }

    // Update is called once per frame
    void Update()
    {
        if (MyPath.PathElements == null || MyPath.PathElements[0] == null)
        {
            return ;
        }

        if (Type == MoveType.Move && stop == false)
        {
            transform.position =Vector3.MoveTowards(transform.position, MyPath.PathElements[moveTo].position +posdist, Time.deltaTime*currentspeed);

        }
        else if (Type == MoveType.Loop)
        {
            transform.position = Vector3.Lerp(transform.position, MyPath.PathElements[moveTo].position + posdist, Time.deltaTime * currentspeed);
        }


        var distanceSquare = (transform.position - (MyPath.PathElements[moveTo].position + posdist)).sqrMagnitude;

        if (distanceSquare < Maxdistance*Maxdistance && stop ==false)
        {
            moveTo=GetNextPoint(moveTo);
        }

        if (moveTo >= MyPath.PathElements.Length - 1 && Waittime>0)
        {
            stop = true;
        }
        else
        {
            stop = false;

        }

        if (stop == true)
        {
            currentspeed= 0;
            time += 5f*Time.deltaTime;
        }
        if (time > Waittime)
        {
            currentspeed= speed/2;
            time = 0;
            stop = false;
            moveTo = GetNextPoint(moveTo);
        }

    }
}
