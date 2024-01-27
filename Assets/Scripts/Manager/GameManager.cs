using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public bool isNPCAskQuestion = false;
    public bool isBeginBattle = false;
    public int enemyBattleID;

    protected override void Awake()
    {
        base.Awake();
    }

    public void Reset()
    {
        isNPCAskQuestion = false;
        isBeginBattle = false;
    }
}
