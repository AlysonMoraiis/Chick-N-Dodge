using UnityEngine;

public class EdgeCollision : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private ScreenSide _screenSide;

    private enum ScreenSide
    {
        Left,
        Right,
        Top,
        Bottom
    }

    private void Start()
    {
        if (_camera == null)
        {
            _camera = Camera.main;
        }

        AdjustSize();
        AdjustPosition();
    }

    private void AdjustSize()
    {
        float objectHeight = _camera.orthographicSize * 2f;
        float objectWidth = objectHeight * _camera.aspect;

        transform.localScale = new Vector3(objectWidth, objectHeight, 1f);
    }

    private void AdjustPosition()
    {
        Vector3 newPosition;

        switch (_screenSide)
        {
            case ScreenSide.Right:
                newPosition = CalculatePosition(_camera.aspect * _camera.orthographicSize, transform.localScale.x / 2f);
                break;
            case ScreenSide.Left:
                newPosition = CalculatePosition(-_camera.aspect * _camera.orthographicSize, -transform.localScale.x / 2f);
                break;
            case ScreenSide.Bottom:
                newPosition = CalculatePosition(-_camera.orthographicSize, -transform.localScale.y / 2f, true);
                break;
            default:
                newPosition = CalculatePosition(_camera.orthographicSize, transform.localScale.y / 2f, true);
                break;
        }

        transform.position = newPosition;
    }

    private Vector3 CalculatePosition(float cameraEdge, float objectHalfSize, bool isVertical = false)
    {
        float desiredPosition = cameraEdge + objectHalfSize;
        Vector3 newPosition;

        if (isVertical)
        {
            newPosition = new Vector3(transform.position.x, desiredPosition, transform.position.z);
        }
        else
        {
            newPosition = new Vector3(desiredPosition, transform.position.y, transform.position.z);
        }

        return newPosition;
    }
}
