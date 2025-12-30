using UnityEngine;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;

public class Scene3_Cube : MonoBehaviour
{
    private void OnEnable()
    {
        UniTaskEventManager.moveSquare += MoveSquare;
        UniTaskEventManager.moveSquareAgain += MoveSquareAgain;
    }

    private void OnDisable()
    {
        UniTaskEventManager.moveSquare -= MoveSquare;
        UniTaskEventManager.moveSquareAgain -= MoveSquareAgain;
    }

    private async UniTask MoveSquare(float cubeXOffset)
    {
        gameObject.transform.localPosition = Vector3.zero;
        await gameObject.transform.DOMoveX(cubeXOffset, 1f).ToUniTask();
    }

    private UniTask MoveSquareAgain(float cubeXOffset)
    {
        cubeXOffset *= 2f;
        gameObject.transform.position = new Vector3(cubeXOffset, 0f, 0f);
        return UniTask.CompletedTask;
    }
}
