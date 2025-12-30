using UnityEngine;

public class EventManager_Final : MonoBehaviour
{
    [SerializeField] private GameSequence sequenceExample;

    private async void Start()
    {
        await sequenceExample.Invoke();
    }
}
