using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OZ : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Changes the scoreboard based on the given keyboard input
        if (Input.GetKeyDown(KeyCode.A))
        {
            GetComponent<TextMesh>().text = "Number of Matches\nRemaining:\n1";
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            GetComponent<TextMesh>().text = "Number of Matches\nRemaining:\n2";
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            GetComponent<TextMesh>().text = "Number of Matches\nRemaining:\n3";
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            GetComponent<TextMesh>().text = "Number of Matches\nRemaining:\n4";
        }
    }

    private void changeToOne()
    {
        
    }
}
