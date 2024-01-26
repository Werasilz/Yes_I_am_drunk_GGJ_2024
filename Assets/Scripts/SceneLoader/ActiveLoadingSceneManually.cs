#if UNITY_EDITOR
using Udar.SceneManager;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Project.Utility
{
    public class ActiveLoadingSceneManually : MonoBehaviour
    {
        [SerializeField] private SceneField _loadingScene;

        private void Awake()
        {
            // Find scene loader manager from loading scene
            var sceneLoaderManager = FindObjectOfType<SceneLoaderManager>();

            // Can't find means this enter play directly to gameplay not from loading scene
            if (sceneLoaderManager == null)
            {
                SceneManager.LoadSceneAsync(_loadingScene.Name, LoadSceneMode.Additive);
            }
        }
    }
}
#endif
