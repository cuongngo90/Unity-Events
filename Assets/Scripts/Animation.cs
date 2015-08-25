using UnityEngine;
using System.Collections;

public class Animation : MonoBehaviour
{
    private Dead _dead;
    private Skill _skill;
    
    void Start()
    {
        _dead = GetComponent<Dead>();
        _skill = GetComponent<Skill>();
    }
}
