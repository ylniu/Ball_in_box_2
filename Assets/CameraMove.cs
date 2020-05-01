using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
 
public class CameraMove : MonoBehaviour {
 
    //public Transform target;
    public Vector3 target;
    private Vector3 offset;
    private bool IsControlMove;
    private Vector3 _position = Vector3.zero;
    private Quaternion _rotation = Quaternion.identity;
    private float _xAngles = 0.0f;
    private float _yAngles = 0.0f;
    private float m_xAngles = 0.0f;
    private float m_yAngles = 0.0f;    
    private float m_xSpeed = 100f;
    private float m_ySpeed = 100f;
    //Limit
    private float m_xMinValue = -15f;
    private float m_xMaxValue = 15;
    private float m_yMinValue = -15;
    private float m_yMaxValue = 15;
    private float m_limitChangeTime = 3.0f;
    void Awake ()
    {
        //Init
        Vector3 myCameraAngles = transform.eulerAngles;
        _xAngles = myCameraAngles.y;
        _yAngles = myCameraAngles.x;
        _position = transform.position;
        _rotation = transform.rotation;
        m_xAngles = myCameraAngles.y;
        m_yAngles = myCameraAngles.x;
        offset = transform.position - target;
    }
    void LateUpdate () {
        if (IsControlMove)
        {
            if (Application.platform == RuntimePlatform.WindowsEditor)
            {
                if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
                {
                    CameraChange();
                }
                if (Input.GetMouseButtonUp(0))
                {
                    InitDataOfCamera();
                }
            }
            else if (Application.platform == RuntimePlatform.Android ||
                     Application.platform == RuntimePlatform.IPhonePlayer)
            {
                if (Input.touchCount == 1)
                {
                    if (Input.touches[0].phase == TouchPhase.Moved)
                    {
                        //Move
                        CameraChange();
                    }
                }
                if (Input.touches[0].phase == TouchPhase.Ended && Input.touches[0].phase != TouchPhase.Canceled)
                {
                    InitDataOfCamera();
                }
            }
        }
    }
    public void CameraChange()
    {
        m_xAngles -= Input.GetAxis("Mouse X") * m_xSpeed * 0.02f;
        m_xAngles = Mathf.Clamp(m_xAngles, m_xMinValue, m_xMaxValue);
        m_yAngles += Input.GetAxis("Mouse Y") * m_ySpeed * 0.02f;
        m_yAngles = Mathf.Clamp(m_yAngles, m_yMinValue, m_yMaxValue);
        var rotation = Quaternion.Euler(m_yAngles, m_xAngles, 0);
        Vector3 position = rotation * offset + target;
        transform.rotation = rotation;
        transform.position = position;
    }
    /// <summary>
    /// Init Data
    /// </summary>
     public void InitDataOfCamera()
    {
        m_xAngles = _xAngles;
        m_yAngles = _yAngles;
        transform.position = Vector3.Lerp(transform.position, _position, m_limitChangeTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, _rotation, m_limitChangeTime);
    }
    public void OnCameraControl(bool value)
    {
        IsControlMove = value;
    }
}