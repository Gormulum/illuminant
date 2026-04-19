using UnityEngine;
using Yarn.Unity;

public class QuestCompetionTrigger : MonoBehaviour
{
    [SerializeField] private DialogueRunner dialogueRunner;
    [SerializeField] private string variableName;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CompleteQuest();
        }
    }

    void CompleteQuest()
    {
       // Increment the count variable in Yarn
        if (dialogueRunner.VariableStorage != null)
        {
            dialogueRunner.VariableStorage.SetValue(variableName, true);
            Debug.Log("Completed Quest");
            Destroy(gameObject);
        } 
    }
}
