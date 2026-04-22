using System;
using Unity.Behavior;
using UnityEngine;
using Yarn.Unity;

public class YarnRunOnEnable : MonoBehaviour
{
    [SerializeField] private DialogueRunner dialogueRunner;
    [SerializeField] string nodeName;

    void Start()
    {
        dialogueRunner.StartDialogue(nodeName);
    }

    
}
