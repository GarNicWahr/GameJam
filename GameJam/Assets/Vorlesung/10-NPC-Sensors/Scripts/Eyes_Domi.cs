using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[ExecuteInEditMode]
public class Eyes_Domi : MonoBehaviour
{
    public Transform HeadReferenceTransform;
    public float Range;
    public float HearableRange;
    public float Fov;
    public LayerMask DetectionLayer;
    public bool IsDetecting { get; private set; }

    private Transform _player;
    private Vector3 _directionToPlayer;


    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        _directionToPlayer = _player.position - HeadReferenceTransform.position;

        if(IsInRange() && IsInFieldOfView() && IsNotOccluded() || IsInRange() && IsHearable())
        {
            IsDetecting = true;
        }
        else
        {
            IsDetecting = false;
        }
    }

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        SenseGizmos.DrawRangeCircle(HeadReferenceTransform.position, transform.up, Range);

        if(IsInRange())
        {
            SenseGizmos.DrawFOV(HeadReferenceTransform.position, HeadReferenceTransform.forward, transform.up, Range, Fov);
            SenseGizmos.DrawRangeDisc(HeadReferenceTransform.position, transform.up, HearableRange);

            if (IsInFieldOfView())
            {
                SenseGizmos.DrawRay(HeadReferenceTransform.position, _player.position, IsNotOccluded());
            }

        }
    }
#endif

    public bool IsInRange()
    {
        // _directionToPlayer.magnitude < Range (Aufwendiger zu berechnen)
        return _directionToPlayer.sqrMagnitude <= Range * Range;
    }

    public bool IsInFieldOfView ()
    {
        Vector3 direction = _directionToPlayer;
        direction.y = 0;

        Vector3 forward = HeadReferenceTransform.forward;
        forward.y = 0;

        float angleBetween = Vector3.Angle(forward, direction);
        return angleBetween < Fov * 0.5f;
    }

    public bool IsNotOccluded()
    {
        RaycastHit hit;
        Ray ray = new Ray(HeadReferenceTransform.position, _directionToPlayer);

        if(Physics.Raycast(ray, out hit, Range, DetectionLayer))
        {
            return hit.collider.gameObject.CompareTag("Player");
        }
        else
        {
            return false;
        }
    }

    public bool IsHearable()
    {
        return _directionToPlayer.sqrMagnitude <= HearableRange * HearableRange;
    }
}
