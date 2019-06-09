using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class GetRect : Singleton<GetRect>
{
    /// <summary>
    /// 获得GameObj的RecPos
    /// </summary>
    /// <param name="go"></param>
    /// <returns></returns>
    public Vector3 GetRectPos(GameObject go)
    {
        return go.GetComponent<RectTransform>().anchoredPosition3D;
    }

    /// <summary>
    /// 设置GameObj的RecPos
    /// </summary>
    /// <param name="go"></param>
    /// <param name="v"></param>
    public void SetRectPos(GameObject go, Vector3 v)
    {
        go.GetComponent<RectTransform>().anchoredPosition3D = v;
    }

    /// <summary>
    /// 获取鼠标在画布坐标的位置
    /// </summary>
    /// <param name="mousPos"></param>
    public void GetMouseRecPos(Canvas canvas, RectTransform canvasRect, out Vector2 mousPos)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, Input.mousePosition, canvas.worldCamera, out mousPos);
    }
}

