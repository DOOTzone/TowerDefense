using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LivesScript : MonoBehaviour
{
    [SerializeField] TMP_Text lives;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lives.text = "Lives: " + GameManager.Instance.Health;
    }
}
