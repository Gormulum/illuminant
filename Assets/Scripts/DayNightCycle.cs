using UnityEngine;

public class DayNightCycke : MonoBehaviour
{
    [SerializeField]private Material skybox;
    float transition = 0;
    public float time;
    public string scenario01 = "Name";

    public string scenario02 = "Name";

    [Min(1)] public int numberOfCellsBlendedPerFrame = 10;
    [SerializeField] GameObject light;
    [SerializeField] Vector3 start;
    [SerializeField] Vector3 end;


    UnityEngine.Rendering.ProbeReferenceVolume probeRefVolume;


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

        Quaternion startAngle = Quaternion.Euler(start);
        Quaternion endAngle = Quaternion.Euler(end);

        probeRefVolume.BlendLightingScenario(scenario02, transition);

        light.transform.rotation = Quaternion.Lerp(startAngle, endAngle, transition);
    }
}
