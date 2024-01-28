using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public bool isStopPlayerMove = false;
    public bool isBeginBattle = false;
    public Profile battleEnemyProfile;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        SoundManager.Instance.PlayMusic(0);
    }

    public void Reset()
    {
        isStopPlayerMove = false;
        isBeginBattle = false;
    }
}
