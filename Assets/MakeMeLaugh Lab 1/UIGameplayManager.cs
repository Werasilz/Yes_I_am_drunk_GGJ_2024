using Lab1;
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

    public static System.Action<int, string, string, string, string> OnDisplayValue = delegate { };
    [SerializeField] private float arrowLerpSpeed;
    [SerializeField] Player player;

    [Header("Hint")]
    public TMP_Text hintPersistText;

    protected override void Awake()
    {
        base.Awake();

        OnDisplayValue += DisplayCalculatedValue;
    }

    public void Initialize()
    {
        turnCounter.InitializeTurnLimit(GameManager.Instance.battleEnemyProfile.turnLimit);
        OnDisplayValue?.Invoke(0, "0", "0", "0", "0");
        ClearHintText();

        // Cursor.lockState = CursorLockMode.None;
        // Cursor.visible = true;
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

    public void ClearHintText()
    {
        if (player.playedCardDatas.Count - 1 >= 0)
        {
            hintPersistText.text = player.playedCardDatas[player.playedCardDatas.Count - 1].persistantValue.ToString();
            valueTexts[0].persistantValueText.text = player.playedCardDatas[player.playedCardDatas.Count - 1].persistantValue.ToString();
        }
        else
        {

            hintPersistText.text = player.startPersistantValue.ToString();
            valueTexts[0].persistantValueText.text = player.startPersistantValue.ToString();
        }

        valueTexts[0].stackBonusValueText.text = "0";
        valueTexts[0].calculateValueText.text = "0";
    }
}

[System.Serializable]
public class TurnCounter
{
    public int turnLimit = 5;
    public int currentTurn = 1;

    public TextMeshProUGUI turnLimitText;
    public static System.Action<bool> OnTurnUpdated = delegate { };
    [SerializeField] EnemyHealthBar enemyHealth;

    public void InitializeTurnLimit(int turnLimit)
    {
        currentTurn = 1;
        this.turnLimit = turnLimit;
        UpdateTurnUI();
    }
    public void TurnUpdate()
    {
        currentTurn++;

        UpdateTurnUI();

        bool isEndByTurnLimit = currentTurn > turnLimit;
        bool isEndByFullScore = enemyHealth.currrentHealth >= enemyHealth.targetHealth;

        if (isEndByFullScore || isEndByTurnLimit)
            OnTurnUpdated?.Invoke(true);
        else
            OnTurnUpdated?.Invoke(false);
    }

    public void UpdateTurnUI()
    {
        turnLimitText.text = $"{currentTurn} / {turnLimit}";
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
