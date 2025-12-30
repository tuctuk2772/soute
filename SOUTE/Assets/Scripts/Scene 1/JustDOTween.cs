using UnityEngine;
using DG.Tweening;
using TMPro;

public class JustDOTween : MonoBehaviour
{
    [SerializeField] private GameObject cube;
    [SerializeField] private float cubeXOffset = 2f;

    [Space(10)]
    [SerializeField] private TextMeshProUGUI text;

    private void Start()
    {
        Sequence start = DOTween.Sequence();

        //this sequence won't work the way it's written out,
        //  because some of the tasks are synchronous and DOTween is async

        start.Append(MoveSquare()); //square moves cubeXOffset amount using DOTween
        start.AppendCallback(() => MoveSquareAgain()); //cubeXOffset gets multiplied by 2, and square teleports to new cubeXOffset
        start.AppendInterval(1f); //wait 1 second
        start.Append(FinishText()); //text moves and displays current cubeXOffset (cubeXOffset * 2)
    }

    private Tween MoveSquare()
    {
        cube.transform.localPosition = Vector3.zero;
        return cube.transform.DOMoveX(cubeXOffset, 1f);
    }

    private void MoveSquareAgain()
    {
        cubeXOffset *= 2f;
        cube.transform.position = new Vector3(cubeXOffset, 0f, 0f);
    }

    private Tween FinishText()
    {
        text.rectTransform.localPosition = Vector3.zero;
        text.text = $"cubeXOffset = {cubeXOffset}";
        return text.rectTransform.DOLocalMoveX(25f, 1f);
    }
}
