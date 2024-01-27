using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIGameplayManager : MonoBehaviour
{
    public bool playerTurn;
    public GameObject Arrow;
    public Transform arrowPlayerPos;
    public Transform arrowEnemyPos;
    public ValueText[] valueTexts;

    public static System.Action<int, string, string, string, string> OnDisplayValue = delegate { };
    [SerializeField] private float arrowLerpSpeed;

    private void Awake()
    {
        OnDisplayValue += DisplayCalculatedValue;
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
public class ValueText
{
    public TextMeshProUGUI persistantValueText;
    public TextMeshProUGUI stackBonusValueText;
    public TextMeshProUGUI calculateValueText;
    public TextMeshProUGUI totalValueText;
}
