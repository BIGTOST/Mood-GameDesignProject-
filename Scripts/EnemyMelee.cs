using UnityEngine;
using UnityEngine.AI;

public class EnemyMelee : MonoBehaviour
{
    public Transform player; // Refer�ncia ao transform do jogador
    public float attackRange = 2f; // Dist�ncia m�nima para iniciar o ataque
    public float attackCooldown = 1f; // Tempo de recarga entre ataques
    private float lastAttackTime = 0f; // Armazena o �ltimo tempo de ataque
    private NavMeshAgent agent; // Refer�ncia ao NavMeshAgent

    void Start()
    {
        agent = GetComponent<NavMeshAgent>(); // Inicializa a refer�ncia ao NavMeshAgent

        // Verificar se o agente est� corretamente configurado
        if (agent == null)
        {
            Debug.LogError("NavMeshAgent n�o encontrado no inimigo.");
        }
    }

    void Update()
    {
        if (player != null && agent != null)
        {
            // Verificar se o agente est� ativo e em um NavMesh
            if (!agent.isOnNavMesh)
            {
                Debug.LogError("NavMeshAgent n�o est� no NavMesh.");
                return;
            }

            float distanceToPlayer = Vector3.Distance(transform.position, player.position); // Calcula a dist�ncia do inimigo ao jogador

            if (distanceToPlayer > attackRange)
            {
                // Move-se em dire��o ao jogador
                agent.SetDestination(player.position);
            }
            else
            {
                // Para de se mover e ataca
                agent.SetDestination(transform.position);

                if (Time.time > lastAttackTime + attackCooldown)
                {
                    AttackPlayer(); 
                    lastAttackTime = Time.time;
                }
            }
        }
    }

    void AttackPlayer()
    {
        // Ainda falta a l�gica do ataque
        Debug.Log("Atacando o jogador!");
    }
}
