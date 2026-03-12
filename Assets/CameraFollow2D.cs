using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    public Transform target;       // Il tuo player
    public float damping = 0.15f;  // Tempo di risposta (più basso = più veloce)
    public Vector3 offset = new Vector3(0, 0, -10); // Importante: Z deve essere -10!

    private Vector3 velocity = Vector3.zero;

    void FixedUpdate()
    {
        if (target != null)
        {
            // Calcoliamo la posizione target mantenendo la Z della telecamera
            Vector3 targetPosition = target.position + offset;

            // SmoothDamp è più fluido di Lerp per il movimento delle camere
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, damping);
        }
    }
}