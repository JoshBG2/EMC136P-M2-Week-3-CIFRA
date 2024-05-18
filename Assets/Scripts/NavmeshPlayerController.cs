using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class NavmeshPlayerController : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent agent;
    private float playerHP = 1f;
    private Vector3 spawnPosition;

    public delegate void DamageEvent(float damage);
    public static event DamageEvent OnTakeDamage;

    void Start()
    {
        spawnPosition = transform.position;
    }

    void Update()
    {
        MovePlayer();
    }

    void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("EndPlatform"))
        {
            SceneManager.LoadScene("Win Scene");
        }

    }

    void MovePlayer()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(moveHorizontal, 0, moveVertical).normalized;

        if (moveDirection.magnitude >= 0.1f)
        {
            agent.Move(moveDirection * agent.speed * Time.deltaTime);

            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * agent.angularSpeed);
        }
    }

    public void TakeDamage(float damage)
    {
        playerHP -= damage;

        if (playerHP <= 0) 
        {
            transform.position = spawnPosition;
            playerHP = 1f;
            Debug.Log("Player respawned.");
        }
    }

}