using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WaveDisplay : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public int waveWidth = 800;
    public int waveHeight = 10; 

    protected void DrawWave(float amplitude, float wavelength)
    {

        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            float x = i / (float)(lineRenderer.positionCount - 1) * waveWidth;
            float y = Mathf.Sin(x * wavelength * Mathf.PI * 2) * amplitude * waveHeight;
            lineRenderer.SetPosition(i, new Vector3(x, y, 0));
        }
    }
}
