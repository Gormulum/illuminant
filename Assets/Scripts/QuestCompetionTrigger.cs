using UnityEngine;
using Yarn.Unity;

public class QuestCompetionTrigger : MonoBehaviour
{
    [SerializeField] private DialogueRunner dialogueRunner;
    [SerializeField] private GameObject questIndicator;
    [SerializeField] private string variableName;
    [SerializeField] private string questName;
    [SerializeField] private string nodeName;

    bool questActive;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CompleteQuest();
        }

        if (nodeName != null)
        {
            dialogueRunner.StartDialogue(nodeName);
        }

    }

    void Update()
    {
        dialogueRunner.VariableStorage.TryGetValue(questName, out questActive);

        if (questActive == true && questIndicator != null)
        {
            questIndicator.SetActive(true);
        }
    }

    void CompleteQuest()
    {
       // Increment the count variable in Yarn
        if (dialogueRunner.VariableStorage != null)
        {
            
            if (questActive == true)
            {
                dialogueRunner.VariableStorage.SetValue(variableName, true);
                Debug.Log("Completed Quest");
                Destroy(gameObject);
            }
            
        } 
    }
}
