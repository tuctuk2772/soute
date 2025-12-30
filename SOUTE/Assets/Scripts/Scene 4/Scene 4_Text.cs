using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class Scene4_Text : MonoBehaviour
{
    [SerializeField] private FloatVariable cubeXOffset;

    private void OnEnable()
    {
        UniTaskEventManagerwScriptableObjectVariables.finishText += FinishText;
    }

    private void OnDisable()
    {
        UniTaskEventManagerwScriptableObjectVariables.finishText -= FinishText;
    }

    private async UniTask FinishText()
    {
        TextMeshProUGUI text = gameObject.GetComponent<TextMeshProUGUI>();

        text.rectTransform.localPosition = Vector3.zero;
        text.text = $"cubeXOffset = {cubeXOffset.value}";
        await text.transform.DOLocalMoveX(25f, 1f).ToUniTask();
    }
}
