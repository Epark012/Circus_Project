using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrumZone : MonoBehaviour
{
    public enum ForceDirection
    {
        Left,
        Forward,
        Right,
        Up,
        Down
    }

    public ForceDirection forceDirection;
    Vector3 direction;
    Color collColour = Color.white;
    [SerializeField]
    private int forceIntensity = 1;

    // Start is called before the first frame update
    void Start()
    {
        switch(forceDirection)
        {
            case ForceDirection.Left:
                {
                    direction = Vector3.right;
                    collColour = Color.red;
                }
                break;
            case ForceDirection.Forward:
                {
                    direction = Vector3.forward;
                    collColour = Color.yellow;
                }
                break;
            case ForceDirection.Right:
                {
                    direction = Vector3.left;
                    collColour = Color.green;
                }
                break;
            case ForceDirection.Up:
                {
                    direction = Vector3.up;
                    collColour = Color.blue;
                }
                break;
            case ForceDirection.Down:
                {
                    direction = Vector3.down;
                    collColour = Color.cyan;
                }
                break;
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Drum")
        {
            Debug.Log("Direction :" + forceDirection);
            Debug.Log("Intensity :" + forceIntensity);
            other.GetComponent<Rigidbody>().AddForce(direction * forceIntensity);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = collColour;
        Gizmos.DrawWireCube(transform.position, transform.lossyScale);
    }
}

