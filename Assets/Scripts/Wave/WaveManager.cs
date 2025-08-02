using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public WaveDisplayExample waveDisplayExample;
    public WaveDisplayPlayer waveDisplayPlayer;

    public AudioSource brokenRadio, radioOn;

    [SerializeField] float waitTime;

    public void startMinigame(WaveData wd)
    {
        waveDisplayExample.waveData = wd;
        radioOn.Play();
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
            brokenRadio.Play();
            yield return new WaitForSeconds(5f);
            brokenRadio.Stop();
            // more happening
            waveDisplayPlayer.resetSliders();
            PlayerMovement.unfreeze();
            this.gameObject.SetActive(false);
        }
    }
}
