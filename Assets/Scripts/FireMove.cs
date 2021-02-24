using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireMove : MonoBehaviour
{

    public float speed = 5;

    public float gravity = -9.81f;
    float delta = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float newXPosition = transform.position.x + delta;
        transform.position = new Vector3(newXPosition, 2, -7);
        if (transform.position.x < -3.5)
        {
            delta = 0.1f;

        }
        else if (transform.position.x > 3.5)
        {
            delta = -0.1f;

        }
    }
}