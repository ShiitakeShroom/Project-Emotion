using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerPosition
{
    private static Vector3 savedPosition;

    public static void SavePosition(Vector3 position)
    {
        savedPosition = position;
    }

    public static Vector3 GetPosition() 
    { 
        return savedPosition;
    }
}
