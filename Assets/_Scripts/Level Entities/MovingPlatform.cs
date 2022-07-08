using UnityEngine;
using DG.Tweening;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Ease easing;
    [SerializeField] private Vector2 movingSpeed;
    [SerializeField] private float horizontalBorderOffset;
    [SerializeField] private float verticalMovementRange;
    [SerializeField][Range(0f, 1f)] private float verticalOrHorizontalProbability;

    private float startPos;
    private float endPos;

    void Start()
    {
        if (Random.Range(0f,1f) > verticalOrHorizontalProbability)
		{
            startPos = transform.position.y;

            endPos = startPos;
            endPos += verticalMovementRange;

            MoveVertically();
            
        }
        else
		{
            MoveHorizontally();
        }
    }

    private void MoveVertically()
    {
        float speed = Random.Range(movingSpeed.x, movingSpeed.y);

        transform.DOMoveY(endPos, speed)
            .SetEase(easing).OnComplete(() =>
            {
                transform.DOMoveY(startPos, speed)
                .SetEase(easing).OnComplete(() => MoveVertically());
            });
    }

    private void MoveHorizontally()
	{
        float levelWidth = FindObjectOfType<LevelGenerator>().LevelWidth;
        levelWidth -= horizontalBorderOffset;

        float speed = Random.Range(movingSpeed.x, movingSpeed.y);

        transform.DOMoveX(levelWidth, speed)
            .SetEase(easing).OnComplete(() =>
        {
            transform.DOMoveX(-levelWidth, speed)
            .SetEase(easing).OnComplete(() => MoveHorizontally());
        });
    }

    private void OnDestroy()
	{
        transform.DOKill();
	}
}
