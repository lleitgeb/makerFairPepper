using UnityEngine;

public enum ColorRGB
{
    Red,
    Green,
    Blue,
}

public class MixObjectColor : MonoBehaviour
{
    private Material objMaterial;
    [SerializeField] private PepperOnlyManager pepperManager;
    [SerializeField] private GameObject kuebelObject;
    [SerializeField] private Liquid red, blue, green;
    [SerializeField] private ParticleSystem redParticleSystem, blueParticleSystem, greenParticleSystem;
    [SerializeField] private GameObject taskPoints;
    private ParticleSystem currentParticleSystem;

    // Werte für den Eingangsbereich
    private float inputMarkerFull = -0.34f;  // Voll
    private float inputMarkerEmpty = 1.63f;   // Leer

    [SerializeField] private float stepSize;

    // Werte für den Ausgangsbereich
    private float outputMarkerFull = 255f;   // Voll
    private float outputMarkerEmpty = 0f;     // Leer

    private Color32 mixedColor = Color.black;

    private ItemData peppersGhostData;


    // Start is called before the first frame update
    private void Start()
    {
        pepperManager = GameObject.FindObjectOfType<PepperOnlyManager>();
        peppersGhostData = Resources.Load<ItemData>("PeppersGhostData");

        objMaterial = new Material(Shader.Find("Universal Render Pipeline/Lit"));
        mixedColor = new Color32(0, 0, 0, 255);
        objMaterial.color = mixedColor;
        kuebelObject.GetComponent<MeshRenderer>().material = objMaterial;
        
        currentParticleSystem = null;
        redParticleSystem.Stop();
        blueParticleSystem.Stop();
        greenParticleSystem.Stop();

        FillPipes();
    }

    public Color32 GetPlayerMixedColor()
    {
        return mixedColor;
    }

    public void StartParticles(ColorRGB color)
    {
        switch (color)
        {
            case ColorRGB.Red:
                if (PipeEmpty(ColorRGB.Red)) return;
                currentParticleSystem = redParticleSystem;
                break;
            case ColorRGB.Blue:
                if (PipeEmpty(ColorRGB.Blue)) return;
                currentParticleSystem = blueParticleSystem;
                break;
            case ColorRGB.Green:
                if (PipeEmpty(ColorRGB.Green)) return;
                currentParticleSystem = greenParticleSystem;
                break;
        }

        if (currentParticleSystem.isPlaying)
        {
            currentParticleSystem.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }

        currentParticleSystem.Play();
    }

    public void StopParticles()
    {
        if (currentParticleSystem == null) return;

        if (currentParticleSystem.isPlaying)
        {
            currentParticleSystem.Stop();
            currentParticleSystem.Clear();
        }
    }
    private byte GetByte(byte channelByte)
    {
        int result = 255 - channelByte;
        result = Mathf.Clamp(result, 0, 255);
        return (byte)result;
    }

    public bool PipeEmpty(ColorRGB pipe)
    {
        switch (pipe)
        {
            case ColorRGB.Red:
                return red.fillAmount >= inputMarkerEmpty;
            case ColorRGB.Green:
                return green.fillAmount >= inputMarkerEmpty;
            case ColorRGB.Blue:
                return blue.fillAmount >= inputMarkerEmpty;
            default:
                return false;
        }
    }

    public void DecreaseFillState(ColorRGB color)
    {
        byte r = GetByte((byte)MapRange(red.fillAmount));
        byte g = GetByte((byte)MapRange(green.fillAmount));
        byte b = GetByte((byte)MapRange(blue.fillAmount));

        switch (color)
        {
            case ColorRGB.Red:
                if(red.fillAmount <= inputMarkerEmpty)
                {
                    red.fillAmount += stepSize;
                    int result = 255 - (byte)MapRange(red.fillAmount);
                    result = Mathf.Clamp(result, 0, 255);
                    r = (byte)result;
                }
                break;
            case ColorRGB.Blue:
                if (blue.fillAmount <= inputMarkerEmpty)
                {
                    blue.fillAmount += stepSize;
                    int result = 255 - (byte)MapRange(blue.fillAmount);
                    result = Mathf.Clamp(result, 0, 255);
                    b = (byte)result;
                }
                break;
            case ColorRGB.Green:
                if (green.fillAmount <= inputMarkerEmpty)
                {
                    green.fillAmount += stepSize;
                    int result = 255 - (byte)MapRange(green.fillAmount);
                    result = Mathf.Clamp(result, 0, 255);
                    g = (byte)result;
                }
                break;
        }

        mixedColor = new Color32(r, g, b, 255);
        objMaterial.color = mixedColor;
    }

    public float MapRange(float inputValue)
    {
        return outputMarkerFull + (inputValue - inputMarkerFull) * (outputMarkerEmpty - outputMarkerFull) / (inputMarkerEmpty - inputMarkerFull);
    }

    public void FillPipes()
    {
        red.fillAmount = blue.fillAmount = green.fillAmount = inputMarkerFull;
    }
    public void ResetPipeStation()
    {
        mixedColor = peppersGhostData.taskColors[(int)TaskToDo.None];
        objMaterial.color = mixedColor;
        FillPipes();
    }

    private void Update()
    {
        if (redParticleSystem.isPlaying && PipeEmpty(ColorRGB.Red)) redParticleSystem.Stop();
        if (greenParticleSystem.isPlaying && PipeEmpty(ColorRGB.Green)) greenParticleSystem.Stop();
        if (blueParticleSystem.isPlaying && PipeEmpty(ColorRGB.Blue)) blueParticleSystem.Stop();
    }
}
