using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombinedCharacterController : MonoBehaviour
{
    public GameObject characterA;
    public GameObject characterB;
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public int maxJumps = 2;
    public float controlInfluence = 0.1f;

    [Header("Squash & Stretch Settings")]
    public float stretchFactor = 0.5f;
    public float squashFactor = 0.8f;
    public float bounceThreshold = 5f;

    private bool isGrounded;
    private int jumpCount;
    private Rigidbody rb;
    private Vector3 originalScale;
    private bool isCombined = true;
    private Vector3 separationOffsetA;
    private Vector3 separationOffsetB;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        originalScale = transform.localScale;
        if (characterA != null && characterB != null)
        {
            CalculateSeparationOffsets();
        }
        else
        {
            Debug.LogError("CharacterA or CharacterB is not assigned in the inspector!");
        }
    }

    void Update()
    {
        if (isCombined)
        {
            HandleMovement();
            HandleJump();
            ApplySquashAndStretch();
        }
        else
        {
            PositionBetweenCharacters();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isCombined)
                SeparateCharacters();
            else
                CombineCharacters();
        }
    }

    void HandleMovement()
    {
        Vector3 moveDirection = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
            moveDirection += Vector3.forward;

        if (Input.GetKey(KeyCode.S))
            moveDirection += Vector3.back;

        if (Input.GetKey(KeyCode.J))
            moveDirection += Vector3.right;

        if (Input.GetKey(KeyCode.L))
            moveDirection += Vector3.left;

        rb.AddForce(moveDirection.normalized * moveSpeed, ForceMode.Force);
    }

    void HandleJump()
    {
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpCount = 1;
            isGrounded = false;
        }
        else if (!isGrounded && jumpCount < maxJumps && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpCount++;
        }
    }

    void ApplySquashAndStretch()
    {
        float speed = rb.linearVelocity.magnitude;

        if (speed > bounceThreshold)
        {
            float stretchAmount = 1 + (speed * stretchFactor * 0.01f);
            transform.localScale = new Vector3(originalScale.x / stretchAmount, originalScale.y * stretchAmount, originalScale.z / stretchAmount);
        }
        else if (isGrounded)
        {
            float squashAmount = 1 + (squashFactor * 0.1f);
            transform.localScale = new Vector3(originalScale.x * squashAmount, originalScale.y / squashAmount, originalScale.z * squashAmount);
        }
        else
        {
            transform.localScale = originalScale;
        }
    }

    void PositionBetweenCharacters()
    {
        if (characterA != null && characterB != null)
        {
            Vector3 midPoint = (characterA.transform.position + characterB.transform.position) / 2;
            transform.position = new Vector3(midPoint.x, transform.position.y, midPoint.z);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            jumpCount = 0;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    void CombineCharacters()
    {
        isCombined = true;
        characterA.SetActive(false);
        characterB.SetActive(false);
        gameObject.SetActive(true); 
    }

    void SeparateCharacters()
    {
        isCombined = false;

        if (characterA != null && characterB != null)
        {
            characterA.SetActive(true);
            characterB.SetActive(true);

            CalculateSeparationOffsets();
            characterA.transform.position = transform.position + separationOffsetA;
            characterB.transform.position = transform.position + separationOffsetB;
            transform.position = (characterA.transform.position + characterB.transform.position) / 2;

            gameObject.SetActive(false); 
        }
        else
        {
            Debug.LogError("CharacterA or CharacterB is not assigned when trying to separate!");
        }
    }

    void CalculateSeparationOffsets()
    {
        separationOffsetA = characterA.transform.position - transform.position;
        separationOffsetB = characterB.transform.position - transform.position;
    }
}
