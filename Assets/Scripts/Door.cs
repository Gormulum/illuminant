using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using Yarn.Unity;

public class Door : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private DialogueRunner dialogueRunner;
    [SerializeField] private GameObject teleportLocation;
    [SerializeField] private GameObject player;
    [SerializeField] private string nodeName;

    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasTriggered)
        {
            Debug.Log("Triggerd");
            hasTriggered = true; 
        }
    }

    public void Teleport(InputAction.CallbackContext context)
    {
        
        if (hasTriggered == true && context.started)
        {
            player.GetComponent<CharacterController>().enabled = false;
            player.transform.position = teleportLocation.transform.position;
            player.GetComponent<CharacterController>().enabled = true;
            hasTriggered = false;
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

    [YarnCommand("QuestComplete")]
    public void QuestComplete()
    {
        Debug.Log("Quest completed! You can add your quest completion logic here.");
        // You can add additional logic here, such as giving rewards, updating quest status, etc.
    }
}
