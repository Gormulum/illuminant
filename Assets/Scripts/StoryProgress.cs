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

        Story();

        
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
            Story();
        }
        
    }

    public void Story()
    {
        if (day == 0)
        {
            Fisherman1.SetActive(true);
            //max 1
        }

        if (day == 1)
        {
            Fisherman1.SetActive(false);
            Fisherman2.SetActive(true);
            //max 2
        }

        if (day == 2)
        {
            Fisherman2.SetActive(false);
            if (corruption == 0)
            {

                CorruptedFisherman.SetActive(true);
                //max 1
            }
            else if (corruption == 1 || corruption == 2)
            {
                Shipwreck.SetActive(true);
                //max  3 min 1
            }

            //max 3 min 0
        }

        if (day == 3)
        {
            if (corruption == 0 || corruption == 1)
            {
                ParanoidFisherman.SetActive(true);
                //max 2 min 0
            }
            else if (corruption > 1 && Shipwreck.activeSelf == false)
            {
                Shipwreck.SetActive(true);
            }
            else if (corruption > 1 && Shipwreck.activeSelf == true)
            {
                Eyeball.SetActive(true);
            }
            else
            {
                ParanoidFisherman.SetActive(true);
            }
        }

        if (day == 4)
        {
            if (Eyeball.activeSelf)
            {
                Eyeball.SetActive(false);
                ParanoidFisherman.SetActive(true);
            }
            
            
            
        } 
        
        
    }
}
