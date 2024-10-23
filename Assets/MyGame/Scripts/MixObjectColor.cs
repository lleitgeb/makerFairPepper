using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum ColorRGB
{
    Red,
    Green,
    Blue,
}

public class MixObjectColor : MonoBehaviour
{
    private Color32[] taskColors = new Color32[]
    {
        new Color32(253, 196, 4, 255), //fdch04 gelb bear
        new Color32(90, 160, 216, 255), //5aa0d8 blau cat
        new Color32(0, 167, 102, 255), //00a766 grün owl
        new Color32(217, 75, 50, 255), //d94b32 rot one
        new Color32(186, 198, 52, 255), //bac634 lime two
        new Color32(147, 114, 177, 255), //fdch04 lila three
        new Color32(0, 0, 0, 255), //000000 black None
    };

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

    private Color32 mixedColor;

    private TaskToDo currentTask = TaskToDo.None;
    [SerializeField] private GameObject taskInputCanvas;

    // Start is called before the first frame update
    private void Start()
    {
        pepperManager = GameObject.FindObjectOfType<PepperOnlyManager>();
        objMaterial = new Material(Shader.Find("Universal Render Pipeline/Lit"));
        mixedColor = new Color32(0, 0, 0,255);
        objMaterial.color = mixedColor;
        kuebelObject.GetComponent<MeshRenderer>().material = objMaterial;
        currentParticleSystem = null;
        redParticleSystem.Stop();
        blueParticleSystem.Stop();
        greenParticleSystem.Stop();

        red.fillAmount = inputMarkerFull;
        blue.fillAmount = inputMarkerFull;
        green.fillAmount = inputMarkerFull;

        taskInputCanvas.SetActive(true);
    }

    public void StartParticles(ColorRGB color)
    {
        Debug.Log("Start Particles!!!!");
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

        Debug.Log("in decrase" + color);
        switch (color)
        {
            case ColorRGB.Red:
                if(red.fillAmount <= inputMarkerEmpty)
                {
                    Debug.Log("in REd");
                    red.fillAmount += stepSize;
                    int result = 255 - (byte)MapRange(red.fillAmount);
                    result = Mathf.Clamp(result, 0, 255);
                    r = (byte)result;
                }
                break;
            case ColorRGB.Blue:
                if (blue.fillAmount <= inputMarkerEmpty)
                {
                    Debug.Log("in Blue");
                    blue.fillAmount += stepSize;
                    int result = 255 - (byte)MapRange(blue.fillAmount);
                    result = Mathf.Clamp(result, 0, 255);
                    b = (byte)result;
                }
                break;
            case ColorRGB.Green:
                if (green.fillAmount <= inputMarkerEmpty)
                {
                    Debug.Log("in Green");
                    green.fillAmount += stepSize;
                    int result = 255 - (byte)MapRange(green.fillAmount);
                    result = Mathf.Clamp(result, 0, 255);
                    g = (byte)result;
                }
                break;
        }

        mixedColor = new Color32(r, g, b, 255);
        Debug.Log("----------------------Color: "+mixedColor);
        objMaterial.color = mixedColor;
    }

    public float MapRange(float inputValue)
    {
        return outputMarkerFull + (inputValue - inputMarkerFull) * (outputMarkerEmpty - outputMarkerFull) / (inputMarkerEmpty - inputMarkerFull);
    }

    private void Update()
    {
        if (redParticleSystem.isPlaying && PipeEmpty(ColorRGB.Red)) redParticleSystem.Stop();
        if (greenParticleSystem.isPlaying && PipeEmpty(ColorRGB.Green)) greenParticleSystem.Stop();
        if (blueParticleSystem.isPlaying && PipeEmpty(ColorRGB.Blue)) blueParticleSystem.Stop();
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //EvaluatePoints();
            mixedColor = taskColors[(int)TaskToDo.None];
            objMaterial.color = mixedColor;
            red.fillAmount = blue.fillAmount = green.fillAmount = inputMarkerFull;
            //Debug.Log("RESET PIPES");
            //pepperManager.EnableTaskCanvas();
            //taskInputCanvas.SetActive(true);
        }
    }

    public void EnableTaskCanvas()
    {
        taskInputCanvas.SetActive(true);

    }


  

    public void SetTask(int task) 
    {
        currentTask = (TaskToDo)task;
        taskInputCanvas.SetActive(false);
    }


}
