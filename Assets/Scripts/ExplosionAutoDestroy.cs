using UnityEngine;

public class ExplosionAutoDestroy : MonoBehaviour
{
    void Start()
    {
        float animLength = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
        Destroy(gameObject, animLength);
    }
}
