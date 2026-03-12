using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections; // Necessario per IEnumerator

public class PlayerMovements : MonoBehaviour
{
    [Header("Impostazioni Movimento")]
    public float speed = 5f;

    [Header("Impostazioni Salto")]
    public float jumpDuration = 0.6f;
    public float jumpHeight = 1.5f;
    public float jumpPushForce = 1.5f; // Spinta extra per superare il collider
    public Transform visualTransform;

    private Vector2 moveInput;
    private Animator animator;
    private Rigidbody2D rb;
    private bool isJumping = false;
    private bool isAttacking = false;

    private float lastX = 0;
    private float lastY = -1;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Se non hai trascinato il figlio "Visual" nell'inspector, usa se stesso
        if (visualTransform == null) visualTransform = transform;

        animator = visualTransform.GetComponent<Animator>();

        rb.freezeRotation = true;
        rb.gravityScale = 0;
    }

    void OnMove(InputValue value) => moveInput = value.Get<Vector2>();

    void OnJump(InputValue value)
    {
        if (value.isPressed && !isJumping && !isAttacking)
        {
            StartCoroutine(JumpRoutine());
        }
    }

    IEnumerator JumpRoutine()
    {
        isJumping = true;
        animator.SetTrigger("Jump");

        // Identifica i layer
        int playerLayer = gameObject.layer;
        int obstacleLayer = LayerMask.NameToLayer("Obstacles");

        // DISATTIVA COLLISIONE
        if (obstacleLayer != -1)
            Physics2D.IgnoreLayerCollision(playerLayer, obstacleLayer, true);

        Vector3 startPos = visualTransform.localPosition;

        // Calcola direzione salto: se fermo, usa l'ultima direzione guardata
        Vector2 jumpDir = moveInput.magnitude > 0 ? moveInput.normalized : new Vector2(lastX, lastY).normalized;

        float timer = 0;
        while (timer < jumpDuration)
        {
            timer += Time.deltaTime;
            float progress = timer / jumpDuration;

            // Arco visivo (Su/Giù)
            float h = 4f * jumpHeight * progress * (1f - progress);
            visualTransform.localPosition = new Vector3(startPos.x, startPos.y + h, startPos.z);

            // Spinta fisica per scavalcare l'ostacolo
            rb.linearVelocity = jumpDir * (speed * jumpPushForce);

            yield return null;
        }

        // Reset posizione e collisioni
        visualTransform.localPosition = startPos;
        if (obstacleLayer != -1)
            Physics2D.IgnoreLayerCollision(playerLayer, obstacleLayer, false);

        isJumping = false;
        rb.linearVelocity = Vector2.zero;
    }

    void OnAttack(InputValue value)
    {
        if (value.isPressed && !isJumping && !isAttacking)
        {
            StartCoroutine(AttackRoutine());
        }
    }

    IEnumerator AttackRoutine()
    {
        isAttacking = true;
        animator.SetTrigger("Attack");
        rb.linearVelocity = Vector2.zero;
        yield return new WaitForSeconds(0.4f);
        isAttacking = false;
    }

    void FixedUpdate()
    {
        if (!isJumping && !isAttacking)
        {
            rb.linearVelocity = moveInput * speed;
        }
    }

    void Update()
    {
        if (moveInput.magnitude > 0.1f && !isAttacking)
        {
            animator.SetBool("isWalking", true);
            lastX = moveInput.x;
            lastY = moveInput.y;

            // Flip dello sprite
            float scaleX = Mathf.Abs(moveInput.x) > Mathf.Abs(moveInput.y) ? Mathf.Sign(moveInput.x) : 1f;
            visualTransform.localScale = new Vector3(scaleX, 1f, 1f);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        animator.SetFloat("LastX", lastX);
        animator.SetFloat("LastY", lastY);
    }
}