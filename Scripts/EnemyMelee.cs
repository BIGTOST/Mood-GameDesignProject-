using UnityEngine;
using UnityEngine.AI;

public class EnemyMelee : MonoBehaviour
{
    public Transform player; // Referência ao transform do jogador
    public float attackRange = 2f; // Distância mínima para iniciar o ataque
    public float attackCooldown = 1f; // Tempo de recarga entre ataques
    private float lastAttackTime = 0f; // Armazena o último tempo de ataque
    private NavMeshAgent agent; // Referência ao NavMeshAgent

    void Start()
    {
        agent = GetComponent<NavMeshAgent>(); // Inicializa a referência ao NavMeshAgent

        // Verificar se o agente está corretamente configurado
        if (agent == null)
        {
            Debug.LogError("NavMeshAgent não encontrado no inimigo.");
        }
    }

    void Update()
    {
        if (player != null && agent != null)
        {
            // Verificar se o agente está ativo e em um NavMesh
            if (!agent.isOnNavMesh)
            {
                Debug.LogError("NavMeshAgent não está no NavMesh.");
                return;
            }

            float distanceToPlayer = Vector3.Distance(transform.position, player.position); // Calcula a distância do inimigo ao jogador

            if (distanceToPlayer > attackRange)
            {
                // Move-se em direção ao jogador
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
        // Ainda falta a lógica do ataque
        Debug.Log("Atacando o jogador!");
    }
}
