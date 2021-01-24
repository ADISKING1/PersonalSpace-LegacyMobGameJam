using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charater : MonoBehaviour
{
    [Tooltip("1=H 2=V 3=/ 4=\\")]
    public int orientation;
    public float offset;
    [Space]
    public float speed;
    public float stopTime;
    public bool switchGoal;

    private float radius = 0.5f;
    private Vector2 GoalPos1;
    private Vector2 GoalPos2;

    private Vector2 GoalPos;

    private bool canMove;

    private Vector2 initialPos;
    private CircleCollider2D circleCollider2D;

    void SetCanMove()
    {
        canMove = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        circleCollider2D = GetComponent<CircleCollider2D>();
        //Init();
        SetVariables();
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;
        if (canMove)
        {
            transform.position = Vector2.MoveTowards(transform.position, GoalPos, step);
            SetCurrentGoal();
        }
    }
    void Init(int orientation, float offset, float radius, float speed, float stopTime)
    {
        this.orientation = orientation;
        this.offset = offset;
        this.radius = radius;
        this.speed = speed;
        this.stopTime = stopTime;
    }

    void SetVariables()
    {
        initialPos = transform.position;

        if (orientation == 1)       // -
        {
            GoalPos1 = new Vector2(initialPos.x + offset, initialPos.y);
            GoalPos2 = new Vector2(initialPos.x - offset, initialPos.y);
        }
        else if (orientation == 2)  // |
        {
            GoalPos1 = new Vector2(initialPos.x, initialPos.y + offset);
            GoalPos2 = new Vector2(initialPos.x, initialPos.y - offset);
        }
        else if (orientation == 3)  // /
        {
            GoalPos1 = new Vector2(initialPos.x + offset, initialPos.y + offset);
            GoalPos2 = new Vector2(initialPos.x - offset, initialPos.y - offset);
        }
        else if (orientation == 4)  // \
        {
            GoalPos1 = new Vector2(initialPos.x - offset, initialPos.y + offset);
            GoalPos2 = new Vector2(initialPos.x + offset, initialPos.y - offset);
        }
        if (!switchGoal)
            GoalPos = GoalPos1;
        else
            GoalPos = GoalPos2;

        circleCollider2D.radius = radius;

        canMove = true;
    }
    void SetCurrentGoal()
    {
        if (Vector2.Distance(GoalPos, transform.position) < 0.05f)
        {
            if (Vector2.Distance(GoalPos1, transform.position) < 0.05f)
            {
                GoalPos = GoalPos2;
                StopMoving();
            }
            else
            {
                GoalPos = GoalPos1;
                StopMoving();
            }
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Const.CHARACTERCOLOR;
        Gizmos.DrawWireSphere(transform.position, radius);
        Gizmos.DrawLine(GoalPos1, GoalPos2);
    }
    public void StopMoving()
    {
        canMove = false;
        Invoke("SetCanMove", stopTime);
    }
    public void SetGoalPos(Vector2 goalPos)
    {
        GoalPos = goalPos;
    }
}
