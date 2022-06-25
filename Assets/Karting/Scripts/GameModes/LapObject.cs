using UnityEngine;

/// <summary>
/// This class inherits from TargetObject and represents a LapObject.
/// </summary>
public class LapObject : TargetObject
{
    [Header("LapObject")]
    [Tooltip("Is this the first/last lap object?")]
    public bool finishLap;

    [HideInInspector]
    public bool lapOverNextPass;

    // +++
    KartLapManager m_kartLapManager;
    ObjectiveCompleteLapsManager m_objectiveCompleteLapsManager;
    int lap;

    // +++
    void Awake()
    {
        m_objectiveCompleteLapsManager = FindObjectOfType<ObjectiveCompleteLapsManager>();
    }

    void Start()
    {
        Register();
    }
    
    void OnEnable()
    {
        lapOverNextPass = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        // +++ other.CompareTag("Player") -> (other.CompareTag("Player") || other.CompareTag("Agent"))
        if (!((layerMask.value & 1 << other.gameObject.layer) > 0 && (other.CompareTag("Player") || other.CompareTag("Agent"))))
            return;

        //Objective.OnUnregisterPickup?.Invoke(this);
        // +++
        m_kartLapManager = other.GetComponentInParent<KartLapManager>();
        m_kartLapManager.SetKartLapCP(this.gameObject);
        if (this.gameObject.name == "StartFinishLine")
        {
            lap = m_kartLapManager.GetKartLap();
            m_objectiveCompleteLapsManager.UpdateLap(lap, other.tag);
        }
        //if (other.CompareTag("Player"))
        //{
        //    m_objectiveCompleteLapsManager.UpdatePlayerLap(lap);
        //}
        //else if (other.CompareTag("Agent"))
        //{
        //    m_objectiveCompleteLapsManager.UpdateAgentLap(lap);
        //}
        //else
        //{

        //}
    }
}
