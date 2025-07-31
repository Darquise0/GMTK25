using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class WaveManager : MinigameManager
{
    public WaveDisplayExample waveDisplayExample;
    public WaveDisplayPlayer waveDisplayPlayer;

    [SerializeField] float waitTime;

    new public void startMinigame(ScriptableObject wd)
    {
        waveDisplayExample.waveData = wd as WaveData;
    }

    public void checkSolved()
    {
        StartCoroutine(waitCheck());
    }
    IEnumerator waitCheck()
    {
        yield return new WaitForSeconds(waitTime);
        if (waveDisplayPlayer.amplitudeSlider.value == waveDisplayExample.waveData.neededAmplitude
        && waveDisplayPlayer.wavelengthSlider.value == waveDisplayExample.waveData.neededWavelength)
        {
            // more happening
            waveDisplayPlayer.resetSliders();
            this.gameObject.SetActive(false);
        }
    }
}
