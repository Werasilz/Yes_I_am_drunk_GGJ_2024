using DG.Tweening;
using UnityEngine;

public class FadeUIController : SceneSingleton<FadeUIController>
{
    [SerializeField] private CanvasGroup _canvasGroup;

    public void FadeIn()
    {
        print("Start Fade In");
        float value = _canvasGroup.alpha;
        DOTween.To(() => value, x => value = x, 1, GlobalConfig.DELAY_ALPHA_DEFAULT).OnUpdate(() =>
        {
            _canvasGroup.alpha = value;
        }).OnComplete(() =>
        {
            print("Fade In Complete");
            _canvasGroup.alpha = 1;
        });
    }

    public void FadeOut()
    {
        print("Start Fade Out");
        float value = _canvasGroup.alpha;
        DOTween.To(() => value, x => value = x, 0, GlobalConfig.DELAY_ALPHA_DEFAULT).OnUpdate(() =>
        {
            _canvasGroup.alpha = value;
        }).OnComplete(() =>
        {
            print("Fade Out Complete");
            _canvasGroup.alpha = 0;
        });
    }
}
