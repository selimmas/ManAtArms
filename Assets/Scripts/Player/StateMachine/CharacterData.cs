using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[CreateAssetMenu(menuName = "Scriptables/Character DATA")]
public class CharacterData : ScriptableObject
{
    private Transform subject;
    private Rigidbody rigidBody;
    private Animator animator;
    private List<Transform> groundRaycastSources;
    private Gears gears;
    private IStance currentStance;

    [Header("SPRING")]

    public bool springEnabled;

    public LayerMask groundMask;

    public float rideHeight;
    public float springForce;
    public float dampForce;

    [Header("LOOK AT")]
    public bool lookAtEnabled;
    public float rotationSpeed;

    [Header("WALKING")]
    public float walkingSpeed;
    public float combatWalkingSpeed;

    [Header("Weapons")]
    public List<IWeapon> weapons;

    [Header("ENEMIES")]
    public bool lockOnEnabled;
    public float lockOnRotationSpeed;
    public LayerMask enemyLayer;
    public float detectionRange = 3f;

    [Header("ACTIONS")]
    public List<IAction> actions = new List<IAction>();

    [Header("DEBUG")]
    public bool debugMode;
    public TextMeshProUGUI debugText;


    public Rigidbody RigidBody { get => rigidBody; set => rigidBody = value; }
    public List<Transform> GroundRaycastSources { get => groundRaycastSources; set => groundRaycastSources = value; }
    public Transform Subject { get => subject; set => subject = value; }
    public Gears Gears { get => gears; set => gears = value; }
    public IStance CurrentStance { get => currentStance; set => currentStance = value; }
    public Animator Animator { get => animator; set => animator = value; }
}
