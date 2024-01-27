using DG.Tweening;
using StarterAssets;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    public static PlayerTrigger Instance;

    [Header("Profile")]
    [SerializeField] private Profile _playerProfile;

    [Header("Enemy")]
    [SerializeField] private EnemyController _enemyController;

    [Header("Delay")]
    [SerializeField] private float _delayToShowVersusCanvas = 1f;
    [SerializeField] private float _delayToLoadBattleScene = 3f;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (_enemyController != null && _enemyController.IsTrigger)
        {
            if (StarterAssetsInputs.Instance.enter && GameManager.Instance.isBeginBattle == false)
            {
                GameManager.Instance.isBeginBattle = true;
                QuestionUIController.Instance.SetActiveContent(false);
                PrepareBattle(_enemyController.EnemyProfile);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_enemyController != null)
        {
            return;
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyController enemyController = other.GetComponent<EnemyController>();
            _enemyController = enemyController;

            if (enemyController.IsTrigger == false)
            {
                enemyController.SetTriggerEnter();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            // EnemyController enemyController = other.GetComponent<EnemyController>();
            _enemyController = null;

            // if (enemyController.IsTrigger)
            // {
            //     enemyController.SetTriggerExit();
            // }
        }
    }

    private void PrepareBattle(Profile enemyProfile)
    {
        print($"PrepareBattle Enemy ID:{_enemyController.EnemyProfile.ID}");
        GameManager.Instance.battleEnemyProfile = _enemyController.EnemyProfile;

        PostProcessManager.Instance.Execute();

        DOVirtual.DelayedCall(_delayToShowVersusCanvas, () =>
        {
            VersusUIController.Instance.SetActiveContent(true);
            VersusUIController.Instance.SetVersusProfile(_playerProfile, enemyProfile);
            VersusUIController.Instance.PlayAnimation();
            VersusUIController.Instance.SetCanvasGroupAlpha(1);
        }).OnComplete(() =>
        {
            DOVirtual.DelayedCall(_delayToLoadBattleScene, () =>
            {
                SceneLoaderManager.Instance.LoadBattleScene();
            });
        });
    }
}
