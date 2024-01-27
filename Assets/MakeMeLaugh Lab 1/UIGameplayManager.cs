using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIGameplayManager : SceneSingleton<UIGameplayManager>
{
    public bool playerTurn;
    public GameObject Arrow;
    public Transform arrowPlayerPos;
    public Transform arrowEnemyPos;
    public ValueText[] valueTexts;

    public TurnCounter turnCounter;

    public PointLimit pointLimit;

    public static System.Action<int, string, string, string, string> OnDisplayValue = delegate { };
    [SerializeField] private float arrowLerpSpeed;

    protected override void Awake()
    {
        base.Awake();

        OnDisplayValue += DisplayCalculatedValue;

        turnCounter.InitializeTurnLimit(GameManager.Instance.battleEnemyProfile.turnLimit);
    }

    private void Update()
    {
        Arrow.transform.position = Vector3.Lerp(Arrow.transform.position, playerTurn ? arrowPlayerPos.position : arrowEnemyPos.position, arrowLerpSpeed * Time.deltaTime);
    }

    private void OnDestroy()
    {
        OnDisplayValue -= DisplayCalculatedValue;
    }

    public void DisplayCalculatedValue(int uiIndex, string persistantValue, string stackBonusValue, string calculateValue, string totalValue)
    {
        valueTexts[uiIndex].persistantValueText.text = persistantValue;
        valueTexts[uiIndex].stackBonusValueText.text = stackBonusValue;
        valueTexts[uiIndex].calculateValueText.text = calculateValue;
        valueTexts[uiIndex].totalValueText.text = totalValue;
    }
}

[System.Serializable]
public class TurnCounter
{
    public int turnLimit = 5;
    public int currentTurn = 1;

    public TextMeshProUGUI turnLimitText;
    public static System.Action<bool> OnTurnUpdated = delegate { };

    public void InitializeTurnLimit(int turnLimit)
    {
        this.turnLimit = turnLimit;
        UpdateTurnUI();
    }
    public void TurnUpdate()
    {
        currentTurn++;

        UpdateTurnUI();

        bool isEndByTurnLimit = currentTurn > turnLimit;

        OnTurnUpdated?.Invoke(isEndByTurnLimit);
    }

    public void UpdateTurnUI()
    {
        turnLimitText.text = $"{currentTurn} / {turnLimit}";
    }
}

[System.Serializable]
public class PointLimit
{
    public float goalPoint;
    public float currentPoint;

    public static System.Action<bool> OnPointUpdated = delegate { };
    public void SetCurrentPoint(float newCurrentPoint)
    {
        currentPoint = newCurrentPoint;

        OnPointUpdated?.Invoke(true);
    }
}

[System.Serializable]
public class ValueText
{
    public TextMeshProUGUI persistantValueText;
    public TextMeshProUGUI stackBonusValueText;
    public TextMeshProUGUI calculateValueText;
    public TextMeshProUGUI totalValueText;
}
