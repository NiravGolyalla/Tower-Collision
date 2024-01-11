using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using Cinemachine;

public class CameraSystem : MonoBehaviour
{
    [SerializeField]CinemachineVirtualCamera cam;
    [SerializeField]float moveSpeed = 10f;
    [SerializeField]float dragSpeed = 10f;
    [SerializeField]float edgeScrollBorder = 20f;
    [SerializeField]float TargetFOV = 25f;
    [SerializeField]float fovMin = 10f;
    [SerializeField]float fovMax = 30f;
    [SerializeField]float zoomSpeed = 30f;
    [SerializeField]float ratio = 30f;
    [SerializeField]bool pan = true;

    private float[] timeScaleValues = { 1f, 2f, 4f, 8f, 16f };
    private int currentIndex = 0;

    bool trackMouse;
    Vector2 lastPosition;
    

    private void HandleZoom(){
        if(Input.mouseScrollDelta.y > 0){
            TargetFOV -= 5;
        }
        if(Input.mouseScrollDelta.y < 0){
            TargetFOV += 5;
        }

        TargetFOV = Mathf.Clamp(TargetFOV,fovMin,fovMax);
        cam.m_Lens.FieldOfView = Mathf.Lerp(cam.m_Lens.FieldOfView,TargetFOV,Time.deltaTime * zoomSpeed);
    }
    private void HandleMouseClickPan(){
        Vector3 moveDir = new Vector3(0,0,0);
        
        if(Input.GetMouseButtonDown(1)){
            trackMouse = true;
            lastPosition = Input.mousePosition;        
        } 
        if(Input.GetMouseButtonUp(1)){
            trackMouse = false;
        }

        if(trackMouse){
            Vector2 mouseMoveDelta = (Vector2)Input.mousePosition - lastPosition;
            moveDir.x = -mouseMoveDelta.x * dragSpeed * TargetFOV/ratio;
            moveDir.z = -mouseMoveDelta.y * dragSpeed * TargetFOV/ratio;

            lastPosition = Input.mousePosition;
        }   
        moveDir = transform.forward * moveDir.z + transform.right * moveDir.x;
        transform.position += moveDir * moveSpeed * Time.unscaledDeltaTime;
    }
    private void HandleKeyboardMove(){
        Vector3 moveDir = new Vector3(0,0,0);

        if(Input.GetKey(KeyCode.W)) moveDir.z = 1f;
        if(Input.GetKey(KeyCode.A)) moveDir.x = -1f;
        if(Input.GetKey(KeyCode.S)) moveDir.z = -1f;
        if(Input.GetKey(KeyCode.D)) moveDir.x = 1f;
        
        moveDir = transform.forward * moveDir.z + transform.right * moveDir.x;
        transform.position += moveDir * moveSpeed * Time.unscaledDeltaTime;
    }

    private void Update()
    {
        HandleZoom();
        if(pan){
            HandleMouseClickPan();
        } else{
            HandleKeyboardMove();    
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Toggle through the array values
            currentIndex = (currentIndex + 1) % timeScaleValues.Length;
            Time.timeScale = timeScaleValues[currentIndex];
        }
        
        
        
    }
}
