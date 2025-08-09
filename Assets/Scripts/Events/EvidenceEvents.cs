using System;

public class EvidenceEvents
{
    public event Action<int> onEvidenceGained;

    public void EvidenceGained(int evidence)
    {
        if (onEvidenceGained != null)
        {
            onEvidenceGained(evidence);
        }
    }

    public event Action<int> onEvidenceChange;
    public void EvidenceChange(int evidence)
    {
        if (onEvidenceChange != null)
        {
            onEvidenceChange(evidence);
        }
    }
}