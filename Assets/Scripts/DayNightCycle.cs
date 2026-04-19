using System.Globalization;
using UnityEngine;
using Yarn.Unity;

public class DayNightCycke : MonoBehaviour
{
    [SerializeField]private Material skybox;
    [SerializeField] private DialogueRunner dialogueRunner;
    float transition = 0;
    public float time;
    public string scenario01 = "Name";

    public string scenario02 = "Name";

    public float startIntensity;
    public float endIntensity;

    [Min(1)] public int numberOfCellsBlendedPerFrame = 10;

    [SerializeField] GameObject light;
    [SerializeField] Vector3 start;
    [SerializeField] Vector3 end;

    bool goneToBed;
    UnityEngine.Rendering.ProbeReferenceVolume probeRefVolume;


    Quaternion startAngle;
    Quaternion endAngle;

    void Start()
    {
        skybox = RenderSettings.skybox;
        //transition = skybox.GetFloat("_CubemapTransition");

        probeRefVolume = UnityEngine.Rendering.ProbeReferenceVolume.instance;
        probeRefVolume.lightingScenario = scenario01;
        probeRefVolume.numberOfCellsBlendedPerFrame = numberOfCellsBlendedPerFrame;
    }

    // Update is called once per frame
    void Update()
    {
        if (transition < 1)
        {
            transition += Time.deltaTime / time;
        }
        else
        {
            transition = 1;
        }
        skybox.SetFloat("_CubemapTransition", transition);

        startAngle = Quaternion.Euler(start);
        endAngle = Quaternion.Euler(end);

        probeRefVolume.BlendLightingScenario(scenario02, transition);

        light.transform.rotation = Quaternion.Lerp(startAngle, endAngle, transition);
        light.GetComponent<Light>().intensity = Mathf.Lerp(startIntensity, endIntensity, transition);

        if (dialogueRunner.VariableStorage != null)
        {
            
            dialogueRunner.VariableStorage.TryGetValue("$gone_to_bed", out goneToBed);
            if (goneToBed == true)
            {
                Debug.Log("Slept");
                ResetSky();
                goneToBed = false;
            }

            
            
        } 
    }


    void ResetSky()
    {   
        dialogueRunner.VariableStorage.SetValue("$gone_to_bed", false);
        goneToBed = false;

        probeRefVolume.BlendLightingScenario(scenario01, 0);
        light.transform.rotation = Quaternion.Lerp(endAngle, startAngle, 0);
        light.GetComponent<Light>().intensity = Mathf.Lerp(endIntensity, startIntensity, 0);
        transition = 0;
    }
}
