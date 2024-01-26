using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Udar.SceneManager;

public enum SceneIndexes
{
    LoadingScene = 0,
    MainMenu = 1,
    ExploreGameplay = 2,
    BattleScene = 3,
}

[System.Serializable]
public class SceneLoaderUIContainer
{
    [SerializeField] private GameObject _loadingCanvas;
    public GameObject LoadingCanvas => _loadingCanvas;
    [SerializeField] private Slider _loadingBar;
    public Slider LoadingBar => _loadingBar;
    [SerializeField] private TMP_Text _loadingText;
    public TMP_Text LoadingText => _loadingText;
}

public class SceneLoaderManager : Singleton<SceneLoaderManager>
{
    [SerializeField] private SceneLoaderUIContainer _UI;

    [Header("All Scenes")]
    [SerializeField] private SceneField[] _scenes;

    [Header("Active Scene")]
    [SerializeField] private SceneIndexes _currentActiveScene = SceneIndexes.LoadingScene;
    public SceneIndexes CurrentActiveScene => _currentActiveScene;

    public System.Action<SceneIndexes> OnLoadSceneComplete;

    private float _progression;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        // First time loading from loading scene to main menu
        if (SceneManager.GetActiveScene().buildIndex == (int)SceneIndexes.LoadingScene)
        {
            LoadMainMenu();
        }
#if UNITY_EDITOR
        // First time loading from other scene (possible in editor only)
        else
        {
            // Update current active scene
            SetCurrentActiveScene(SceneManager.GetActiveScene().buildIndex);

            // Disable loading screen and reset loading value
            SetLoadingCanvasVisible(false);
        }
#endif
    }

    #region Load Scene Method
    [ContextMenu("LoadMainMenu")]
    public void LoadMainMenu()
    {
        LoadScene(GetSceneName(_currentActiveScene), _scenes[(int)SceneIndexes.MainMenu].Name);
    }

    [ContextMenu("LoadExploreGameplay")]
    public void LoadExploreGameplay()
    {
        LoadScene(GetSceneName(_currentActiveScene), _scenes[(int)SceneIndexes.ExploreGameplay].Name);
    }

    [ContextMenu("LoadBattleScene")]
    public void LoadBattleScene()
    {
        LoadScene(GetSceneName(_currentActiveScene), _scenes[(int)SceneIndexes.BattleScene].Name);
    }
    #endregion

    public void LoadScene(string currentSceneName, string nextSceneName)
    {
        StartCoroutine(LoadSceneAsync(currentSceneName, nextSceneName));
    }

    IEnumerator LoadSceneAsync(string currentSceneName, string nextSceneName)
    {
        Debug.Log($"[Scene Loader] Load from {currentSceneName} to {nextSceneName}");

        // Set loading text
        _UI.LoadingText.text = "Loading... 0%";

        // Enable loading screen and reset loading value
        SetLoadingCanvasVisible(true);

        // Unload current scene
        if (_currentActiveScene != SceneIndexes.LoadingScene)
        {
            SceneManager.UnloadSceneAsync(currentSceneName);
        }

        // Load next scene
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(nextSceneName, LoadSceneMode.Additive);

        // Update Active Scene
        SetCurrentActiveScene(SceneManager.GetSceneByName(nextSceneName).buildIndex);

        // Not allow next scene to active
        asyncOperation.allowSceneActivation = false;

        // Scene load not finished
        while (asyncOperation.isDone == false)
        {
            // Set loading bar progress
            float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);
            _UI.LoadingBar.value = progress;

            // Set loading text progress
            float currentProgression = progress * 100;
            _progression = currentProgression;
            _UI.LoadingText.text = "Loading... " + (int)_progression + "%";

            // Load finished
            if (asyncOperation.progress >= 0.9f && !asyncOperation.allowSceneActivation)
            {
                yield return new WaitForSeconds(0.1f);
                asyncOperation.allowSceneActivation = true;
            }

            yield return null;
        }

        OnLoadSceneComplete?.Invoke(_currentActiveScene);

        // Disable loading screen
        SetLoadingCanvasVisible(false);
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex((int)_currentActiveScene));
    }

    private string GetSceneName(SceneIndexes activeScene)
    {
        return _scenes[(int)activeScene].Name;
    }

    private void SetLoadingCanvasVisible(bool isVisible)
    {
        _UI.LoadingCanvas.SetActive(isVisible);
        _progression = 0;
        _UI.LoadingBar.value = 0;
    }

    private void SetCurrentActiveScene(int sceneIndex)
    {
        _currentActiveScene = (SceneIndexes)sceneIndex;
    }
}
