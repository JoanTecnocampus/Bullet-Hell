using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    private GameObject Player;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        target = Player.transform;
        //AudioEnemyShoot = GetComponents<AudioSource>();
    }
    
    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, transform.position.z);
    }
}
