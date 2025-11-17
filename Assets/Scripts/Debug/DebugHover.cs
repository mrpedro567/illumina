using UnityEngine;

public class DebugHover : MonoBehaviour
{
    void OnMouseEnter()
    {
        Debug.Log("Mouse entrou no objeto");
    }

    void OnMouseExit()
    {
        Debug.Log("Mouse saiu do objeto");
    }
}
