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
    private Transform platformTransform;

    void OnEnable()
    {
        if (Random.Range(0f, 1f) > verticalOrHorizontalProbability)
		{
            Invoke("MoveVertically", 0.3f);
        }
        else
		{
            MoveHorizontally();
        }
    }
    private void OnDisable()
    {
        platformTransform.DOKill();
    }

	private void Awake()
	{
        platformTransform = transform;
	}

	private void MoveVertically()
    {
        startPos = platformTransform.position.y;

        endPos = startPos;
        endPos += verticalMovementRange;


        float speed = Random.Range(movingSpeed.x, movingSpeed.y);

        platformTransform.DOMoveY(endPos, speed)
            .SetEase(easing).OnComplete(() =>
            {
                platformTransform.DOMoveY(startPos, speed)
                .SetEase(easing).OnComplete(() => MoveVertically());
            });
    }

    private void MoveHorizontally()
	{
        float levelWidth = FindObjectOfType<LevelGenerator>().LevelWidth;
        levelWidth -= horizontalBorderOffset;

        float speed = Random.Range(movingSpeed.x, movingSpeed.y);

        platformTransform.DOMoveX(levelWidth, speed)
            .SetEase(easing).OnComplete(() =>
        {
            platformTransform.DOMoveX(-levelWidth, speed)
            .SetEase(easing).OnComplete(() => MoveHorizontally());
        });
    }
}
