using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VersusUIController : SceneSingleton<VersusUIController>
{
    [SerializeField] private GameObject _content;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private Animator _animator;

    [Header("Player Profile")]
    [SerializeField] private Image _playerImage;
    [SerializeField] private TMP_Text _playerNameText;

    [Header("Enemy Profile")]
    [SerializeField] private Image _enemyImage;
    [SerializeField] private TMP_Text _enemyNameText;

    public void SetActiveContent(bool isActive)
    {
        _content.SetActive(isActive);
    }

    public void SetVersusProfile(Profile playerProfile, Profile enemyProfile)
    {
        _playerImage.sprite = playerProfile.sprite;
        _playerNameText.text = playerProfile.profileName;

        _enemyImage.sprite = enemyProfile.sprite;
        _enemyNameText.text = enemyProfile.profileName;
    }

    public void PlayAnimation()
    {
        _animator.CrossFade("BeginVersus", 0.1f);
    }

    public void SetCanvasGroupAlpha(float alphaTarget)
    {
        float value = _canvasGroup.alpha;
        DOTween.To(() => value, x => value = x, alphaTarget, 1.5f).OnUpdate(() =>
        {
            _canvasGroup.alpha = value;
        }).OnComplete(() =>
        {
            _canvasGroup.alpha = alphaTarget;
        });
    }
}
