using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyScript : MonoBehaviour
{
    public float ViewRad;
    public float ViewAng;

    public LayerMask TargetMask;
    public LayerMask ObstacleMask;

    private Transform Player;


    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;   
    }

    private void Update()
    {
        FindTarget();
    }
    
    void FindTarget()
    {
        Collider2D[] TargetInViewRadius = Physics2D.OverlapCircleAll(transform.position, ViewRad, TargetMask);

        foreach (Collider2D target in TargetInViewRadius)
        {
            Transform TargetTransform = target.transform;
            Vector2 dirToTarget = (target.transform.position - transform.position).normalized;


            if (Vector2.Angle(transform.up, dirToTarget) < ViewAng / 2)
            {
                float ToTarget = Vector2 .Distance(transform.position, TargetTransform.position);

                if (!Physics.Raycast(transform.position, dirToTarget, ToTarget, ObstacleMask))
                {
                    Debug.Log("Player Detected");
                }
            }

        }
    }

    

    public Vector2 DirFromAng(float angleInDegrees, bool angleIsGlobal)
    {
        if(!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.z;
        }
        return new Vector2(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, ViewRad);

        Vector2 viewAngA = DirFromAng(-ViewAng / 2, false);
        Vector2 viewAngB = DirFromAng(ViewAng / 2, false);

        Gizmos.DrawLine(transform.position, (Vector2)transform.position + viewAngA * ViewRad);
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + viewAngB * ViewRad);
    }

}
