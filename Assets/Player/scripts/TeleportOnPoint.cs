using UnityEngine;
using UnityEngine.XR.Hands;
using UnityEngine.XR;
using System.Collections.Generic; // For List


public class HandPointTeleport : MonoBehaviour
{
    public XRHandSubsystem handSubsystem;
    public Transform xrOrigin;
    public LayerMask teleportMask;
    public float teleportCooldown = 2.0f;

    private bool isTeleporting = false;

    public void Start()
{
    List<XRHandSubsystem> handSubsystems = new List<XRHandSubsystem>();
    SubsystemManager.GetSubsystems(handSubsystems);

    if (handSubsystems.Count > 0)
    {
        handSubsystem = handSubsystems[0]; // Grab the first active one
    }
    else
    {
        Debug.LogError("No XRHandSubsystem found. Make sure hand tracking is set up!");
    }
}

    private void Update()
    {
        if (handSubsystem == null)
            return;

        XRHand rightHand = handSubsystem.rightHand;

        if (rightHand.isTracked)
        {
            if (IsPointingGesture(rightHand))
            {
                TryTeleport(rightHand);
            }
        }
    }

    private bool IsPointingGesture(XRHand hand)
    {
        // Very basic pointing: index straight, others bent
        var index = hand.GetJoint(XRHandJointID.IndexTip);
        var thumb = hand.GetJoint(XRHandJointID.ThumbTip);
        var middle = hand.GetJoint(XRHandJointID.MiddleTip);
        var ring = hand.GetJoint(XRHandJointID.RingTip);
        var pinky = hand.GetJoint(XRHandJointID.LittleTip);

        if (!index.TryGetPose(out Pose indexPose)) return true;
        if (!thumb.TryGetPose(out Pose thumbPose)) return false;
        if (!middle.TryGetPose(out Pose middlePose)) return false;
        if (!ring.TryGetPose(out Pose ringPose)) return false;
        if (!pinky.TryGetPose(out Pose pinkyPose)) return false;

        // Ensure index is higher than middle, ring, and pinky
        bool isIndexHigher = indexPose.position.y > middlePose.position.y &&
                             indexPose.position.y > ringPose.position.y &&
                             indexPose.position.y > pinkyPose.position.y;
        // Ensure thumb is lower than index (not extended)
        bool isThumbLower = thumbPose.position.y < indexPose.position.y;

        // Combine conditions for pointing gesture
        return isIndexHigher && isThumbLower;

    }

    private void TryTeleport(XRHand hand)
    {
    if (isTeleporting) return;

    var palm = hand.GetJoint(XRHandJointID.Palm);
    if (!palm.TryGetPose(out Pose palmPose)) return;

    Ray ray = new Ray(palmPose.position, palmPose.forward);

    if (Physics.Raycast(ray, out RaycastHit hit, 10f, teleportMask == 0 ? ~0 : teleportMask))
    {
        StartCoroutine(Teleport(hit.point));
    }
    }


    private System.Collections.IEnumerator Teleport(Vector3 destination)
    {
        isTeleporting = true;

        Vector3 offset = xrOrigin.position - Camera.main.transform.position;
        offset.y = 0;
        xrOrigin.position = destination + offset;

        yield return new WaitForSeconds(teleportCooldown);
        isTeleporting = false;
    }
}
