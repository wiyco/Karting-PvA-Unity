using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveCompleteLapsManager : MonoBehaviour
{
    ObjectiveCompleteLaps[] m_objectiveCompleteLaps;
    ObjectiveCompleteLaps oclPlayer;
    ObjectiveCompleteLaps oclAgent;
    bool setPlayer = false;
    bool setAgent = false;

    void Awake()
    {
        m_objectiveCompleteLaps = FindObjectsOfType<ObjectiveCompleteLaps>();

        foreach (ObjectiveCompleteLaps ocl in m_objectiveCompleteLaps)
        {
            if (setPlayer == true && setAgent == true)
                break;
            if (ocl.CompareTag("Player"))
            {
                oclPlayer = ocl;
                setPlayer = true;
            }
            else if (ocl.CompareTag("Agent"))
            {
                oclAgent = ocl;
                setAgent = true;
            }
        }
        if (setPlayer == false || setAgent == false)
            Debug.LogWarning("WARNING: Couldn't set/get ObjectiveCompleteLaps of Player or Agent.");
    }

    public void UpdateLap(int lap, string kartName)
    {
        if (kartName == "Player")
        {
            oclPlayer.UpdateCurrentLap(lap, kartName);
        }
        else if (kartName == "Agent")
        {
            oclAgent.UpdateCurrentLap(lap, kartName);
        }
        
    }

    //public void UpdatePlayerLap(int lap)
    //{
    //    oclPlayer.UpdateCurrentLap(lap);
    //}

    //public void UpdateAgentLap(int lap)
    //{
    //    oclAgent.UpdateCurrentLap(lap);
    //}

}
