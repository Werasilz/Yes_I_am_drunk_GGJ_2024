using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;
    private Animator _animator;

    private Vector3 _startPosition;
    private float _animatorSpeed;
    private bool _isArrive = false;

    [Header("AI Settings")]
    public float patrolRadius = 5f;

    [Space]
    public float minPatrolDelay = 2f;
    public float maxPatrolDelay = 4f;

    [Space]
    public float minWaitTime = 1f;
    public float maxWaitTime = 3f;

    [Space]
    public float lerpSpeed = 5f;

    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponentInChildren<Animator>();
        _startPosition = transform.position;
        PatrolToRandomPoint();
    }

    void Update()
    {
        if (_navMeshAgent.isStopped == false)
        {
            if (_navMeshAgent.remainingDistance > 0.1f)
            {
                _animatorSpeed = Mathf.Lerp(_animatorSpeed, 1f, Time.deltaTime * lerpSpeed);
            }
            else
            {
                _animatorSpeed = Mathf.Lerp(_animatorSpeed, 0f, Time.deltaTime * lerpSpeed);

                if (_isArrive == false)
                {
                    _isArrive = true;
                    float waitTime = Random.Range(minWaitTime, maxWaitTime);
                    DOVirtual.DelayedCall(waitTime, () =>
                    {
                        WaitAtDestination();
                    });
                }
            }

            _animator.SetFloat("Speed", _animatorSpeed);
        }
    }

    void PatrolToRandomPoint()
    {
        _isArrive = false;
        Vector3 randomPoint = RandomNavSphere(_startPosition, patrolRadius, -1);
        _navMeshAgent.SetDestination(randomPoint);
    }

    void WaitAtDestination()
    {
        float patrolDelay = Random.Range(minPatrolDelay, maxPatrolDelay);
        DOVirtual.DelayedCall(patrolDelay, () =>
        {
            PatrolToRandomPoint();
        });
    }

    Vector3 RandomNavSphere(Vector3 origin, float distance, int layerMask)
    {
        Vector3 randomDirection = Random.insideUnitSphere * distance;
        randomDirection += origin;
        NavMesh.SamplePosition(randomDirection, out NavMeshHit navHit, distance, layerMask);
        return navHit.position;
    }

    public void SetStopMove(bool isStop)
    {
        _navMeshAgent.isStopped = isStop;

        if (isStop)
        {
            float value = _animator.GetFloat("Speed");
            DOTween.To(() => value, x => value = x, 0, 0.5f).OnUpdate(() =>
            {
                _animator.SetFloat("Speed", value);
            }).OnComplete(() =>
            {
                Vector3 lookAt = new(0, PlayerTrigger.Instance.transform.position.y, 0);
                transform.DODynamicLookAt(lookAt, 1f);
            });
        }
    }
}
