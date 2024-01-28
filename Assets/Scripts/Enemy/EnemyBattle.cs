using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBattle : MonoBehaviour
{
    [SerializeField] private Profile _enemyProfile;

    [Header("Character")]
    [SerializeField] private GameObject[] _characters;


    void Start()
    {
        _enemyProfile = GameManager.Instance.battleEnemyProfile;
        _characters[_enemyProfile.ID].SetActive(true);
    }
}
