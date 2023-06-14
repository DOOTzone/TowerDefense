using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTile : Tile
{
    float posX = 0;
    float posY = 0;
    [SerializeField] private Color _offsetColor;
    [SerializeField]private GameObject tower;
    //[SerializeField] private Tower; 
    public override void Init(int x, int y)
    {
        posX = (float)x;
        posY =(float)y;
        var isOffset = (x + y) % 2 == 1;
        if (isOffset)
        {
            
            _renderer.color = _offsetColor;
        }
    }
    private void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.C) && !this.IsFull)
        {
            Instantiate(tower, new Vector3(posX/2, posY/2),Quaternion.identity);
            this.IsFull = true;
        }
        else
        if(Input.GetKeyDown(KeyCode.C))
            Debug.Log("Tile is full");
    }
}
