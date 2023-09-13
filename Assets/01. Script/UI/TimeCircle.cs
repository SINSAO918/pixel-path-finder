using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeCircle : MonoBehaviour
{
    [SerializeField] private Image image;
    private void Update()
    {
        image.fillAmount = Time.time % 1f;
    }
}
