using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleHandler : MonoBehaviour
{
    public Animator animator;

    public RuntimeAnimatorController runtimeAnimatorController;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(4);
        animator.runtimeAnimatorController = runtimeAnimatorController;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
