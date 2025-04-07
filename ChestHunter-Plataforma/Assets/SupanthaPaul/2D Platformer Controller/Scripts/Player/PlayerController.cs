using UnityEngine;

namespace SupanthaPaul
{
	public class PlayerController : MonoBehaviour
	{
		[SerializeField] private float speed;
		[Header("Jumping")]
		[SerializeField] private float jumpForce;
		[SerializeField] private float fallMultiplier;
		[SerializeField] private Transform groundCheck;
		[SerializeField] private float groundCheckRadius;
		[SerializeField] private LayerMask whatIsGround;
		[SerializeField] private int extraJumpCount = 1;

		[HideInInspector] public bool isGrounded;
		[HideInInspector] public float moveInput;
		[HideInInspector] public bool canMove = true;
		[HideInInspector] public bool isCurrentlyPlayable = false;

		private Rigidbody2D m_rb;
		private ParticleSystem m_dustParticle;
		private bool m_facingRight = true;
		private readonly float m_groundedRememberTime = 0.25f;
		private float m_groundedRemember = 0f;
		private int m_extraJumps;
		private float m_extraJumpForce;

		public GameObject dialogueBox;

		void Start()
		{
			if (transform.CompareTag("Player"))
				isCurrentlyPlayable = true;

			m_extraJumps = extraJumpCount;
			m_extraJumpForce = jumpForce * 0.7f;

			m_rb = GetComponent<Rigidbody2D>();
			m_dustParticle = GetComponentInChildren<ParticleSystem>();
		}

		private void FixedUpdate()
		{
			isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

			if (isCurrentlyPlayable)
			{
				if (canMove)
					m_rb.velocity = new Vector2(moveInput * speed, m_rb.velocity.y);
				else
					m_rb.velocity = new Vector2(0f, m_rb.velocity.y);

				if (m_rb.velocity.y < 0f)
				{
					m_rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
				}

				if (!m_facingRight && moveInput > 0f)
					Flip();
				else if (m_facingRight && moveInput < 0f)
					Flip();

				float playerVelocityMag = m_rb.velocity.sqrMagnitude;
			}
		}

		private void Update()
		{
			moveInput = InputSystem.HorizontalRaw();

			if (isGrounded)
			{
				m_extraJumps = extraJumpCount;
			}

			m_groundedRemember -= Time.deltaTime;
			if (isGrounded)
				m_groundedRemember = m_groundedRememberTime;

			if (!isCurrentlyPlayable) return;

			if (InputSystem.Jump() && m_extraJumps > 0 && !isGrounded)
			{
				m_rb.velocity = new Vector2(m_rb.velocity.x, m_extraJumpForce);
				m_extraJumps--;
			}
			else if (InputSystem.Jump() && (isGrounded || m_groundedRemember > 0f))
			{
				m_rb.velocity = new Vector2(m_rb.velocity.x, jumpForce);
			}

			if (transform.position.x > -6.3f && transform.position.x < -4.5f)
			{
				if (Input.GetKeyDown(KeyCode.F))
				{
					dialogueBox.SetActive(true);
				}
			}
		}

		void Flip()
		{
			m_facingRight = !m_facingRight;
			Vector3 scale = transform.localScale;
			scale.x *= -1;
			transform.localScale = scale;
		}

		private void OnDrawGizmosSelected()
		{
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
		}
	}
}
