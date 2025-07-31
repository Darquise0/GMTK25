using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public WaveDisplayExample waveDisplayExample;
    public WaveDisplayPlayer waveDisplayPlayer;

    [SerializeField] float waitTime;

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
            this.gameObject.SetActive(false);
        }
    }
}
