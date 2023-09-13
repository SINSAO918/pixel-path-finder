using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class UiWindow : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private RectTransform rectTransform;

    private InputData inputData = new();

    private void Start()
    {
        rectTransform.pivot = Vector2.zero;
    }

    private void Update()
    {
        if (inputData.isDown)
        {
            Vector3 delta = Input.mousePosition - inputData.mousePosition;
            rectTransform.position = Vector3.Lerp(rectTransform.position, inputData.rectPosition + delta, Time.deltaTime * 30);
            
            ClampPosition();
        }
    }

    private void ClampPosition()
    {
        Vector2 position = rectTransform.position;
        position.x = Mathf.Clamp(position.x, 0, Screen.width - rectTransform.rect.width);
        position.y = Mathf.Clamp(position.y, 0, Screen.height - rectTransform.rect.height);
        rectTransform.position = position;

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        inputData.isDown = true;
        inputData.mousePosition = Input.mousePosition;
        inputData.rectPosition = rectTransform.position;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        inputData.isDown = false;
    }

    public struct InputData
    {
        public bool isDown;
        public Vector3 mousePosition;
        public Vector3 rectPosition;
    }
}
