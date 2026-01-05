using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using TMPro;
using UnityEngine;

public class UniTaskDOTween : MonoBehaviour
{
    [SerializeField] private GameObject cube;
    [SerializeField] private float cubeXOffset = 2f;

    [Space(10)]
    [SerializeField] private TextMeshProUGUI text;

    private async void Start()
    {
        await MoveSquare(); //square moves cubeXOffset amount using DOTween
        await MoveSquareAgain(); //cubeXOffset gets multiplied by 2, and square teleports to new cubeXOffset
        await UniTask.Delay(TimeSpan.FromSeconds(1f)); //wait 1 second
        await FinishText(); //text moves and displays current cubeXOffset (cubeXOffset * 2)
    }

    private async UniTask MoveSquare()
    {
        cube.transform.localPosition = Vector3.zero;
        await cube.transform.DOMoveX(cubeXOffset, 1f).ToUniTask();
    }

    private UniTask MoveSquareAgain()
    {
        cubeXOffset *= 2f;
        cube.transform.position = new Vector3(cubeXOffset, 0f, 0f);
        return UniTask.CompletedTask;
    }

    private async UniTask FinishText()
    {
        text.rectTransform.localPosition = Vector3.zero;
        text.text = $"cubeXOffset = {cubeXOffset}";
        await text.transform.DOLocalMoveX(25f, 1f).ToUniTask();
    }
}
