using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Lab1;
using StarterAssets;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public Player player;
    public int targetHealth;
    public int currrentHealth;
    public Image fill;

    public Sprite[] emojis;
    public Image emojiImage;


    public void IntializeHealth()
    {
        targetHealth = GameManager.Instance.battleEnemyProfile.health;
        currrentHealth = 0;
        UpdateHealth(0);

        emojiImage.sprite = emojis[0];
    }

    private void Awake()
    {
        TurnCounter.OnTurnUpdated += OnHealthUpdatedEachTurnCheck;
    }

    private void OnDestroy()
    {
        TurnCounter.OnTurnUpdated -= OnHealthUpdatedEachTurnCheck;
    }

    private void OnHealthUpdatedEachTurnCheck(bool obj)
    {
        UpdateHealth((int)player.playedCardDatas[player.playedCardDatas.Count - 1].totalValue);

        if (obj)
        {
            EndBattle();
        }
    }

    private void EndBattle()
    {
        if (currrentHealth >= targetHealth)
        {
            // Win
            GameProgressManager.Instance.UpdateEnemyProgress(GameManager.Instance.battleEnemyProfile.ID, true);
        }
        else
        {
            // Lose
            GameProgressManager.Instance.UpdateEnemyProgress(GameManager.Instance.battleEnemyProfile.ID, true);
        }

        UIWindowManager.Instance.CloseAllWindow();
        BattleManager.Instance.EndBattle();
        GameManager.Instance.Reset();
        PlayerTrigger.Instance.ClearEnemy();

        StarterAssetsInputs.Instance.SetCursorState(!GameManager.Instance.isBeginBattle);
    }

    public void UpdateHealth(int newHealth)
    {
        currrentHealth = newHealth;

        float target = (float)currrentHealth / (float)targetHealth;
        DOTween.To(() => fill.fillAmount, x => fill.fillAmount = x, target, 0.1f).SetEase(Ease.InOutBounce);

        if (currrentHealth >= targetHealth)
        {
            // Player Win
            emojiImage.sprite = emojis[1];
            EndBattle();
        }
    }
}
