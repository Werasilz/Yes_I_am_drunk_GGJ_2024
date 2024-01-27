using DG.Tweening;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform playerTargetPosition;
    public Transform enemyTargetPosition;

    [Header("Character")]
    public Transform playerTransform;
    public Transform enemyTransform;

    [Header("Lerp")]
    public float duration = 1;
    public Ease endEase;

    private void Awake()
    {
        playerTransform.DOMove(playerTargetPosition.position, duration).SetEase(endEase).OnComplete(() =>
        {
            playerTransform.DODynamicLookAt(enemyTransform.localPosition, 1f);
        });
        enemyTransform.DOMove(enemyTargetPosition.position, duration).SetEase(endEase).OnComplete(() =>
        {
            enemyTransform.DODynamicLookAt(playerTransform.localPosition, 1f);
        });
    }
}
