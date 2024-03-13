using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 3f;
    public float right;
    public float left;
    private Vector3 rotation;

    // Start is called before the first frame update
    void Start()
    {
        right = transform.position.x + right;
        left = transform.position.x - left;
        rotation = transform.eulerAngles;
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
        if(transform.position.x < left)
        {
            transform.eulerAngles = rotation - new Vector3(0, 180, 0);
        }
        if(transform.position.x > right)
        {
            transform.eulerAngles = rotation;
        }
        
    }
}
