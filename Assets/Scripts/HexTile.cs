using UnityEngine;
using System.Collections;

public class HexTile : MonoBehaviour {


    private Vector2 position;
    private bool ocupied = false;

    void OnMouseDown()
    {
        if(!ocupied)
        {
            ChangeColor(Color.red);
        }
    }
    public void ChangeColor(Color materialColor)
    {
        ocupied = true;
        GetComponent<MeshRenderer>().material.color = materialColor;
    }
    public Vector2 Position
    {
        set { position = value; }
        get { return position; }
    }
}
