using UnityEngine;

public class SetActiveWithCheckPoint : MonoBehaviour
{
    public int checkPointIDTarget;

    void Start()
    {
        if (GameProgressManager.Instance.CheckPointID == checkPointIDTarget)
        {
            gameObject.SetActive(false);
        }
    }
}
