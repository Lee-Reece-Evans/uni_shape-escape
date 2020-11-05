using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("player variables")]
    public bool isCircle = true;
    public bool isTriangle;
    public bool isSquare;
    public GameObject playerPrefab;
    public ParticleSystem Explosion;
    public ParticleSystem impactExplosion;
    public GameObject arrow;
    private LineRenderer arrowLine;
    public GameObject arrowHead;

    private GameObject tmpPlayer;

    private Rigidbody2D rb;
    private Vector2 initialPos;
    private bool hasSplit = false;
    private Transform goal;
    private float dirX, dirY;
    public float speed = 10f;

    //Flicking Variables
    public float minDistanceX;
    public float minDistanceY;
    private Vector2 startPos;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        initialPos = transform.position;
        arrowLine = arrow.GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (SystemInfo.deviceType == DeviceType.Desktop)
        {
            dirX = Input.GetAxis("Horizontal");
            dirY = Input.GetAxis("Vertical");
        }
        else
        {
            dirX = Input.acceleration.x;
            dirY = Input.acceleration.y;

            //Flicking Logic
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        startPos = touch.position;
                        arrow.SetActive(true);
                        break;
                    case TouchPhase.Ended:
                        arrow.SetActive(false);
                        Vector2 swipeDirection = touch.position - startPos;
                        swipeDirection.Normalize();
                        float swipeDistance = (touch.position - startPos).magnitude;
                        swipeDistance = Mathf.Clamp(swipeDistance, 0f, 2000f);
                        rb.AddForce(swipeDirection * swipeDistance);
                        break;
                }
                // directional arrow
                arrow.transform.right = (Vector2)Camera.main.ScreenToWorldPoint(touch.position) - (Vector2)Camera.main.ScreenToWorldPoint(startPos);
                if ((touch.position - startPos).magnitude > 10f)
                    arrowLine.SetPosition(1, new Vector3(Mathf.Clamp(((touch.position - startPos).magnitude + 500f) / 500f, 0.5f, 5.0f), 0f, 0f));
                else
                    arrowLine.SetPosition(1, new Vector3(0f, 0f, 0f));
                arrowHead.transform.localPosition = arrowLine.GetPosition(1);
            }
        }
    }

    private void FixedUpdate()
    {
        rb.AddForce(new Vector2(dirX, dirY) * speed * Time.fixedDeltaTime);

        if (goal != null && Vector2.Distance(goal.position, transform.position) < 0.12 && rb.velocity.magnitude < 2.5)
        {
            rb.bodyType = RigidbodyType2D.Static;
            //set shape into goal position
            transform.position = goal.position;

            // set sorting order for other shapes to show ontop
            GetComponent<SpriteRenderer>().sortingOrder = 1;

            // disable arrow if not already
            if (arrow.activeSelf)
                arrow.SetActive(false);

            //disable physics on this shape
            rb.simulated = false;

            if (hasSplit)
            {
                if (tmpPlayer.transform.position != goal.position) // check if matching split object is not in goal
                {
                    SFXManager.instance.PlayPlayerSplitGoalMetSFX();
                }
                else // make split objects merge
                {
                    SFXManager.instance.PlayPlayerGoalMetSFX();
                    Destroy(tmpPlayer);
                    transform.localScale = new Vector2(transform.localScale.x * 1.65f, transform.localScale.y * 1.65f);
                    LevelManager.instance.GoalMet();
                    CameraShake.ShouldShake = true;
                    Instantiate(Explosion, transform.position, transform.rotation);
                }
            }
            else
            {
                SFXManager.instance.PlayPlayerGoalMetSFX();
                LevelManager.instance.GoalMet();
                CameraShake.ShouldShake = true;
                Instantiate(Explosion, transform.position, transform.rotation);
            }
            // disable this script
            this.enabled = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (ContactPoint2D contact in collision.contacts)
            Instantiate(impactExplosion, contact.point, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isCircle && collision.gameObject.tag == "CircleGoal")
        {
            goal = collision.gameObject.transform;
        }
        if (isTriangle && collision.gameObject.tag == "TriangleGoal")
        {
            goal = collision.gameObject.transform;
        }
        if (isSquare && collision.gameObject.tag == "SquareGoal")
        {
            goal = collision.gameObject.transform;
        }
        if (collision.gameObject.tag == "MovingEnemy")
        {
            if (collision.gameObject.GetComponent<MovingObstacle>().moveUpDown)
            {
                if (transform.position.x > collision.gameObject.transform.position.x)
                {
                    rb.velocity = new Vector2(15, rb.velocity.y);
                }
                else
                {
                    rb.velocity = new Vector2(-15, rb.velocity.y);
                }
            }
            else
            {
                if (transform.position.y > collision.gameObject.transform.position.y)
                {
                    rb.velocity = new Vector2(rb.velocity.x, 15);
                }
                else
                {
                    rb.velocity = new Vector2(rb.velocity.x, -15);
                }
            }
            SFXManager.instance.PlayPlayerHitMovingEnemySFX();
        }

        if (collision.gameObject.tag == "Enemy")
        {
            transform.position = initialPos;
            rb.velocity = Vector2.zero;

            SFXManager.instance.PlayPlayerHitEnemySFX();
        }

        if (collision.gameObject.tag == "Splitter")
        {
            if(!hasSplit)
            {
                SplitPlayer();
                SFXManager.instance.PlayPlayerSplitSFX();
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Fan")
        {
            rb.AddForce(collision.transform.right * 5,ForceMode2D.Force);
        }
    }

    private void SplitPlayer()
    {
        tmpPlayer = Instantiate(playerPrefab, transform.position, transform.rotation);
        tmpPlayer.transform.localScale = new Vector2(transform.localScale.x * 0.65f, transform.localScale.y * 0.65f);
        tmpPlayer.GetComponent<PlayerController>().tmpPlayer = this.gameObject;
        tmpPlayer.GetComponent<Rigidbody2D>().velocity = -rb.velocity;

        transform.localScale = new Vector2(transform.localScale.x * 0.65f, transform.localScale.y * 0.65f);
        tmpPlayer.GetComponent<PlayerController>().hasSplit = true;
        hasSplit = true;
    }
}
