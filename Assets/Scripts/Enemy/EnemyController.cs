using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    [Header("Profile")]
    [SerializeField] private Profile _enemyProfile;
    public Profile EnemyProfile => _enemyProfile;

    [Header("User Interface")]
    [SerializeField] private Image _profileImage;
    [SerializeField] private TMP_Text _nameText;

    [Header("Status")]
    [SerializeField] private bool _isTrigger;
    public bool IsTrigger => _isTrigger;

    private EnemyAI _enemyAI;

    void Start()
    {
        _enemyAI = GetComponent<EnemyAI>();

        // Set Profile Image and Name
        _profileImage.sprite = _enemyProfile.sprite;
        _nameText.text = _enemyProfile.profileName;
    }

    public void SetTriggerEnter()
    {
        _isTrigger = true;
        _enemyAI.SetStopMove(true);
        GameManager.Instance.isStopPlayerMove = true;

        QuestionUIController.Instance.SetActiveContent(true);
        QuestionUIController.Instance.SetCanvasGroupAlpha(1);
        QuestionUIController.Instance.SetEnemyProfile(_enemyProfile);
        QuestionUIController.Instance.SetQuestion(_enemyProfile.dialogue);
    }

    // public void SetTriggerExit()
    // {
    //     QuestionUIController.Instance.SetCanvasGroupAlpha(0);
    //     DOVirtual.DelayedCall(GlobalConfig.DELAY_ALPHA_DEFAULT, () =>
    //     {
    //         QuestionUIController.Instance.SetActiveContent(false);
    //         _isTrigger = false;
    //     });
    // }
}
