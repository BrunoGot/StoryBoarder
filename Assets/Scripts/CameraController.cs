using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{
    private GameObject m_camera;

    //buffers
    private Vector2 m_mousePos2;

    //rotate varibles
    private Vector3 m_pivot; //rotate pivot point
    private Ray m_ray;
    private Vector3 m_dir;

    //settings
    private float scrollFactor = 10.0f;
    //GameObject go;

    public void SetCamera(GameObject _camera)
    {
        m_camera = _camera;
       //go = GameObject.CreatePrimitive(PrimitiveType.Cube);
    }

    private void Zoom()
    {
        m_camera.transform.position += Input.mouseScrollDelta.y * m_camera.transform.forward * scrollFactor;
    }

    private void Rotate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            m_mousePos2 = Input.mousePosition; //init the mouse buffer pos

            //init rotate pivot
            m_ray = m_camera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);// new Vector3(Input.mousePosition.x, 0, Input.mousePosition.y));
            float angle = 180 - Vector3.Angle(Vector3.up, m_ray.direction.normalized);
            Debug.Log("angle = " + angle);
            float dist = m_camera.transform.position.y / Mathf.Cos(angle * Mathf.Deg2Rad);

            m_pivot = m_ray.GetPoint(dist);
            m_pivot.y = 0.0f;
          //  go.transform.position = m_pivot;
            m_dir = m_camera.transform.position- m_pivot;

        }
        if (Input.GetMouseButton(0))
        {
            //compute delta
            Vector2 deltaMouse = new Vector2(Input.mousePosition.x - m_mousePos2.x, Input.mousePosition.y-m_mousePos2.y);
          //  Debug.Log("draw ray ");

            Vector3 fwd = m_camera.transform.forward;
            float dot = Vector3.Dot(fwd, Vector3.right);
           // Debug.Log("dot = "+dot+" | deltaMouse.y*(1.0f-dot) = " + deltaMouse.y * (1.0f - dot));
            //m_camera.transform.RotateAround(m_pivot, new Vector3(deltaMouse.y*(1.0f-dot) , deltaMouse.x,0f), deltaMouse.magnitude*0.1f);

            m_dir = Quaternion.Euler(new Vector3(0, deltaMouse.x, deltaMouse.y))* m_dir;
            m_camera.transform.position = m_pivot + m_dir;
          //  Debug.DrawRay(m_ray.origin, m_ray.direction*400.0f, Color.red,10.0f);
            m_camera.transform.LookAt(m_pivot);
            m_mousePos2 = Input.mousePosition;
            //Vector3 targetPos = ray.GetPoint()

            //Vector3 targetPos =; //ScreenToWorldPoint(new Vector3(Input.mousePosition.x, 0, Input.mousePosition.y));
            // Debug.Log("targetPos = " + targetPos);
        }
    }
    
    private void Update()
    {
//        Debug.Log("EventSystem.current.name = " + EventSystem.current.currentSelectedGameObject);
        if (m_camera != null)
        {
            if (EventSystem.current.IsPointerOverGameObject()==false || EventSystem.current.currentSelectedGameObject!= null && EventSystem.current.currentSelectedGameObject.name == "CameraViewGUI") //if the user doesn't click on button
            {
                Zoom();
                Rotate();
            }
        }
    }
}
