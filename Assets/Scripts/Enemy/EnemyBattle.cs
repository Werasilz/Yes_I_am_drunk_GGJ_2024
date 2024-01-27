using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBattle : MonoBehaviour
{
    [Header("Character")]
    [SerializeField] private GameObject[] _characters;

    void Start()
    {
        _characters[GameManager.Instance.enemyBattleID].SetActive(true);
    }
}
