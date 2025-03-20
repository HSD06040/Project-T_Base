using UnityEngine;

public class Entity : MonoBehaviour
{
    #region Component
    public Rigidbody rb;
    public CapsuleCollider cd;
    public Animator anim;
    #endregion

    protected virtual void Awake()
    {
        rb   = GetComponent<Rigidbody>();
        cd   = GetComponent<CapsuleCollider>();
        anim = GetComponent<Animator>();
    }
}
