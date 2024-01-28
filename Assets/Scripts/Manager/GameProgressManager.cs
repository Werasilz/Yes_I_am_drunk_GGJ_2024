using UnityEngine;

[System.Serializable]
public class EnemyProgress
{
    public int enemyID;
    public bool isClear = false;
    public CheckPointTrigger checkPointTrigger;
}

public class GameProgressManager : Singleton<GameProgressManager>
{
    [Header("Start Point")]
    [SerializeField] private Vector3 _startPoint;

    [Header("Check Point")]
    [SerializeField] private int _checkPointID;
    public int CheckPointID => _checkPointID;
    [SerializeField] private Vector3 _checkPointPosition;
    public Vector3 CheckPointPosition => _checkPointPosition;

    [Header("Enemy Progress")]
    [SerializeField] private int _lastEnemyProgress = 0;
    [SerializeField] private EnemyProgress[] _enemyProgresses;

    public System.Action OnUpdateProgress;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        _checkPointPosition = _startPoint;
        LoadCheckPoint();

        // For load check point when back to explore gameplay scene
        SceneLoaderManager.Instance.OnLoadSceneComplete += (sceneIndexes) =>
        {
            if (sceneIndexes == SceneIndexes.ExploreGameplay)
            {
                CheckEnemyProgress();
            }
        };
    }

    public void LoadCheckPoint()
    {
        PlayerTrigger player = FindObjectOfType<PlayerTrigger>();
        player.gameObject.transform.position = _checkPointPosition;
    }

    public void SetCheckPoint(int checkPointID, Vector3 checkPointPosition)
    {
        _checkPointID = checkPointID;
        _checkPointPosition = checkPointPosition;
    }

    public void UpdateEnemyProgress(int enemyID, bool isClear)
    {
        if (isClear)
        {
            _enemyProgresses[enemyID].isClear = isClear;
            _lastEnemyProgress = enemyID;
            OnUpdateProgress?.Invoke();
        }
        else
        {
        }
    }

    public void CheckEnemyProgress()
    {
        if (_enemyProgresses[_lastEnemyProgress].isClear)
        {
            SetCheckPoint(_lastEnemyProgress, _enemyProgresses[_lastEnemyProgress].checkPointTrigger.CheckPointTransform.position);
            LoadCheckPoint();
        }
    }

    public bool GetEnemyProgress(int enemyID)
    {
        return _enemyProgresses[enemyID].isClear;
    }
}
