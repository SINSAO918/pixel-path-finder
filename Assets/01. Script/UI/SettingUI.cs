using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class SettingUI : MonoBehaviour
{
    [SerializeField] private PathFindAgent pathFindAgent;

    [SerializeField] private List<PixelPathFinder> pixelPathFinders;

    [SerializeField] private int algoritmType;
    [SerializeField] private PathFindDirection pathFindDirection;

    public void ChangeAlgoritm(TMP_Dropdown dropdown)
    {
        algoritmType = dropdown.value;
        pathFindAgent.pathFinder = pixelPathFinders[dropdown.value];
    }

    public void ChangeFindDirection(TMP_Dropdown direction)
    {
        pathFindDirection = (PathFindDirection)direction.value;
        foreach (var item in pixelPathFinders)
        {
            item.pathFindDirection = (PathFindDirection)direction.value;
        }
    }


    public void PathFind()
    {
        pathFindAgent.Find();   
    }

    public void SetDontCrossCorner(Toggle toggle)
    {
        foreach (var item in pixelPathFinders)
        {
            item.Dont_Cross_Corners = toggle.isOn;
        }
    }
}