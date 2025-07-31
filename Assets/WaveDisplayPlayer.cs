using UnityEngine;
using UnityEngine.UI;

public class WaveDisplayPlayer : WaveDisplay
{
    public Slider amplitudeSlider;
    public Slider wavelengthSlider;

    public WaveDisplayExample example;

    public float currentAmp, currentWL;

    public void RedrawWave()
    {
        currentAmp = amplitudeSlider.value / 2;
        currentWL = wavelengthSlider.value / 500;
        DrawWave(currentAmp, currentWL);
    }
        
    void Start()
    {
        RedrawWave();
    }
}
