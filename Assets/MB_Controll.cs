using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MB_Controll : MonoBehaviour
{
    private float horizontalInput;

    public void OnBreakEnter() { horizontalInput = -1f; }
    public void OnBreakExit() { horizontalInput = 0f; }

    public void OnGasEnter() { horizontalInput = 1f; }
    public void OnGasExit() { horizontalInput = 0f; }
}