using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using Yarn.Unity;

public class Bed : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private DialogueRunner dialogueRunner;
    [SerializeField] private GameObject player;
    [SerializeField] private string nodeName;

    private bool hasTriggered = false;


    [SerializeField] InMemoryVariableStorage variableStorage;

    public float day;

    void Awake()
    {
        variableStorage = GameObject.FindFirstObjectByType<InMemoryVariableStorage>();
    }

    void Start()
    {
        variableStorage.TryGetValue("$day", out day);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasTriggered)
        {
            Debug.Log("Triggerd");
            hasTriggered = true; 
        }
    }

    public void Sleep(InputAction.CallbackContext context)
    {
        
        if (hasTriggered == true && context.started)
        {
            variableStorage.SetValue("$day", day + 1);
            
        }
    }

    // Reset trigger when player leaves, allowing dialogue to trigger again if re-entering
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hasTriggered = false;
        }
    }

    
}
