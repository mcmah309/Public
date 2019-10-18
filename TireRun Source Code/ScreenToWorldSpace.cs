using UnityEngine;
using System.Collections;

public class ScreenToWorldSpace : MonoBehaviour
{
    Camera cam;
    Canvas canvas;
    RectTransform rectTransform;
    void Start()
    {
        cam = GetComponent<Camera>();
        canvas = FindObjectOfType<Canvas>();
        rectTransform = canvas.GetComponent<RectTransform>();
        float distanceFromCanvas = (rectTransform.rect.width / 2) / (Mathf.Tan((cam.fieldOfView / 2)* (Mathf.PI/180)));
        float heightViewableOnScreen = distanceFromCanvas * (Mathf.Tan((cam.fieldOfView / 2) * (Mathf.PI / 180)));
        if( heightViewableOnScreen > (Screen.height / 2))
        {
            Debug.Log(Screen.height / 2);
            Debug.Log(heightViewableOnScreen);
            distanceFromCanvas = (Screen.height / 2) / (Mathf.Tan((cam.fieldOfView / 2) * (Mathf.PI / 180)));
        }
        cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y, distanceFromCanvas * -1);


    }
}