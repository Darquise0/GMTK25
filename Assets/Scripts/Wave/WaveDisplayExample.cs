using UnityEngine;

public class WaveDisplayExample : WaveDisplay
{
    public WaveData waveData;
    void Update()
    {
        phase += Time.deltaTime * waveSpeed;
        DrawWave(waveData.neededAmplitude / 2.0f, waveData.neededWavelength / 500.0f);
    }
}
