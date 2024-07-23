using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;

public class MCameraPlayer : MonoBehaviour
{
    public float dirX;
    public float dirY;

    public Transform orientation;
    public CinemachineFreeLook cinemaCam;

    public float smoothing;  // Smoothing factor

    private Vector2 smoothVelocity;
    private Vector2 currentMouseDelta;
    public Transform playerTrans;

    public RectTransform rightHalf;

    float xRotation;
    float yRotation;


    // Start is called before the first frame update
    // void Start()
    // {
    //     Cursor.lockState = CursorLockMode.Locked;
    //     Cursor.visible = false;   
    // }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);
            Vector2 touchPosition = touch.position;

            // Handle touch phases
            if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                // Check if the touch is within the rightHalf area
                if (IsTouchInArea(touchPosition, rightHalf))
                {
                    HandleCameraInput(touch.deltaPosition); // Handle camera movement
                }
            }
        }
        
        if (Input.GetMouseButton(0))
        {
            Vector2 mousePosition = Input.mousePosition;
            if (RectTransformUtility.RectangleContainsScreenPoint(rightHalf, mousePosition))
            {
                HandleCameraInput(Vector2.zero); // Handle camera input
            }
        }
    }

    private void HandleCameraInput(Vector2 deltaPosition)
    {
        if (cinemaCam != null)
        {
            float mouseX = deltaPosition.x * dirX * Time.deltaTime;
            float mouseY = deltaPosition.y * dirY * Time.deltaTime;

            Vector2 targetMouseDelta = new Vector2(mouseX, mouseY);
            currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref smoothVelocity, smoothing * Time.deltaTime);

            cinemaCam.m_XAxis.Value += currentMouseDelta.x;
            cinemaCam.m_YAxis.Value -= currentMouseDelta.y;

            float rotationY = currentMouseDelta.x;
            playerTrans.Rotate(0f, rotationY, 0f);
        }
    }

    private bool IsTouchInArea(Vector2 position, RectTransform area)
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(area, position, null, out localPoint);
        return area.rect.Contains(localPoint);
    }
}
