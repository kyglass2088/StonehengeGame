using System;
using System.Collections.Generic;
using UnityEngine;

public enum ItemTag
{
    Zombi,
    FlyingStong,
    TargetStone
}
public class GameManager : MonoBehaviour
{
    public ProjectileSO projectileSO;
    public ZombieSpawner zombiSpawner;
    public TargetStoneManager targetStoneManager;
    public MainUI mainUI;

    private void Start()
    {
        //TargetStone.OnKnockDownEvent += TargetStone_OnKnockDownEvent;
        TargetStoneManager.OnStageClearEvent += OnStageClearEvent;
        FlyingStone.OnMissionComplete += FlyingStone_OnMissionComplete;

        targetStoneManager.CreateOneTargeStone();
        //zombiSpawner.SpawnZombi();

    }

    private void FlyingStone_OnMissionComplete()
    {
        mainUI.FlyingStone_OnMissionComplete();
    }

    private void OnStageClearEvent()
    {
        mainUI.OnStageClearEvent();
    }

    private void TargetStone_OnKnockDownEvent(StoneType type)
    {
        mainUI.TargetStone_OnKnockDownEvent((StoneType)type);
    }
}