using System;

public class MiscEvents
{
    public event Action onEvidenceCollected;

    public void EvidenceCollected()
    {
        if (onEvidenceCollected != null)
        {
            onEvidenceCollected();
        }
    }
}