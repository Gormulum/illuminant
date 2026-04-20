using UnityEngine;

public class EyeBallLookAt : MonoBehaviour
{
    [SerializeField] private GameObject player;

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.LookAt(player.transform);
    }
}
