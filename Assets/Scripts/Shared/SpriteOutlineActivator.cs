using UnityEngine;

public class SpriteOutlineActivator : MonoBehaviour
{
    private Material material;

    void Start()
    {
        material = GetComponent<SpriteRenderer>().material;
        material.SetFloat("_OutlineSize", 0); // começa sem borda
    }

    void OnMouseEnter()
    {
        material.SetFloat("_OutlineSize", 2); // aparece a borda
    }

    void OnMouseExit()
    {
        material.SetFloat("_OutlineSize", 0); // some a borda
    }
}
