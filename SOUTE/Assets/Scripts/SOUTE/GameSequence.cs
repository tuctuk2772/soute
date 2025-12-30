using UnityEngine;
using System.Collections.Generic;
using System;
using Cysharp.Threading.Tasks;

[CreateAssetMenu(fileName = "SOUTE Sequence", menuName = "SOUTE/Scriptable Object UniTask Event Sequence")]
public class GameSequence : ScriptableObject
{
    public List<SequenceEvent> sequenceEvents = new();

    public async UniTask Invoke()
    {
        UniTask previousTask = UniTask.CompletedTask;
        List<UniTask> parallelTasks = new();

        for (int i = 0; i < sequenceEvents.Count; i++)
        {
            SequenceEvent sequenceEvent = sequenceEvents[i];

            if (sequenceEvent == null)
            {
                continue;
            }

            switch (sequenceEvent.order)
            {
                case SOUTESOrdering.After:
                    {
                        await UniTask.WhenAll(parallelTasks);
                        parallelTasks.Clear();

                        UniTask task = ExecuteEvent(sequenceEvent);
                        parallelTasks.Add(task);
                        previousTask = task;
                        break;
                    }

                case SOUTESOrdering.With:
                    {
                        UniTask task = ExecuteEvent(sequenceEvent);
                        parallelTasks.Add(task);
                        previousTask = task;
                        break;
                    }

                case SOUTESOrdering.Wait:
                    {
                        await UniTask.WhenAll(parallelTasks);
                        parallelTasks.Clear();

                        await UniTask.Delay(TimeSpan.FromSeconds(sequenceEvent.duration.value));
                        previousTask = UniTask.CompletedTask;
                        break;
                    }
            }
        }

        await UniTask.WhenAll(parallelTasks);
    }

    private async UniTask ExecuteEvent(SequenceEvent sequenceEvent)
    {
        try
        {
            if (sequenceEvent.SOUTE == null)
            {
                return;
            }

            if (sequenceEvent.SOUTE.setFloat)
            {
                sequenceEvent.setFloatReference.value = sequenceEvent.setFloat;
            }

            await sequenceEvent.SOUTE.Invoke();
        }
        catch
        {
            Debug.LogError($"There was an issue executing event {sequenceEvent.SOUTE.name}");
            return;
        }
    }
}

[Serializable]
public class SequenceEvent
{
    public SOUTESOrdering order = SOUTESOrdering.After;
    public GameEvent SOUTE;
    public FloatReference duration;
    public float setFloat;
    public FloatVariable setFloatReference;
}

public enum SOUTESOrdering
{
    After, With, Wait
}