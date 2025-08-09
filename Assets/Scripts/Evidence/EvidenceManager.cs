using UnityEngine;

public class EvidenceManager : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] private int startingEvidence = 0;

    public int currentEvidence { get; private set; }

    private void Awake()
    {
        currentEvidence = startingEvidence;
    }

    private void OnEnable()
    {
        GameEventsManager.instance.evidenceEvents.onEvidenceGained += EvidenceGained;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.evidenceEvents.onEvidenceGained -= EvidenceGained;
    }

    private void Start()
    {
        GameEventsManager.instance.evidenceEvents.EvidenceChange(currentEvidence);
    }

    private void EvidenceGained(int evidence)
    {
        currentEvidence += evidence;
        GameEventsManager.instance.evidenceEvents.EvidenceChange(currentEvidence);
    }
}