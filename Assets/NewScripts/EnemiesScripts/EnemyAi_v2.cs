using UnityEngine.AI;
using UnityEngine;
/// <summary>
/// Enemy AI version 2. 
/// <para>
/// This script is used to control the enemy AI
/// </para>
/// <see cref="NavMeshAgent"/> component
/// </summary>
public class EnemyAi_v2 : MonoBehaviour
{

    private Transform player_pos;   // player position
    public float chaseDistance = 10f; 
    private NavMeshAgent _agent; // 

    [SerializeField] private float timer = 5;
    // private float _bulletTime;
    // public float bulletSpeed;
    // public GameObject enemyBullet;
    public Transform spawnPoint;

    // Start is called before the first frame update
    void Start()
    {   
        player_pos = GetComponent<Transform>();
        _agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
