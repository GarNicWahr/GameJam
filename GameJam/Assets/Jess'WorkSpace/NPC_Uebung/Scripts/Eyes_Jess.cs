using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]

public class Eyes_Jess : MonoBehaviour
{
    public float Range;
    public float Fov;

    public bool IsDetecting { get; private set; }

    public LayerMask DetectionLayer;

    public Transform HeadReferenceTransform;

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

        if(IsInRange() && IsInFieldOfView() && IsNotOccluded())
        {
            IsDetecting = true;
        }
        else
        {
            IsDetecting = false;
        }
    }

    private void OnDrawGizmos()
    {
        SenseGizmos.DrawRangeCircle(HeadReferenceTransform.position, transform.up, Range);

        if (IsInRange())
        {
            SenseGizmos.DrawFOV(HeadReferenceTransform.position, HeadReferenceTransform.forward, transform.up, Range, Fov);

            if(IsInFieldOfView())
            {
                SenseGizmos.DrawRay(HeadReferenceTransform.position, _player.position, IsNotOccluded());
            }

        }

    }



    public bool IsInRange()
    {

        //_directionToPlayer.magnitude < Range
        return _directionToPlayer.sqrMagnitude <= Range * Range;

    }

    public bool IsInFieldOfView()
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

}
