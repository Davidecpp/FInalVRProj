using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public float chaseDistance = 10f;
    private NavMeshAgent _agent;

    [SerializeField] private float timer = 5;
    private float _bulletTime;

    public GameObject enemyBullet;
    public Transform spawnPoint;
    public float bulletSpeed;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);
        if (distanceToPlayer < chaseDistance)
        {
            ChasePlayer();
            Shoot();
        }
        else
        {
            _agent.isStopped = true; 
        }
    }

    private void Shoot()
    {
        _bulletTime -= Time.deltaTime;
        if (_bulletTime > 0) return;

        _bulletTime = timer;
        GameObject bullet = Instantiate(enemyBullet, spawnPoint.transform.position, spawnPoint.transform.rotation);
        Rigidbody bulletRig = bullet.GetComponent<Rigidbody>();
        bulletRig.AddForce(bulletRig.transform.forward * bulletSpeed);
        Destroy(bullet, 0.5f);
    }

    private void ChasePlayer()
    {
        _agent.isStopped = false;
        _agent.SetDestination(player.position);
    }
}