using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    [Header("GENERAL")]
    [SerializeField] private State _initialState;
    [SerializeField] private CharacterData _data;

    [Header("HELPERS")]
    [SerializeField] List<Transform> groundRaycastSources;

    private IState _currentState;

    public WeaponManager _weaponManager;

    private void Awake()
    {
        _currentState = StatePicker.GetState(_initialState, this, _data);

        _data.CurrentStance = _currentState.Stance();

        _data.Subject = GetComponent<Transform>();  
        _data.RigidBody = GetComponent<Rigidbody>();
        _data.Gears = GetComponent<Gears>();

        _data.GroundRaycastSources = groundRaycastSources;

        _weaponManager = new WeaponManager(_data);
        _weaponManager.Initialize();
    }

    private void Start()
    {
        if(_data.Gears != null)
        {
            _weaponManager.EquipAll(_data.Gears);
        }
    }

    private void Update()
    {
        _currentState.OnStateBaseUpdate();
        _currentState.OnStateUpdate();
    }

    private void FixedUpdate()
    {
        _currentState.OnStateBaseFixedUpdate();
        _currentState.OnStateFixedUpdate();
    }

    public void TransitionToState(IState state)
    {
        _currentState.OnStateExit();

        if (state.Stance() != _data.CurrentStance)
        {
            _data.CurrentStance = state.Stance();
        }

        _currentState = state;
        _currentState.OnStateEnter();

        if (_data.debugMode)
        {
            Debug.Log("CURRENT STANCE : " + _data.CurrentStance.ToString());
            Debug.Log("CURRENT STATE : " + _currentState.ToString());
        }
    }

    private void OnDrawGizmos()
    {
        if(_data.debugMode && _currentState != null)
        {
            _currentState.OnDrawGizmos();
        }
    }
}
