using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private GameObject kuebelObject;
    [SerializeField] private Liquid red, blue, green;
    [SerializeField] private ParticleSystem redParticleSystem, blueParticleSystem, greenParticleSystem;
    private ParticleSystem currentParticleSystem;

    // Werte für den Eingangsbereich
    private float inputMin = -0.34f;  // Voll
    private float inputMax = 1.63f;   // Leer

    [SerializeField] private float stepSize;

    // Werte für den Ausgangsbereich
    private float outputMin = 255f;   // Voll
    private float outputMax = 0f;     // Leer

    private Color32 mixedColor;

    // Start is called before the first frame update
    private void Start()
    {
        objMaterial = new Material(Shader.Find("Universal Render Pipeline/Lit"));
        mixedColor = new Color32(0, 0, 0,255);
        objMaterial.color = mixedColor;
        kuebelObject.GetComponent<MeshRenderer>().material = objMaterial;
        currentParticleSystem = null;
        redParticleSystem.Stop();
        blueParticleSystem.Stop();
        greenParticleSystem.Stop();

        red.fillAmount = inputMin;
        blue.fillAmount = inputMin;
        green.fillAmount = inputMin;
    }

    public void StartParticles(ColorRGB color)
    {
        switch (color)
        {
            case ColorRGB.Red: 
                currentParticleSystem = redParticleSystem;
                break;
            case ColorRGB.Blue:
                currentParticleSystem = blueParticleSystem;
                break;
            case ColorRGB.Green:
                currentParticleSystem = greenParticleSystem;
                break;
        }
        
        if (!currentParticleSystem.isPlaying)
        {
            currentParticleSystem.Play();
            Debug.Log("Partikel gestartet");
        }
    }

    public void StopParticles()
    {
        if (currentParticleSystem == null) return;

        if (currentParticleSystem.isPlaying)
        {
            currentParticleSystem.Stop();
        }
    }
    private byte GetByte(byte channelByte)
    {
        int result = 255 - channelByte;
        result = Mathf.Clamp(result, 0, 255);
        return (byte)result;
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
                if(red.fillAmount <= inputMax)
                {
                    Debug.Log("in REd");
                    red.fillAmount += stepSize;
                    int result = 255 - (byte)MapRange(red.fillAmount);
                    result = Mathf.Clamp(result, 0, 255);
                    r = (byte)result;
                }
                break;
            case ColorRGB.Blue:
                if (blue.fillAmount <= inputMax)
                {
                    Debug.Log("in Blue");
                    blue.fillAmount += stepSize;
                    int result = 255 - (byte)MapRange(blue.fillAmount);
                    result = Mathf.Clamp(result, 0, 255);
                    b = (byte)result;
                }
                break;
            case ColorRGB.Green:
                if (green.fillAmount <= inputMax)
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
        return outputMin + (inputValue - inputMin) * (outputMax - outputMin) / (inputMax - inputMin);
    }






}
