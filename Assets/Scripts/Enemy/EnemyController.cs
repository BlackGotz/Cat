using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Truth.Utils;
public class EnemyController : MonoBehaviour
{
    [SerializeField] private State startingState;

    [SerializeField] private float roamingDistanceMax = 7f;
    [SerializeField] private float roamingDistanceMin = 3f;
    [SerializeField] private float roamingTimerMax = 2f;
    [SerializeField] private float idleTimerMax = 2f;
    [SerializeField] private float atackingDistanceMin = 3f;
    [SerializeField] private Vector3 minAtacking= new Vector3(0,1,0);

    private NavMeshAgent navMeshAgent;
    private State state;
    private float roamingTime;
    private float idleTime;
    private Vector3 roamPosition;
    private Vector3 startingPosition;
    public GameObject obj;


    public bool atacking = false;
    public Vector3 lookDirection = new Vector3(1, 0,0);

    private enum State
    {
        Idle,
        Roaming,
        Atacking
    }

    private void Start()
    {
        startingPosition = transform.position;
    }
    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
        state = startingState;
    }

    private void Update()
    {
        switch (state)
        {
            default:
            case State.Idle:
            {
                    idleTime -= Time.deltaTime;
                    if (idleTime < 0)
                    {
                        idleTime = idleTimerMax;
                        state = State.Roaming;
                    }
                    break;
            }
            case State.Roaming:
            {
                roamingTime -= Time.deltaTime;
                if(roamingTime < 0)
                {
                    lookDirection = Utils.GetRandomDir();
                    Roaming();
                    roamingTime = roamingTimerMax;
                    state = State.Idle;
                }

                break;
            }
            case State.Atacking:
            {
                    atacking = true;
                    navMeshAgent.SetDestination(obj.transform.position+minAtacking);
                    break;
            }
        }
    }

    private void Roaming()
    {
        roamPosition = GetRoamingPosition();
        navMeshAgent.SetDestination(roamPosition);
    }

    private Vector3 GetRoamingPosition() 
    {
        return startingPosition+lookDirection*UnityEngine.Random.Range(roamingDistanceMin, roamingDistanceMax);
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if ((other.gameObject.layer == 8)&&((other.gameObject.transform.position-transform.position).sqrMagnitude<=atackingDistanceMin*atackingDistanceMin))
        {
            obj = other.gameObject;
            state = State.Atacking;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == 8)
        {
            atacking = false;
            state = State.Roaming;
        }
    }
}
