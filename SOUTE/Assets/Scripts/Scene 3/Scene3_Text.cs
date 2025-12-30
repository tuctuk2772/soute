using UnityEngine;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using TMPro;

public class Scene3_Text : MonoBehaviour
{
    private void OnEnable()
    {
        UniTaskEventManager.finishText += FinishText;
    }

    private void OnDisable()
    {
        UniTaskEventManager.finishText -= FinishText;
    }

    private async UniTask FinishText(float cubeXOffset)
    {
        cubeXOffset *= 2f;

        TextMeshProUGUI text = gameObject.GetComponent<TextMeshProUGUI>();

        text.rectTransform.localPosition = Vector3.zero;
        text.text = $"cubeXOffset = {cubeXOffset}";
        await text.transform.DOLocalMoveX(25f, 1f).ToUniTask();
    }
}
