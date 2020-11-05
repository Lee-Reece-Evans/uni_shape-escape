using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector2 startPos;
    public Sprite standard;
    public Sprite glowing;
    private Animator anim;
    public float initialAnimDelay;

    private bool hasCollided = false;
    private Transform doorPos;
    private bool isDragged = false;
    public string blockname;

    private void Start()
    {
        anim = GetComponent<Animator>();
        InitialiseAnimation();
    }

    private void OnEnable()
    {
        InitialiseAnimation();
    }

    private void OnDisable()
    {
        CancelInvoke("PlayAnimation");
    }

    private void Update()
    {
        if (hasCollided && isDragged == false)
        {
            transform.position = Vector2.MoveTowards(transform.position, doorPos.position, Time.deltaTime * 500f);

            if (Vector2.Distance(doorPos.position, transform.position) < 5)
            {
                SFXManager.instance.PlayDragDropMenuSFX();
                doSomething(blockname);
            }
        }
    }

    private void InitialiseAnimation()
    {
        if (anim != null)
        {
            InvokeRepeating("PlayAnimation", initialAnimDelay, 2);
        }
    }

    private void PlayAnimation()
    {
        if(!isDragged)
        {
            anim.Play("ShapeShake", 0, 0);
        }
    }

    private void DisableAnimation()
    {
        anim.enabled = false;
        transform.rotation = Quaternion.identity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag(blockname))
        {
            hasCollided = true;
            doorPos = collision.transform;
            collision.GetComponent<Image>().sprite = glowing;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(blockname))
        {
            hasCollided = false;
            collision.GetComponent<Image>().sprite = standard;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (startPos == Vector2.zero)
        {
            startPos = transform.position;
        }

        isDragged = true;

        DisableAnimation();
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position += new Vector3(eventData.delta.x, eventData.delta.y, 0f);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragged = false;

        if (!hasCollided)
            ResetShape();
    }

    private void ResetShape()
    {
        anim.enabled = true;
        transform.position = startPos;
    }

    private void doSomething(string blockname)
    {
        if (blockname == "exit")
        {
            MenuController.instance.ExitApplication();
        }
        else if (blockname == "options")
        {
            MenuController.instance.ShowOptionsMenu();

        }
        else if (blockname == "play")
        {
            MenuController.instance.TransitiontToMenu("LevelMenu");
        }
        ResetShape();
    }
}
