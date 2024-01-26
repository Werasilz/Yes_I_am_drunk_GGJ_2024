using DG.Tweening;
using UnityEngine;

public static class Utility
{
    public static void SetCanvasGroupAlpha(CanvasGroup canvasGroup, float alphaTarget)
    {
        float value = canvasGroup.alpha;
        DOTween.To(() => value, x => value = x, alphaTarget, GlobalConfig.DELAY_ALPHA_DEFAULT).OnUpdate(() =>
        {
            canvasGroup.alpha = value;
        }).OnComplete(() =>
        {
            canvasGroup.alpha = alphaTarget;
        });
    }
}

public static class GlobalConfig
{
    public static float DELAY_ALPHA_DEFAULT = 0.5f;
}