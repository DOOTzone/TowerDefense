using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cannon : Towers
{
    [SerializeField] GameObject _bullet;
    private const float thresholdSqr = 2 * 2;
    GameObject Bullet = null;
    public static cannon Instance;
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
        if (TargetLocked)
        {
            if (!GameObject.FindGameObjectWithTag("Enemy"))
            {
                
                ResetAll();
            }
        }
        if (!TargetLocked)
        {
            FindTarget();
        }
        else
        {
                var distanceSqr = (Target.transform.position - transform.position).sqrMagnitude;
                if (distanceSqr <= thresholdSqr)
                {
                    Shoot(Target, 0.4f);
                }
        }
        
        
    }
    IEnumerator ShootIEnum(GameObject currentTarget, float delayTime)
    {
        
        yield return new WaitForSeconds(delayTime);
        if (!GameObject.FindGameObjectWithTag("Bullet"))
            Bullet = Instantiate(_bullet, transform.position, Quaternion.identity);
        else
        {
            Bullet.transform.position = Vector2.MoveTowards(Bullet.transform.position, currentTarget.transform.position, BulletSpeed*Time.deltaTime);
        }
    }

    public void ResetAll()
    {
        var bul = GameObject.FindGameObjectWithTag("Bullet");
        Destroy(bul);
        Target = null;
        TargetLocked = false;
        StopAllCoroutines();
    }

    //You call this function 
    void Shoot(GameObject currentTarget, float delayTime)
    {
        StartCoroutine(ShootIEnum(currentTarget, delayTime));
    }
}
