using UnityEngine;

public class WaveDisplayExample : WaveDisplay
{
    public WaveData waveData;
    void Start()
    {
        DrawWave(waveData.neededAmplitude / 2.0f , waveData.neededWavelength /500.0f);
    }
}
