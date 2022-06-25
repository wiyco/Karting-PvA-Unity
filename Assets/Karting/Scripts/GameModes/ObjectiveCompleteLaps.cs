﻿using System.Collections;
using KartGame.Track;
using UnityEngine;

public class ObjectiveCompleteLaps : Objective
{
    
    [Tooltip("How many laps should the player complete before the game is over?")]
    public int lapsToComplete;

    [Header("Notification")]
    [Tooltip("Start sending notification about remaining laps when this amount of laps is left")]
    public int notificationLapsRemainingThreshold = 1;
    
    public int currentLap { get; private set; }

    // +++
    GameFlowManager m_GameFlowManager;

    void Awake()
    {
        currentLap = 0;
        
        // set a title and description specific for this type of objective, if it hasn't one
        if (string.IsNullOrEmpty(title))
            title = $"Complete {lapsToComplete} {targetName}s";

        // +++
        m_GameFlowManager = FindObjectOfType<GameFlowManager>();
    }

    IEnumerator Start()
    {
        TimeManager.OnSetTime(totalTimeInSecs, isTimed, gameMode);
        TimeDisplay.OnSetLaps(lapsToComplete);
        yield return new WaitForEndOfFrame();
        Register();
    }

    protected override void ReachCheckpoint(int remaining)
    {

        if (isCompleted)
            return;

        //currentLap++;

        int targetRemaining = lapsToComplete - currentLap;

        // update the objective text according to how many enemies remain to kill
        if (targetRemaining == 0)
        {
            // +++ change text
            CompleteObjective(string.Empty, GetUpdatedCounterAmount(),
                "Finish: " + title);
            // +++
            if (this.CompareTag("Player"))
            {
                m_GameFlowManager.EndGame(true);
            }
            else if (this.CompareTag("Agent"))
            {
                m_GameFlowManager.EndGame(false);
            }
        }
        else if (targetRemaining == 1)
        {
            // +++ change text (add title+": ")
            string notificationText = notificationLapsRemainingThreshold >= targetRemaining
                ? title + ": One " + targetName + " left"
                : string.Empty;
            UpdateObjective(string.Empty, GetUpdatedCounterAmount(), notificationText);
        }
        else if (targetRemaining > 1)
        {
            // create a notification text if needed, if it stays empty, the notification will not be created
            string notificationText = notificationLapsRemainingThreshold >= targetRemaining
                ? targetRemaining + " " + targetName + "s to collect left"
                : string.Empty;

            UpdateObjective(string.Empty, GetUpdatedCounterAmount(), notificationText);
        }

    }
    
    public override string GetUpdatedCounterAmount()
    {
        // +++ currentLap -> currentLap + 1
        return currentLap + 1 + " / " + lapsToComplete;
    }

    // +++
    public void UpdateCurrentLap(int lap, string kartName)
    {
        currentLap = lap;
        ReachCheckpoint(lap);
    }
}
