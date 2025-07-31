using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public WaveDisplayExample waveDisplayExample;
    public WaveDisplayPlayer waveDisplayPlayer;


    public void checkSolved()
    {
        if (waveDisplayPlayer.amplitudeSlider.value == waveDisplayExample.waveData.neededAmplitude
        && waveDisplayPlayer.wavelengthSlider.value == waveDisplayExample.waveData.neededWavelength)
        {
            // more happening
            this.gameObject.SetActive(false);
        }
    }
}
