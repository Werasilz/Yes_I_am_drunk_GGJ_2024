using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    [Header("Profile")]
    [SerializeField] private EnemyProfileData _enemyProfileData;

    [Header("User Interface")]
    [SerializeField] private Image _profileImage;
    [SerializeField] private TMP_Text _nameText;

    [Header("Status")]
    [SerializeField] private bool _isTrigger;

    void Start()
    {
        // Set Profile Image and Name
        int randomIndex = Random.Range(0, _enemyProfileData.profiles.Length);
        _profileImage.sprite = _enemyProfileData.profiles[randomIndex].sprite;
        _nameText.text = _enemyProfileData.profiles[randomIndex].profileName;
    }

    public void SetTrigger(bool isTrigger)
    {
        _isTrigger = isTrigger;
    }
}
