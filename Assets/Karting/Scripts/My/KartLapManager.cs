using UnityEngine;

public class KartLapManager : MonoBehaviour
{
    int lap;
    int checkpoint;
    int checkpointCount;
    int colIndex;

    void Awake()
    {
        lap = 0;
        checkpoint = -1;
        // count all checkpoints(tag) except "StartFinishLine"
        checkpointCount = GameObject.FindGameObjectsWithTag("Checkpoint").Length - 2;
    }

    public void SetKartLapCP(GameObject g_lapObject)
    {
        if (g_lapObject.CompareTag("Checkpoint"))
        {
            if (g_lapObject.name == "StartFinishLine")
            {
                if (checkpoint == checkpointCount)
                {
                    lap++;
                    checkpoint = 0;
                }
                if (this.CompareTag("Player"))
                    KartGame.Track.TimeDisplay.OnUpdateLap();
            }
            else
            {
                colIndex = int.Parse(g_lapObject.name);
                if (colIndex == checkpoint + 1)
                    checkpoint++;
            }
        }
        //Debug.Log("checkpoint: " + checkpoint);
        //Debug.Log("lap: " + lap);
    }

    public int GetKartLap()
    {
        return lap;
    }
    public int GetKartCheckpoint()
    {
        return checkpoint;
    }
}
