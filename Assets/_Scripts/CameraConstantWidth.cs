using UnityEngine;

public class CameraConstantWidth : MonoBehaviour
{
    [SerializeField] private Vector2 defaultResolution = new Vector2(720, 1280);
    [SerializeField] [Range(0f, 1f)] private float WidthOrHeight = 0;
    
    private float horizontalFov;
    private Camera camComponent;
    private float targetAspect;
    private float initialVerticalFov;

    private void Start()
    {
        camComponent = GetComponent<Camera>();

        targetAspect = defaultResolution.x / defaultResolution.y;

        initialVerticalFov = camComponent.fieldOfView;
        horizontalFov = CalcVerticalFov(initialVerticalFov, 1 / targetAspect);
    }

    private void Update()
    {
        float constantWidthFov = CalcVerticalFov(horizontalFov, camComponent.aspect);
        camComponent.fieldOfView = Mathf.Lerp(constantWidthFov, initialVerticalFov, WidthOrHeight);
    }

    private float CalcVerticalFov(float hFovInDeg, float aspectRatio)
    {
        float hFovInRads = hFovInDeg * Mathf.Deg2Rad;

        float vFovInRads = 2 * Mathf.Atan(Mathf.Tan(hFovInRads / 2) / aspectRatio);

        return vFovInRads * Mathf.Rad2Deg;
    }
}