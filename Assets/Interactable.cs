using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script is based on a video tutorial at: https://www.youtube.com/watch?v=HnzmnSqE-Bc

[RequireComponent(typeof(Rigidbody))]
public class Interactable : MonoBehaviour
{
    [HideInInspector]
    public Hands m_ActiveHand = null;
}
