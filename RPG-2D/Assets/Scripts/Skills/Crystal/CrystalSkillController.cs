using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalSkillController : MonoBehaviour
{
    private Animator anim => GetComponent<Animator>();
    private CircleCollider2D circleCollider => GetComponent<CircleCollider2D>();

    private float crystalExitsTimer;
    private bool canExplode;
    private bool canMove;
    private float moveSpeed;

    private bool canGrow;
    private float growSpeed = 5f;

    private Transform closestTarget;
    [SerializeField] private LayerMask whatIsEnemy;

    public void SetupCrystal(float _crystalDuration, bool _canExplode, bool _canMove, float _movespeed, Transform _closestTarget)
    {
        crystalExitsTimer = _crystalDuration;
        canExplode = _canExplode;
        canMove = _canMove;
        moveSpeed = _movespeed;
        closestTarget = _closestTarget;
    }

    private void Update()
    {
        crystalExitsTimer -= Time.deltaTime;

        if (crystalExitsTimer < 0)
        {
            FinishCrystal();
        }

        if (canMove)
        {
            transform.position = Vector2.MoveTowards(transform.position, closestTarget.position, moveSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, closestTarget.position) < 1)
            {
                FinishCrystal();
                canMove = false;
            }
        }

        if (canGrow)
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(3, 3), growSpeed * Time.deltaTime);
    }

    private void AnimationExplodeEvent()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, circleCollider.radius);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null)
                hit.GetComponent<Enemy>().Damage();
        }
    }

    public void FinishCrystal()
    {
        if (canExplode)
        {
            canGrow = true;
            anim.SetTrigger("Explode");
        }
        else
            SelfDestroy();
    }

    public void ChooseRandomEnemy()
    {
        float radius = SkillManager.Instance.blackhole.GetBlackHoleRadius();

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position,radius, whatIsEnemy);

        if(colliders.Length > 0)
            closestTarget = colliders[Random.Range(0, colliders.Length)].transform;
    }

    public void SelfDestroy() => Destroy(gameObject);
}
