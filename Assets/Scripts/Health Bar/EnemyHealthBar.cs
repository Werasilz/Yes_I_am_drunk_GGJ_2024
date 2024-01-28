using System;
using System.Collections;
using System.Collections.Generic;
using Lab1;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public Player player;
    public int targetHealth;
    public int currrentHealth;
    public Image fill;
    public void IntializeHealth()
    {
        targetHealth = GameManager.Instance.battleEnemyProfile.health;
        currrentHealth = 0;
    }

    private void Awake()
    {
        TurnCounter.OnTurnUpdated += OnHealthUpdatedEachTurnCheck;
    }

    private void Start()
    {
        IntializeHealth();
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
            if (currrentHealth >= targetHealth)
            {
                // Win
                GameProgressManager.Instance.UpdateEnemyProgress(GameManager.Instance.battleEnemyProfile.ID, true);
            }
            else
            {
                // Lose
                GameProgressManager.Instance.UpdateEnemyProgress(GameManager.Instance.battleEnemyProfile.ID, false);
            }

            UIWindowManager.Instance.CloseAllWindow();
            BattleManager.Instance.EndBattle();
            GameManager.Instance.Reset();
            PlayerTrigger.Instance.ClearEnemy();
        }
    }

    public void UpdateHealth(int newHealth)
    {
        currrentHealth = newHealth;

        fill.fillAmount = (float)currrentHealth / (float)targetHealth;

        if (currrentHealth >= targetHealth)
        {
            // Player Win
        }
        else
        {

        }
    }
}
