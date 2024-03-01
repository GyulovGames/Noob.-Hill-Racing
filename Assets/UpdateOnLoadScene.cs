using UnityEngine;

public class UpdateOnLoadScene : MonoBehaviour
{
    private void Start()
    {
        GameObject gameObject = GameObject.FindGameObjectWithTag("MainCanvas");
        MainCanvas mainCanvas = gameObject.GetComponent<MainCanvas>();
        mainCanvas.UpdateUI();
    }
}