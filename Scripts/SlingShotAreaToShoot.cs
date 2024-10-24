using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SlingShotAreaToShoot : MonoBehaviour
{

   [SerializeField] private LayerMask slingShotAreaMask;

   public bool isWithInSliceSlingShotArea()
    {
        Vector2 worldposition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        if (Physics2D.OverlapPoint(worldposition,slingShotAreaMask))
        {
            return true;
        }
        else
        {
            return false;
        }

    }
}
