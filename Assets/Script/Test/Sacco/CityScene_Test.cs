using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CityScene_Test : MonoBehaviour
{

    //GET TRANSFORM OF THE OBJECT
    public Transform pos;

    // Start is called before the first frame update
    void Start()
    {
        pos = GetComponent<Transform>();
        Debug.Log("Position: " + pos.position);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Position: " + pos.position);
    }
}
