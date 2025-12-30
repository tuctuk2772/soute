using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class Text_Final : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] private GameEvent finishText;

    [Header("Variables")]
    [SerializeField] private FloatVariable cubeXOffset;

    private void OnEnable()
    {
        finishText.SOUTE += FinishText;
    }

    private void OnDisable()
    {
        finishText.SOUTE -= FinishText;
    }

    private async UniTask FinishText()
    {
        TextMeshProUGUI text = gameObject.GetComponent<TextMeshProUGUI>();

        text.rectTransform.localPosition = Vector3.zero;
        text.text = $"cubeXOffset = {cubeXOffset.value}";
        await text.transform.DOLocalMoveX(25f, 1f).ToUniTask();
    }
}
