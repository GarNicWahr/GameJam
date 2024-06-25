using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshAgentController_domi : MonoBehaviour
{
    public Transform Target;
    private NavMeshAgent _agent;
    private Camera _rayCamera;
    public LayerMask LayerMask;
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
        Ray ray = _rayCamera.ScreenPointToRay(Input.mousePosition);
        if(Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, LayerMask))
            {
                if(raycastHit.collider.gameObject.layer.Equals(LayerMask.NameToLayer("Ground")))
                {
                    Target.position = raycastHit.point;
                    _agent.SetDestination(Target.position);
                }
            }
        }
        //Blendtree Parameter auf Geschwindigkeit des Nav Mesh Agents setzen
        _animator.SetFloat(_speedParameterHash, _agent.velocity.magnitude);

    }
}
