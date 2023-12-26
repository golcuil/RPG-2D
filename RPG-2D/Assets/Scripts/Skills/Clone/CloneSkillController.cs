using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneSkillController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Animator anim;
    [SerializeField] float colorLoosingSoeed;

    private float cloneTimer;

    [SerializeField] private Transform attackCheck;
    [SerializeField] private float attackCheckRadius;
    private Transform closestEnemy;
    private int facingDirection = 1;

    private bool canDuplicateClone;
    private float chanceToDuplicate;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        cloneTimer -= Time.deltaTime;

        if(cloneTimer < 0)
        {
            spriteRenderer.color = new Color(1, 1, 1, spriteRenderer.color.a - (Time.deltaTime * colorLoosingSoeed));

            if (spriteRenderer.color.a <= 0)
                Destroy(gameObject);
        }
    }

    public void SetupClone(Transform _newTransform, float _cloneDuration, bool _canAttack, Vector3 _offset, Transform _closestEnemy, bool _canDuplicateClone, float _chanceToDuplicate)
    {
        if (_canAttack)
            anim.SetInteger("AttackNumber", Random.Range(1, 3));

        transform.position = _newTransform.position + _offset;
        cloneTimer = _cloneDuration;

        closestEnemy = _closestEnemy;
        canDuplicateClone = _canDuplicateClone;
        chanceToDuplicate = _chanceToDuplicate;
        FaceClosestTarget();
    }


    private void AnimationTrigger()
    {
        cloneTimer = -.1f;
    }

    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackCheck.position, attackCheckRadius);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                hit.GetComponent<Enemy>().Damage();

                if(canDuplicateClone)
                {
                    if(Random.Range(0,100) < chanceToDuplicate)
                    {
                        SkillManager.Instance.clone.CreateClone(hit.transform, new Vector2(.5f * facingDirection, 0));
                    }
                }
            }
        }
    }

    private void FaceClosestTarget()
    {   
        if(closestEnemy != null)
        {
            if (transform.position.x > closestEnemy.transform.position.x)
            {
                facingDirection = -1;
                transform.Rotate(0, 180, 0);
            }
        }
    }
}
