using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float JF, MS;
    private float dirx, diry;
    private Rigidbody2D rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        MS = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        dirx = 0f;
        diry = rb.linearVelocityY;

        if (name == "Player")
        {
            if (Input.GetKey(KeyCode.A)) dirx = -MS;
            if (Input.GetKey(KeyCode.D)) dirx = MS;

            if (Input.GetKey(KeyCode.Space) && isGrounded)
            {
                rb.linearVelocity = new Vector2(0, JF);
                isGrounded = false;
            }
        }
    }
    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(dirx, rb.linearVelocityY);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Player is on the ground!");
            isGrounded = true;
        }
    }

}
