using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestionUIController : SceneSingleton<QuestionUIController>
{
    [SerializeField] private GameObject _content;
    [SerializeField] private CanvasGroup _canvasGroup;

    [Header("Enemy Profile")]
    [SerializeField] private Image _enemyImage;
    [SerializeField] private TMP_Text _enemyNameText;

    [Header("Question")]
    [SerializeField] private QuestionData _questionData;
    [SerializeField] private TMP_Text _questionText;

    protected override void Awake()
    {
        base.Awake();
    }

    public void SetActiveContent(bool isActive)
    {
        _content.SetActive(isActive);
    }

    public void SetQuestion()
    {
        int randomQuestion = Random.Range(0, _questionData.questions.Length);
        _questionText.text = _questionData.questions[randomQuestion];
    }

    public void SetEnemyProfile(Profile enemyProfile)
    {
        _enemyImage.sprite = enemyProfile.sprite;
        _enemyNameText.text = enemyProfile.profileName;
    }

    public void SetCanvasGroupAlpha(float alphaTarget)
    {
        Utility.SetCanvasGroupAlpha(_canvasGroup, alphaTarget);
    }
}
