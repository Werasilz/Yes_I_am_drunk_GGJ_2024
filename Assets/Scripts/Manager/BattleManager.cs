using Cinemachine;
using DG.Tweening;
using Lab1;
using UnityEngine;

public class BattleManager : SceneSingleton<BattleManager>
{
    [Header("Target")]
    [SerializeField] private Transform playerTargetPosition;
    [SerializeField] private Transform enemyTargetPosition;

    [Header("Start Point")]
    [SerializeField] private Transform playerStartPosition;
    [SerializeField] private Transform enemyStartPosition;

    [Header("Enemy")]
    [SerializeField] private EnemyBattle _enemyBattle;

    [Header("Character")]
    public Transform playerTransform;
    public Transform enemyTransform;

    [Header("Camera")]
    [SerializeField] private CinemachineVirtualCamera _battleCamera;

    [Header("Lerp")]
    public float duration = 1;
    public Ease endEase;

    [Header("Initialize Component")]
    public UIGameplayManager uiGameplayManager;
    public Player player;
    public EnemyHealthBar enemyHealth;

    [ContextMenu("StartBattle")]
    public void StartBattle()
    {
        _enemyBattle.SetCharacter();

        _battleCamera.gameObject.SetActive(true);

        playerTransform.DOMove(playerTargetPosition.position, duration).SetEase(endEase).OnComplete(() =>
        {
            playerTransform.DODynamicLookAt(enemyTransform.position, 1f);
        });
        enemyTransform.DOMove(enemyTargetPosition.position, duration).SetEase(endEase).OnComplete(() =>
        {
            enemyTransform.DODynamicLookAt(playerTransform.position, 1f).OnComplete(() =>
            {
                UIWindowManager.Instance.OpenWindow("Gameplay");

                // Initialize all Gameplay
                uiGameplayManager.Initialize();
                player.Initialize();
                enemyHealth.IntializeHealth();
            });
        });
    }

    [ContextMenu("EndBattle")]
    public void EndBattle()
    {
        DOVirtual.DelayedCall(duration, () =>
        {
            _battleCamera.gameObject.SetActive(false);
        });

        playerTransform.DOMove(playerStartPosition.position, duration).SetEase(endEase);
        enemyTransform.DOMove(enemyStartPosition.position, duration).SetEase(endEase);
    }
}
