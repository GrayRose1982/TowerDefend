using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameHelper
{
    public static void SetDirection(this Transform trans, Vector2 direction, float angleAdd = 0f)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + angleAdd;
        trans.eulerAngles = Vector3.forward * angle;
    }
}
