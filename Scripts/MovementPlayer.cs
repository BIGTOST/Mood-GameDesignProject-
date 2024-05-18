using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPlayer : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 6f;
    public float meleeRange = 1.5f;
    public float meleeDuration = 0.2f; // Tempo que dura o ataque
    public GameObject meleeColliderPrefab;// Colliders
    public GameObject projectilePrefab;
    public float projectileSpeed = 10f;
    public Transform firePoint;  // Ponto de onde os projéteis serão disparados

    public float dashSpeed = 20f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;
    private bool canDash = true;

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        HandleAttacks();
        HandleDash();
    }

    void MovePlayer()
    {
        //movimentos
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);

            controller.Move(direction * speed * Time.deltaTime);
        }
    }

    void HandleAttacks()
    {
        if (Input.GetButtonDown("Fire1"))  // Botão de ataque corpo a corpo (atualmente rato direito, mudar depois)
        {
            StartCoroutine(MeleeAttack());
        }

        if (Input.GetButtonDown("Fire2"))  // Botão de ataque à distância (atualmente rato esquerdo, mudar depois)
        {
            RangedAttack();
        }
    }

    IEnumerator MeleeAttack()
    {
        // Criar um collider temporário na frente do jogador para o ataque corpo a corpo
        GameObject meleeCollider = Instantiate(meleeColliderPrefab, transform.position + transform.forward * meleeRange, transform.rotation);
        meleeCollider.transform.SetParent(transform);  // Opcional: fazer o collider seguir o jogador

        yield return new WaitForSeconds(meleeDuration);

        Destroy(meleeCollider);
    }

    void RangedAttack()
    {
        // Instanciar e lançar o projétil
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = transform.forward * projectileSpeed;
        }
        // falta destroir objeto após um tempo
    }

    void HandleDash()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            StartCoroutine(Dash()); //Ativa o dash caso carregue no espaço
        }
    }

    IEnumerator Dash()
    {
        canDash = false;
        float startTime = Time.time;

        while (Time.time < startTime + dashDuration)
        {
            controller.Move(transform.forward * dashSpeed * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}