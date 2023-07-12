using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Visual_Setting : MonoBehaviour
{
    public Volume globalVolume;
    private ColorAdjustments colorGrading;
    private WhiteBalance whitebalancelayer;


    // Defining slider components to access it on the in-application UI

    public Slider contrast;
    
    public Slider saturation;

    public Slider temperature;

    public Slider tint;

    public Slider Hue;
    


    // Start is called before the first frame update
    void Start()
    {
        // Retrieve the global volume
        //globalVolume = GetComponent<Volume>();    

        VolumeProfile GlobalProfile = globalVolume.sharedProfile;

        // Retrieve the ColorGrading and white balance component from the volume's profile
        globalVolume.profile.TryGet(out colorGrading);
        globalVolume.profile.TryGet(out whitebalancelayer);

        // Set the below profiles to allow overriding its state
        colorGrading.contrast.overrideState = true;
        colorGrading.hueShift.overrideState = true;
        colorGrading.saturation.overrideState = true;

        whitebalancelayer.temperature.overrideState = true;
        whitebalancelayer.tint.overrideState = true;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //setting the new values on every update by calling it form the defined function.
        colorGrading.contrast.value = setcontrast();
        colorGrading.hueShift.value = setHueshift();
        colorGrading.saturation.value = setSaturation();

        whitebalancelayer.temperature.value = settemperature();
        whitebalancelayer.tint.value = settint();

    }


    //Color Adjustments

    //Defining Get and set functions to get the new value and set to the respective parameters.
    public void getcontrast(float slidervalue)
    {
        contrast.value = slidervalue;
    }

    public float setcontrast()
    {
        return contrast.value;
    }

    public void getHueshift(float slidervalue)
    {
        Hue.value = slidervalue;
    }

    public float setHueshift()
    {
        return Hue.value;
    }

    public void getSaturation(float slidervalue)
    {
        saturation.value = slidervalue;
    }

    public float setSaturation()
    {
        return saturation.value;
    }

    // white Balance

    public void gettemperature(float slidervalue)
    {
        temperature.value = slidervalue;
    }

    public float settemperature()
    {
        return temperature.value;
    }

    public void gettint(float slidervalue)
    {
        tint.value = slidervalue;
    }

    public float settint()
    {
        return tint.value;
    }

    // This function resets all the parameters to zero.
    public void Reset()
    {
        contrast.value = 0.0f;
        saturation.value = 0.0f;
        temperature.value = 0.0f;
        tint.value = 0.0f;
        Hue.value = 0.0f;
    }

}
