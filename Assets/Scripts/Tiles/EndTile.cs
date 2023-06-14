using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTile : Tile
{
    public static EndTile Instance;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        Instance = this;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
   public void Hurt(int power)
    {
        GameManager.Instance.Health-=power;
    }
}
