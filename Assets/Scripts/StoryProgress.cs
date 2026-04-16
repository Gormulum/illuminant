using UnityEngine;
using UnityEngine.InputSystem;
using Yarn.Unity;


public class StoryProgress : MonoBehaviour
{
    [SerializeField] GameObject Fisherman1;
    [SerializeField] GameObject Fisherman2;
    [SerializeField] GameObject CorruptedFisherman;
    [SerializeField] GameObject ParanoidFisherman;
    [SerializeField] GameObject Shipwreck;
    [SerializeField] GameObject Eyeball;

    [SerializeField] InMemoryVariableStorage variableStorage;

    public float day;
    public float corruption;

    void Awake()
    {
        variableStorage = GameObject.FindFirstObjectByType<InMemoryVariableStorage>();
    }
    void Start()
    {



        
    }

    void Update()
    {
        variableStorage.TryGetValue("$day", out day);
        variableStorage.TryGetValue("$corruption_level", out corruption);
    }

    public void Progress(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (day == 0 && corruption == 0)
            {
                Fisherman1.SetActive(true);
            }

            if (day == 1)
            {
                if (corruption == 0)
                {
                    Fisherman2.SetActive(true);
                }
                else if (corruption == 1)
                {
                    Shipwreck.SetActive(true);
                }
            }

            if (day == 2)
            {
                CorruptedFisherman.SetActive(true);
            }

            if (day == 3)
            {
                if (corruption == 0)
                {

                }
                else
                {
                    Eyeball.SetActive(true);
                }
            } 
            
            
            if (day == 4)
            {
                ParanoidFisherman.SetActive(true);
            }
        }
        
    }
}
