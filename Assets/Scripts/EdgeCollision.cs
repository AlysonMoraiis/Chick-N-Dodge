using System.Diagnostics;
using UnityEngine;

public class EdgeCollision : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private ScreenSide _screenSide = ScreenSide.Right;

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
            // Se a referência da câmera não for definida, use a câmera principal
            _camera = Camera.main;
        }

        AdjustSize();
        AdjustPosition();
    }

    private void AdjustSize()
    {
        float objectHeight = _camera.orthographicSize * 2f;
        float objectWidth = objectHeight * _camera.aspect;

        // Define a escala do objeto com base na largura e altura desejadas
        transform.localScale = new Vector3(objectWidth, objectHeight, 1f);
    }

    private void AdjustPosition()
    {
        float cameraRightEdge;
        float objectHalfWidth;
        float desiredXPosition;
        Vector3 newPosition;

        switch (_screenSide)
        {
            case ScreenSide.Right:
                cameraRightEdge = _camera.aspect * _camera.orthographicSize;
                objectHalfWidth = transform.localScale.x / 2f;
                desiredXPosition = cameraRightEdge + objectHalfWidth;
                newPosition = new Vector3(desiredXPosition, transform.position.y, transform.position.z);
                transform.position = newPosition;
                break;
            case ScreenSide.Left:
                cameraRightEdge = -_camera.aspect * _camera.orthographicSize;
                objectHalfWidth = transform.localScale.x / 2f;
                desiredXPosition = cameraRightEdge - objectHalfWidth;
                newPosition = new Vector3(desiredXPosition, transform.position.y, transform.position.z);
                transform.position = newPosition;
                break;
            case ScreenSide.Bottom:
                cameraRightEdge = -_camera.orthographicSize;
                objectHalfWidth = transform.localScale.y / 2f;
                desiredXPosition = cameraRightEdge - objectHalfWidth;
                newPosition = new Vector3(transform.position.x, desiredXPosition, transform.position.z);
                transform.position = newPosition;
                break;
            default:
                cameraRightEdge = _camera.orthographicSize;
                objectHalfWidth = transform.localScale.y / 2f;
                desiredXPosition = cameraRightEdge + objectHalfWidth;
                newPosition = new Vector3(transform.position.x, desiredXPosition, transform.position.z);
                transform.position = newPosition;
                break;
        }

    }
}

