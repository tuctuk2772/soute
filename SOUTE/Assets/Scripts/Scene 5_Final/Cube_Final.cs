using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class Cube_Final : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] private GameEvent moveSquare;
    [SerializeField] private GameEvent moveSquareAgain;

    [Header("Variables")]
    [SerializeField] private FloatVariable cubeXOffset;

    private void OnEnable()
    {
        moveSquare.SOUTE += MoveSquare;
        moveSquareAgain.SOUTE += MoveSquareAgain;
    }

    private void OnDisable()
    {
        moveSquare.SOUTE -= MoveSquare;
        moveSquareAgain.SOUTE -= MoveSquareAgain;
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
