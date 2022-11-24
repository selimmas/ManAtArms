using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] Animator animator;

    [SerializeField] Transform target;

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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRange, enemyMask);

        if (target == null) return;

        float distance = Vector3.Distance(target.position, transform.position);

        if (distance < detectionRange && distance > minRange)
        {
            float step = speed * Time.deltaTime;

            transform.LookAt(target.position);

            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
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