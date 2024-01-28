using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class UIWindow : MonoBehaviour
{
    public string windowName;
    public bool isOpen;
    public CanvasGroup canvasGroup;

    public void Open()
    {
        isOpen = true;

        if (windowName == "Gameplay")
        {
            print("Fade Window Gameplay");
            gameObject.SetActive(isOpen);
            SetCanvasGroup(1, null);
        }
        else
        {
            gameObject.SetActive(isOpen);
        }
    }

    public void Close()
    {
        isOpen = false;

        if (windowName == "Gameplay")
        {
            print("Fade Window Gameplay");
            if (gameObject.activeSelf)
            {
                SetCanvasGroup(0, () =>
                {
                    gameObject.SetActive(isOpen);
                });
            }
        }
        else
        {
            gameObject.SetActive(isOpen);
        }
    }

    private void SetCanvasGroup(float alphaTarget, System.Action callback)
    {
        float value = canvasGroup.alpha;
        DOTween.To(() => value, x => value = x, alphaTarget, GlobalConfig.DELAY_ALPHA_DEFAULT).OnUpdate(() =>
        {
            canvasGroup.alpha = value;
        }).OnComplete(() =>
        {
            canvasGroup.alpha = alphaTarget;
            callback?.Invoke();
        });
    }
}
