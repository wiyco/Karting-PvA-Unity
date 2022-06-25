using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    // all checkpoints
    Transform checkPoints;

    int index;

    void Awake()
    {
        // rename checkpoints except "StartFinishLine"
        index = 0;
        checkPoints = this.GetComponentInChildren<Transform>();
        foreach (Transform cp in checkPoints)
        {
            if (cp.gameObject != this.gameObject)
            {
                if (cp.gameObject.name != "StartFinishLine")
                {
                    cp.gameObject.name = "" + index;
                    index++;
                    //Debug.Log(cp.gameObject.name);
                }
            }
        }
    }
}
