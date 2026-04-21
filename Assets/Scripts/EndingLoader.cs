using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class EndingLoader : MonoBehaviour
{
    [SerializeField] InMemoryVariableStorage variableStorage;

    bool good;
    bool murdered;
    bool corrupted;
    void Start()
    {
        variableStorage = GameObject.FindFirstObjectByType<InMemoryVariableStorage>();
    }

    // Update is called once per frame
    void Update()
    {
        variableStorage.TryGetValue("$murdered_ending", out murdered);
        variableStorage.TryGetValue("$corruption_ending", out corrupted);
        variableStorage.TryGetValue("$good_ending", out good);

        if (murdered == true)
        {
            SceneManager.LoadScene(1);
        }

        if (corrupted == true)
        {
            SceneManager.LoadScene(2);
        }

        if (good == true)
        {
            SceneManager.LoadScene(3);
        }
    }
}
