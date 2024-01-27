using DG.Tweening;
using StarterAssets;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    public static PlayerTrigger Instance;

    [Header("Profile")]
    [SerializeField] private Profile _playerProfile;

    [Header("Enemy")]
    [SerializeField] private EnemyController _currentEnemyTrigger;

    [Header("Delay")]
    [SerializeField] private float _delayToShowVersusCanvas = 1f;
    [SerializeField] private float _delayToLoadBattleScene = 3f;

    private Animator _animator;

    private void Awake()
    {
        Instance = this;
        _animator = GetComponent<Animator>();
        _animator.CrossFade("StandUp", 0.1f);

        GameManager.Instance.isStopPlayerMove = true;
        DOVirtual.DelayedCall(5f, () =>
        {
            GameManager.Instance.isStopPlayerMove = false;
        });
    }

    private void Update()
    {
        if (_currentEnemyTrigger != null && _currentEnemyTrigger.IsTrigger)
        {
            if (StarterAssetsInputs.Instance.enter && GameManager.Instance.isBeginBattle == false)
            {
                GameManager.Instance.isBeginBattle = true;
                QuestionUIController.Instance.SetActiveContent(false);
                PrepareBattle(_currentEnemyTrigger.EnemyProfile);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_currentEnemyTrigger != null)
        {
            return;
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyController enemyController = other.GetComponent<EnemyController>();

            _currentEnemyTrigger = enemyController;
            print($"Trigger {_currentEnemyTrigger.EnemyProfile.profileName}");

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

            // if (enemyController == _currentEnemyTrigger)
            // {
            //     _currentEnemyTrigger = null;
            // }

            // if (enemyController.IsTrigger)
            // {
            //     enemyController.SetTriggerExit();
            // }
        }
    }

    private void PrepareBattle(Profile enemyProfile)
    {
        print($"PrepareBattle Enemy ID:{_currentEnemyTrigger.EnemyProfile.ID}");
        GameManager.Instance.battleEnemyProfile = _currentEnemyTrigger.EnemyProfile;

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
