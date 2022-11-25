using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    [SerializeField] CharacterData characterData;
    [SerializeField] Animator animator;

    [Header("GEARS")]
    [SerializeField] Weapon weapon;
    [SerializeField] Weapon shield;

    [SerializeField] float speed;

    [Header("RANGES")]
    [SerializeField] float detectionRange;
    [SerializeField] float minRange;

    [Header("ATTACH")]
    [SerializeField] float attackRange;
    [SerializeField] float attackDelay;
    [SerializeField] float attackDuration;

    [Header("DEFEND")]
    [SerializeField] int defendChance;
    [SerializeField] float defendDelay;
    [SerializeField] float defendDuration;

    [Header("DASH")]
    [SerializeField] int dashChance;
    [SerializeField] float dashPower;
    [SerializeField] float dashingDuration;
    [SerializeField] float dashCooldown;

    [Header("ENEMY")]
    [SerializeField] LayerMask enemyMask;

    Coroutine attackCoroutine;
    Coroutine defendCoroutine;
    Coroutine dashCoroutine;

    private HitResponder hitResponder;
    private Transform target;

    private ISpring spring;
    private ILockOnTarget lockOnTarget;

    [Header("HELPERS")]
    [SerializeField] List<Transform> groundRaycastSources;

    // Start is called before the first frame update
    void Start()
    {
        lockOnTarget = GetComponent<LockOnTarget>();

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

        lookAt -= transform.position;

        transform.rotation = Quaternion.LookRotation(Vector3.MoveTowards(transform.forward, lookAt, characterData.rotationSpeed * Time.deltaTime));

        float distance = Vector3.Distance(target.position, transform.position);
        float step = speed * Time.deltaTime;

        if (distance < detectionRange && distance > minRange)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
        }

        // Random DASH
        if (dashCoroutine == null && distance < minRange)
        {
            bool randomDash = Random.Range(0, 100) <= dashChance;

            if (randomDash)
            {
                dashCoroutine = StartCoroutine(Dash());
            }
        }

        // Random DEFEND

        if (defendCoroutine == null && attackCoroutine == null && shield != null && distance < minRange)
        {
            bool randomDefend = Random.Range(0, 100) <= defendChance;

            if (randomDefend)
            {
                defendCoroutine = StartCoroutine(Defend(defendDelay));
            }
        }

        if (attackCoroutine == null && defendCoroutine == null && distance < attackRange)
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

    IEnumerator Defend(float delay)
    {
        hitResponder = shield.Subject().GetComponent<HitResponder>();

        yield return new WaitForSeconds(delay);

        if (target == null || transform == null) yield return null;

        float distance = Vector3.Distance(target.position, transform.position);

        if (distance < attackRange)
        {
            animator.SetTrigger(shield.Trigger(ActionType.SIMPLE));
        }

        yield return new WaitForSeconds(2 * defendDuration / 3);

        if (distance < attackRange)
        {
            hitResponder._attack = true;
        }

        yield return new WaitForSeconds(defendDuration / 3);

        hitResponder._attack = false;
        defendCoroutine = null;
    }

    IEnumerator Dash()
    {
        characterData.RigidBody.useGravity = false;
        characterData.RigidBody.AddForce(-transform.forward * dashPower, ForceMode.Impulse);

        yield return new WaitForSeconds(dashingDuration);

        characterData.RigidBody.useGravity = true;

        yield return new WaitForSeconds(dashCooldown);

        dashCoroutine = null;
    }
}