using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class RaceAI : MonoBehaviour
{
    public Transform[] goals; // Array di obiettivi
    public Transform goal; // Obiettivo attuale
    public float goalReachedThreshold = 1.0f; // Distanza per considerare l'obiettivo raggiunto
    public float jumpForce = 5f;
    public float jumpDistance = 2f;
    public float jumpCooldown = 1f;

    private NavMeshAgent agent;
    private Rigidbody rb;
    private Animator animator;
    private bool isJumping = false;
    private float lastJumpTime;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        if (agent == null)
        {
            Debug.LogError("NavMeshAgent component is missing.");
            return;
        }

        if (!agent.isOnNavMesh)
        {
            Debug.LogError("NavMeshAgent is not placed on a NavMesh.");
            return;
        }

        if (goal != null)
        {
            agent.SetDestination(goal.position);
        }
        else
        {
            Debug.LogError("Goal is not set for RaceAI.");
        }

        lastJumpTime = -jumpCooldown; 
    }

    void Update()
    {
        if (agent == null || !agent.isOnNavMesh)
        {
            Debug.LogError("NavMeshAgent is not on the NavMesh in Update.");
            return;
        }

        if (goal != null)
        {
            agent.SetDestination(goal.position);
        }

        CheckForObstacles();
    }

    void CheckForObstacles()
    {
        if (isJumping || Time.time - lastJumpTime < jumpCooldown)
        {
            return;
        }

        RaycastHit hit;
        Vector3 origin = transform.position + Vector3.up * 0.7f; 

        if (Physics.Raycast(origin, transform.forward, out hit, jumpDistance))
        {
            if (hit.collider.CompareTag("JumpObstacle"))
            {
                Debug.Log("Obstacle detected. Initiating jump.");
                StartCoroutine(Jump());
            }
        }
    }


    IEnumerator Jump()
    {
        isJumping = true;
        agent.enabled = false;
        
        Debug.Log("Jumping");
        
        if (animator != null)
        {
            animator.SetTrigger("Jump");
        }
        
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        
        yield return new WaitForSeconds(jumpCooldown);

        agent.enabled = true;
        isJumping = false;
        lastJumpTime = Time.time;
    }

    void OnDrawGizmos()
    {
        // Disegna il raggio di rilevamento degli ostacoli
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + Vector3.up * 0.5f, transform.position + Vector3.up * 0.5f + transform.forward * jumpDistance);

        // Disegna il percorso del NavMeshAgent
        if (agent != null && agent.path != null)
        {
            Gizmos.color = Color.green;
            var path = agent.path;
            Vector3 previousCorner = transform.position;

            foreach (var corner in path.corners)
            {
                Gizmos.DrawLine(previousCorner, corner);
                previousCorner = corner;
            }
        }
    }
}
