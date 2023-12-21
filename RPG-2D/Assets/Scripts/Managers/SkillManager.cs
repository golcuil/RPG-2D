using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager Instance;
    public DashSkill dash { get; private set; }
    public CloneSkill clone { get; private set; }
    public SwordSkill sword { get; private set; }

    private void Awake()
    {
        if (Instance != null)
            Destroy(this.gameObject);
        else
            Instance = this;
    }


    private void Start()
    {
        dash = GetComponent<DashSkill>();
        clone = GetComponent<CloneSkill>();
        sword = GetComponent<SwordSkill>();
    }
}
