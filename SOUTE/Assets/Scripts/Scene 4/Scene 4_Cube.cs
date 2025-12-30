using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class Scene4_Cube : MonoBehaviour
{
    [SerializeField] private FloatVariable cubeXOffset;

    private void OnEnable()
    {
        UniTaskEventManagerwScriptableObjectVariables.moveSquare += MoveSquare;
        UniTaskEventManagerwScriptableObjectVariables.moveSquareAgain += MoveSquareAgain;
    }

    private void OnDisable()
    {
        UniTaskEventManagerwScriptableObjectVariables.moveSquare -= MoveSquare;
        UniTaskEventManagerwScriptableObjectVariables.moveSquareAgain -= MoveSquareAgain;
    }

    private async UniTask MoveSquare()
    {
        gameObject.transform.localPosition = Vector3.zero;
        await gameObject.transform.DOMoveX(cubeXOffset.value, 1f).ToUniTask();
    }

    private UniTask MoveSquareAgain()
    {
        cubeXOffset.value *= 2f;
        gameObject.transform.position = new Vector3(cubeXOffset.value, 0f, 0f);
        return UniTask.CompletedTask;
    }
}
