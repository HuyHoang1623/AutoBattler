using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleMap : MonoBehaviour
{
    public GameObject titlePrefab;
    public GameObject rectanglePrefab;
    public GameObject borderPrefab;
    public float rectangleMargin = 1f;
    public float borderOffset = 8f;
    int mapWidth = 10;
    int mapHeight = 10;
    float titleXOffset = 5.5f;
    float titleZOffset = 5f;

    void Start()
    {
        CreateRectangle();
        CreateTitleMap();
        CreateBorders();
    }

    void CreateTitleMap()
    {
        for (int x = 0; x <= mapWidth; x++)
        {
            for (int z = 0; z <= mapHeight; z++)
            {
                GameObject TempGO = Instantiate(titlePrefab);
                if (z % 2 == 0)
                {
                    TempGO.transform.position = new Vector3(x * titleXOffset, 0, z * titleZOffset);
                }
                else
                {
                    TempGO.transform.position = new Vector3(x * titleXOffset + titleXOffset / 2, 0, z * titleZOffset);
                }
                SetTitleInfo(TempGO, x, z);
            }
        }
    }

    void CreateRectangle()
    {
        GameObject rectangle = Instantiate(rectanglePrefab);
        float rectangleWidth = (mapWidth + 1) * titleXOffset + rectangleMargin * 2 + 2;
        float rectangleHeight = (mapHeight + 1) * titleZOffset + rectangleMargin * 2 + 2;
        rectangle.transform.localScale = new Vector3(rectangleWidth, 0.1f, rectangleHeight);

        rectangle.transform.position = new Vector3(
            (mapWidth * titleXOffset) / 2 + 1,
            -0.05f,
            (mapHeight * titleZOffset) / 2
        );
    }

    void CreateBorders()
    {
        for (int x = 0; x <= mapWidth; x++)
        {
            GameObject bottomBorder = Instantiate(borderPrefab);
            GameObject topBorder = Instantiate(borderPrefab);

            bottomBorder.transform.position = new Vector3(x * titleXOffset, 0, -titleZOffset - borderOffset);

            if (mapHeight % 2 == 0)
            {
                topBorder.transform.position = new Vector3(x * titleXOffset, 0, (mapHeight + 1) * titleZOffset + borderOffset);
            }
            else
            {
                topBorder.transform.position = new Vector3(x * titleXOffset + titleXOffset / 2, 0, (mapHeight + 1) * titleZOffset + borderOffset);
            }

            SetTitleInfo(bottomBorder, x, -1);
            SetTitleInfo(topBorder, x, mapHeight + 1);
        }
    }

    void SetTitleInfo(GameObject GO, int x, int z)
    {
        GO.transform.parent = transform;
        GO.name = x.ToString() + "," + z.ToString();
    }
}
