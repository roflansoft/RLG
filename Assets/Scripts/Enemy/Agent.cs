using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class Agent : MonoBehaviour
{
    [SerializeField] private Transform target;

    private NavMeshAgent _agent;

    public float distanceRage;

    public float stopDistance;

    public bool triggered;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        _agent.SetDestination(target.position);
    }

    private void Update()
    {
        if (target == null) return;
        if (triggered == false)
        {
            if (_agent.remainingDistance <= distanceRage && _agent.remainingDistance != 0)
            {
                triggered = true;
                _agent.isStopped = false;
            }
            else
            {
                _agent.isStopped = true;
            }
        }

        _agent.stoppingDistance =
            Mathf.Abs(_agent.remainingDistance - Vector2.Distance(target.position, transform.position)) < 0.1
                ? stopDistance
                : 0.5f;
        _agent.SetDestination(target.position);
    }
}
