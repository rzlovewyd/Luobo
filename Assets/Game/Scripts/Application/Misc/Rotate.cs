using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour 
{
    public float Speed = 360;
	void Update () 
	{
        transform.Rotate(Vector3.forward, Time.deltaTime * Speed);
	}
}
