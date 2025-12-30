using UnityEngine;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "SOUTE", menuName = "SOUTE/Scriptable Object UniTask Event")]
public class GameEvent : ScriptableObject
{
    public event Func<UniTask> SOUTE;

    public bool setFloat;

    public async UniTask Invoke()
    {
        if (SOUTE == null)
        {
            return;
        }

        var handlers = SOUTE.GetInvocationList();
        List<UniTask> tasks = new();

        foreach (Func<UniTask> handler in handlers)
        {
            tasks.Add(handler.Invoke());
        }

        await UniTask.WhenAll(tasks);
    }
}
