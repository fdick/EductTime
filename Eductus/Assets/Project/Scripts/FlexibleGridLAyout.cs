using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlexibleGridLAyout : LayoutGroup
{
    public enum FitType
    {
        Uniform,
        Width,
        Heigh,
        FixedRows,
        FixedColumns,
    }

    public float coeff = 2;
    public FitType fitType;
    public int rows;
    public int colums;
    public Vector2 cellSize;
    public Vector2 space;
    public bool fitX, fitY = true;

    public override void CalculateLayoutInputHorizontal()
    {
        base.CalculateLayoutInputHorizontal();

        if (fitType == FitType.Width || fitType == FitType.Heigh || fitType == FitType.Uniform)
        {
            fitX = fitY = true;
            float sqrRT = Mathf.Sqrt(transform.childCount);
            colums = rows = Mathf.CeilToInt(sqrRT);
        } 

        if (fitType == FitType.Width || fitType == FitType.FixedColumns)
        {
            fitX = fitY = false;
            rows = Mathf.CeilToInt(transform.childCount / (float)colums);
        }
        if (fitType == FitType.Heigh || fitType == FitType.FixedRows)
        {
            fitX = fitY = false;
            colums = Mathf.CeilToInt(transform.childCount / (float)rows);
        }

        float parentWidth = rectTransform.rect.width;
        float parentHeigh = rectTransform.rect.height;

        float cellWidth = (parentWidth / (float)colums) /*- ((space.x / (float)colums)* coeff)*/ - ((padding.left/ (float)colums) * coeff) - (padding.right/ (float)colums) * coeff;
        float cellHeigh = (parentHeigh / (float)rows) /*- ((space.y /(float)rows)* coeff) */- ((padding.top/ (float)rows)*coeff) - (padding.bottom/ (float)rows) * coeff;

        cellSize.x = cellWidth;
        cellSize.y = cellHeigh;

        int columCount = 0;
        int rowCount = 0;
        for (int i = 0; i < rectChildren.Count; i++)
        {                          
            columCount = i / colums;   
            rowCount = i % colums;    


            var item = rectChildren[i];
            item.localScale = Vector3.one;

            var xPos = cellSize.x * columCount + (space.x * columCount) + padding.left;
            var yPos = cellSize.y * rowCount   + (space.y * rowCount) + padding.top;

            SetChildAlongAxis(item, 0, xPos, cellSize.x);
            SetChildAlongAxis(item, 1, yPos, cellSize.y);
        }
    }

    public override void CalculateLayoutInputVertical()
    {
       
        
    }

    public override void SetLayoutHorizontal()
    {
      
    }

    public override void SetLayoutVertical()
    {
       
    }

}
