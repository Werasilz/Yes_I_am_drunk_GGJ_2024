using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    [Header("Timer")]
    [SerializeField] private TimeCounter _beginBattleTimeCounter;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyController enemyController = other.GetComponent<EnemyController>();

            if (enemyController.IsTrigger == false)
            {
                enemyController.SetTrigger(true);

                void OnComplete()
                {
                    SceneLoaderManager.Instance.LoadBattleScene();
                }

                _beginBattleTimeCounter.StartCounting(this, OnComplete, null);
            }
        }
    }
}
