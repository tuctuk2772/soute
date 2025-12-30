using UnityEngine;
using Cysharp.Threading.Tasks;
using System;
using System.Linq;

public class UniTaskEventManager : MonoBehaviour
{
    public static event Func<float, UniTask> moveSquare, moveSquareAgain, finishText;

    [SerializeField] private float cubeXOffset = 2f;

    private async void Start()
    {
        await CallAsyncEvent(moveSquare, cubeXOffset);
        await CallAsyncEvent(moveSquareAgain, cubeXOffset);
        await UniTask.Delay(TimeSpan.FromSeconds(1f));
        await CallAsyncEvent(finishText, cubeXOffset);
    }

    private static async UniTask CallAsyncEvent(
        Func<float, UniTask> asyncEvent, float cubeXOffset)
    {
        if (asyncEvent == null)
        {
            return;
        }

        var tasks = asyncEvent
            .GetInvocationList()
            .Cast<Func<float, UniTask>>()
            .Select(func => func(cubeXOffset));

        await UniTask.WhenAll(tasks);
    }
}
