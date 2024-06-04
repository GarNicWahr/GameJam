using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PathMeshController : MonoBehaviour
{
    public Transform Target;

    public LayerMask layerMask;

    private NavMeshAgent _agent;

    private Camera _rayCamera;
    private Animator _animator;

    private int _speedParameterHash;


    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _rayCamera = Camera.main;
        _animator = GetComponent<Animator>();

        _speedParameterHash = Animator.StringToHash("speed");
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _rayCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, layerMask))
            {
                if (raycastHit.collider.gameObject.layer.Equals(LayerMask.NameToLayer("Ground")))
                {
                    Target.position = raycastHit.point;
                    _agent.SetDestination(Target.position);
                }
               
            }
        }

        //Blendtree Parameter auf Geschwindigkeit von NavMesh Agent setzen
        _animator.SetFloat (_speedParameterHash, _agent.velocity.magnitude);

       
    }
}
