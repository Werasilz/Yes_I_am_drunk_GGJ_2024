using DG.Tweening;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    [Header("Profile")]
    [SerializeField] private Profile _playerProfile;

    [Header("Delay")]
    [SerializeField] private float _delayToShowVersusCanvas = 1f;
    [SerializeField] private float _delayToLoadBattleScene = 3f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyController enemyController = other.GetComponent<EnemyController>();

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
            EnemyController enemyController = other.GetComponent<EnemyController>();

            if (enemyController.IsTrigger)
            {
                enemyController.SetTriggerExit();
            }
        }
    }

    private void PrepareBattle(Profile enemyProfile)
    {
        PostProcessManager.Instance.SetChromaticAberration();
        PostProcessManager.Instance.SetLensDistortion();
        PostProcessManager.Instance.SetVignette();

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
