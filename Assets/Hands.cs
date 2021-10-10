using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR;

//This script is based on a video tutorial at: https://www.youtube.com/watch?v=HnzmnSqE-Bc

public class Hands : BaseInput
{
    public SteamVR_Action_Boolean m_GrabAction = null;

    private SteamVR_Behaviour_Pose m_Pose = null;
    private FixedJoint m_Joint = null;

    private Interactable m_CurrentInteractable = null;
    public List<Interactable> m_ContactInteractables = new List<Interactable>();

    //Initialises Joint and Pose for controller
    private void Awake()
    {
        m_Pose = GetComponent<SteamVR_Behaviour_Pose>();
        m_Joint = GetComponent<FixedJoint>();
    }

    private void Update()
    {
        //When trigger is squeezed down, print to console and run Pickup method
        if(m_GrabAction.GetStateDown(m_Pose.inputSource))
        {
            print(m_Pose.inputSource + " Trigger Down");
            Pickup();
        }

        //When trigger is released, print to console and run Drop method
        if(m_GrabAction.GetStateUp(m_Pose.inputSource))
        {
            print(m_Pose.inputSource + " Trigger Up");
            Drop();
        }
    }

    //Wheen trigger is pressed while controller is inside an object, check if object has the tag "Interactable"
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Interactable"))
            return;

        m_ContactInteractables.Add(other.gameObject.GetComponent<Interactable>());
    }

    //Wheen trigger is released while controller is inside an object, check if object has the tag "Interactable"
    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Interactable"))
            return;

        m_ContactInteractables.Remove(other.gameObject.GetComponent<Interactable>());
    }

    //Method to pickup (attach an object to the controller)
    public void Pickup()
    {
        // Get Nearest
        m_CurrentInteractable = GetNearestInteractable();
        //Flip card upright
        m_CurrentInteractable.transform.Rotate(0, 0, 180);
        print(m_CurrentInteractable.GetInstanceID() + " CardID");

        // Null check
        if (!m_CurrentInteractable)
            return;

        // Already held, check
        if (m_CurrentInteractable.m_ActiveHand)
            m_CurrentInteractable.m_ActiveHand.Drop();

        // Position
        m_CurrentInteractable.transform.position = transform.position;

        // Attach
        Rigidbody targetBody = m_CurrentInteractable.GetComponent<Rigidbody>();
        m_Joint.connectedBody = targetBody;

        // Set active hand
        m_CurrentInteractable.m_ActiveHand = this;
        m_CurrentInteractable = null;
    }

    //Method to drop (detach an object from the controller)
    public void Drop()
    {
        // Null Check
        if (!m_CurrentInteractable)
            return;

        // Appply velocity
        Rigidbody targetBody = m_CurrentInteractable.GetComponent<Rigidbody>();
        targetBody.velocity = m_Pose.GetVelocity();
        targetBody.angularVelocity = m_Pose.GetAngularVelocity();

        // Detach
        
        m_Joint.connectedBody = null;

        // Clear
        m_CurrentInteractable.m_ActiveHand = null;
        m_CurrentInteractable = null;
    }

    //Returns nearest object with "Interactable" method
    private Interactable GetNearestInteractable()
    {
        Interactable nearest = null;
        float minDistance = float.MaxValue;
        float distance = 0.0f;

        foreach(Interactable interactable in m_ContactInteractables)
        {
            distance = (interactable.transform.position - transform.position).sqrMagnitude;

            if(distance < minDistance)
            {
                minDistance = distance;
                nearest = interactable;
            }
        }

        return nearest;
    }
}