using UnityEngine;
using Yarn.Unity;
using Unity.Behavior;

public class BoatAIQuestCheck : MonoBehaviour
{
    [SerializeField] InMemoryVariableStorage variableStorage;
    [SerializeField] private string questName;

    BehaviorGraphAgent agent;

    bool quest;
    void Awake()
    {
        variableStorage = GameObject.FindFirstObjectByType<InMemoryVariableStorage>();
        agent = GetComponent<BehaviorGraphAgent>();
    }

    void Update()
    {
        variableStorage.TryGetValue(questName, out quest);

        if (quest == true)
        {
            agent.SetVariableValue<bool>("QuestEnd", true);
        }
    }
}
