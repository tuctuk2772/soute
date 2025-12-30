using Cysharp.Threading.Tasks;
using System;
using System.Linq;
using UnityEngine;

public class UniTaskEventManagerwScriptableObjectVariables : MonoBehaviour
{
    public static event Func<UniTask> moveSquare, moveSquareAgain, finishText;

    [SerializeField] private FloatVariable cubeXOffset;

    private async void Start()
    {
        await CallAsyncEvent(moveSquare);
        await CallAsyncEvent(moveSquareAgain);
        await UniTask.Delay(TimeSpan.FromSeconds(1f));
        await CallAsyncEvent(finishText);
    }

    private static async UniTask CallAsyncEvent(
        Func<UniTask> asyncEvent)
    {
        if (asyncEvent == null)
        {
            return;
        }

        var tasks = asyncEvent
            .GetInvocationList()
            .Cast<Func<UniTask>>()
            .Select(func => func());

        await UniTask.WhenAll(tasks);
    }
}
