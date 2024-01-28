using UnityEngine;

public class SetActiveWithCheckPoint : MonoBehaviour
{
    public int checkPointIDTarget;

    void Start()
    {
        GameProgressManager.Instance.OnUpdateProgress += () =>
        {
            if (GameProgressManager.Instance.CheckPointID == checkPointIDTarget && GameProgressManager.Instance.GetEnemyProgress(checkPointIDTarget))
            {
                gameObject.SetActive(false);
            }
        };
    }
}
