using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHairCostumization : MonoBehaviour
{
    [SerializeField] private float thickness;
    [SerializeField] private float lenght;
    [SerializeField] private float size;
    [SerializeField] private RectTransform[] crossParts;
    [SerializeField] private RectTransform selfRect;
    private float[,] position;

    private void Awake()
    {
        selfRect.rect.Set(0, 0, size, size);
        position = new float[4, 2]
        {
            {0f, -lenght/2},
            {lenght/2, 0f},
            {-lenght/2, 0f},
            {0f, lenght/2},
        };
        for(int i=0; i<4; i++)
        {
            crossParts[i].rect.Set(position[i, 0], position[i, 1], thickness, lenght);
        }
    }
}
