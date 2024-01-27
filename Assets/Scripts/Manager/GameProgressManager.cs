using UnityEngine;

[System.Serializable]
public class EnemyProgress
{
    public int enemyID;
    public bool isClear = false;
}

public class GameProgressManager : Singleton<GameProgressManager>
{
    [Header("Check Point")]
    [SerializeField] private int _checkPointID;
    public int CheckPointID => _checkPointID;
    [SerializeField] private Vector3 _checkPointPosition;
    public Vector3 CheckPointPosition => _checkPointPosition;

    [Header("Enemy Progress")]
    [SerializeField] private EnemyProgress[] _enemyProgresses;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        LoadCheckPoint();

        SceneLoaderManager.Instance.OnLoadSceneComplete += (sceneIndexes) =>
        {
            if (sceneIndexes == SceneIndexes.ExploreGameplay)
            {
                LoadCheckPoint();
            }
        };
    }

    public void LoadCheckPoint()
    {
        GameManager.Instance.Reset();
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
        _enemyProgresses[enemyID].isClear = isClear;
    }
}
