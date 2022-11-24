using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    [SerializeField] CharacterData characterData;
    [SerializeField] Animator animator;

    [SerializeField] Weapon weapon;

    [SerializeField] float speed;
    [SerializeField] float detectionRange;
    [SerializeField] float minRange;
    [SerializeField] float attackRange;
    [SerializeField] float attackDelay;
    [SerializeField] float attackDuration;

    [SerializeField] LayerMask enemyMask;

    Coroutine attackCoroutine;

    private HitResponder hitResponder;
    private Transform target;

    private ISpring spring;

    [Header("HELPERS")]
    [SerializeField] List<Transform> groundRaycastSources;

    // Start is called before the first frame update
    void Start()
    {
        characterData.Subject = GetComponent<Transform>();
        characterData.RigidBody = GetComponent<Rigidbody>();
        characterData.GroundRaycastSources = groundRaycastSources;

        spring = new Spring();
    }

    // Update is called once per frame
    void Update()
    {
        spring.CheckForGround(characterData);

        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRange, enemyMask);

        float minDistance = Mathf.Infinity;

        foreach (Collider collider in colliders)
        {
            float colliderDistance = Vector3.Distance(collider.transform.position, transform.position);

            if (colliderDistance < minDistance)
            {
                target = collider.transform;
                minDistance = colliderDistance;
            }
        }

        if (target == null) return;

        Vector3 lookAt = target.position;
        lookAt.y = transform.position.y;

        transform.LookAt(lookAt);

        float distance = Vector3.Distance(target.position, transform.position);
        float step = speed * Time.deltaTime;

        if (distance < detectionRange && distance > minRange)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
        }

        if (distance < minRange)
        {
            transform.position = Vector3.MoveTowards(transform.position, -transform.forward, step);
        }

        if (attackCoroutine == null && distance < attackRange)
        {
            attackCoroutine = StartCoroutine(Attack(attackDelay));
        }
    }

    IEnumerator Attack(float delay)
    {
        hitResponder = weapon.Subject().GetComponent<HitResponder>();

        yield return new WaitForSeconds(delay);

        if (target == null || transform == null) yield return null;

        float distance = Vector3.Distance(target.position, transform.position);

        if (distance < attackRange)
        {
            animator.SetTrigger(weapon.Trigger(ActionType.SIMPLE));
        }

        yield return new WaitForSeconds(2 * attackDuration / 3);

        if (distance < attackRange)
        {
            hitResponder._attack = true;
        }

        yield return new WaitForSeconds(attackDuration / 3);

        hitResponder._attack = false;
        attackCoroutine = null;
    }
}