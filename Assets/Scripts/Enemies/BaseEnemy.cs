using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Search;

public class BaseEnemy : MonoBehaviour
{
    public List<Transform> points;
    public float MoveSpeed;
    public float RotationSpeed;
    public Transform CurrentTarget;
    private Transform myTransform; // cash objects transform
    private int pointIndex=0;
    public int Power;
    void Start()
    {
        
    }

    private void Awake()
    {
        points = new List<Transform>();
        AddPoints();
        myTransform = transform;
    }
    // Update is called once per frame
    void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), enemy.GetComponent<Collider2D>());
        if (pointIndex != points.Count)
        {
            myTransform.position = Vector2.MoveTowards(myTransform.position, points[pointIndex].transform.position, MoveSpeed * Time.deltaTime); // move forwards towards the next node.\

            if (myTransform.position == points[pointIndex].transform.position)
            {
                pointIndex++;
            }
        }
        if (pointIndex > 0)
        {
            if (myTransform.position == points[pointIndex - 1].transform.position && pointIndex == points.Count)
            {
                EndTile.Instance.Hurt(Power);
                Destroy(this.gameObject);
            }
        }
    }
    private void AddPoints()
    {
        GameObject[] nodes = GameObject.FindGameObjectsWithTag("Point");

        foreach (GameObject node in nodes)
        {
            AddPoint(node.transform);
        }
    }
    public void AddPoint(Transform node)
    {
        points.Add(node);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Tile")
        {
            Debug.Log("Entered Collision");
            
        }
    }


}
