using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Towers : MonoBehaviour
{
    public bool TargetLocked = false;
    public float BulletSpeed = 3.0f;
    public GameObject Target = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FindTarget()
    {
        if (GameObject.FindGameObjectWithTag("Enemy"))
        {
            Target = GameObject.FindGameObjectWithTag("Enemy");
            if(Target!=null)
                TargetLocked = true;
        }
    }
    public virtual void Shoot(Vector3 TargetLocation, float delay)
    {

    }
}
