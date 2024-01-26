using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    [Header("Timer")]
    [SerializeField] private TimeCounter _beginBattleUITimeCounter;
    [SerializeField] private TimeCounter _loadBattleSceneTimeCounter;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyController enemyController = other.GetComponent<EnemyController>();

            if (enemyController.IsTrigger == false)
            {
                enemyController.SetTrigger(true);
                PrepareBattle();
            }
        }
    }

    private void PrepareBattle()
    {
        // To Do Show UI
        PostProcessManager.Instance.SetChromaticAberration();
        PostProcessManager.Instance.SetLensDistortion();
        PostProcessManager.Instance.SetVignette();

        void OnLoadBattleSceneTimeCounterComplete()
        {
            SceneLoaderManager.Instance.LoadBattleScene();
        }

        void OnBeginBattleUITimeCounterComplete()
        {
            // Wait for begin load scene
            _loadBattleSceneTimeCounter.StartCounting(this, OnLoadBattleSceneTimeCounterComplete, null);
        }

        // Wait for UI
        _beginBattleUITimeCounter.StartCounting(this, OnBeginBattleUITimeCounterComplete, null);
    }
}
