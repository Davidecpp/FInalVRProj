using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbility : MonoBehaviour
{
    public String characterName;
    // Dash
    public float dashSpeed = 20f; 
    public float dashDuration = 0.5f; 
    private bool isDashing = false;
    public TrailRenderer trailRenderer;
    public GameObject dashSparkle;
    public AudioSource dashSound;
    
    // Venom cloud
    public GameObject venomSprayPrefab;
    public GameObject venomCloudPrefab;  
    public Transform venomSpawnPoint;   
    public float sprayCooldown = 5f;
    public float sprayDuration = 0.5f; 
    private bool canSpray = true;
    public AudioSource spraySound;

    public float abilityTime = 5.0f;
    public float abilityCooldown = 0;
    private CanvasManager _canvas;

    private void Start()
    {
        _canvas = FindObjectOfType<CanvasManager>();
        dashSound = GetComponent<AudioSource>();
        spraySound = GetComponent<AudioSource>();
        trailRenderer.emitting = false;
    }

    // Press R for ability
    void Update()
    {
        Debug.Log(abilityCooldown + _canvas.isAbiliting.ToString());
        if (abilityCooldown > 0)
        {
            abilityCooldown -= Time.deltaTime;
            _canvas.isAbiliting = true;
        }
        else
        {
            _canvas.isAbiliting = false;
        }
        if (Input.GetKeyDown(KeyCode.R) && abilityCooldown <= 0)
        {
            if (characterName.Equals("Deer") && !isDashing)
            {
                StartCoroutine(VerticalDash());
            }
            if (characterName.Equals("Snake"))
            {
                StartCoroutine(SprayVenom());
            }
        }
    }
    IEnumerator SprayVenom()
    {
        canSpray = false;
        var position = venomSpawnPoint.position;
        var rotation = venomSpawnPoint.rotation;
        GameObject spray = Instantiate(venomSprayPrefab, position, rotation);
        spraySound.Play();
        yield return new WaitForSeconds(sprayDuration);
        Destroy(spray);

        Vector3 cloudSpawnPosition = position + venomSpawnPoint.forward * 8;
        Instantiate(venomCloudPrefab, cloudSpawnPosition, rotation);
        yield return new WaitForSeconds(sprayCooldown);
        canSpray = true;
        abilityCooldown = abilityTime;
    }
    
    IEnumerator VerticalDash()
    {
        isDashing = true;
        float dashTime = 0f;
        dashSpeed = 1000;
        dashDuration = 0.05f;
        GameObject particles;
        
        trailRenderer.emitting = true;
        
        while (dashTime < dashDuration)
        {
            dashTime += Time.deltaTime;

            // Raycast to find obstacles
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, dashSpeed * Time.deltaTime))
            {
                // Interrupt the dash if there is an obstacle
                if (hit.collider != null)
                {
                    break; 
                }
            }
            dashSound.Play();
            transform.Translate(Vector3.forward * dashSpeed * Time.deltaTime);
            particles = Instantiate(dashSparkle, venomSpawnPoint.position, venomSpawnPoint.rotation);
            Destroy(particles, 1f);
            yield return null;
        }
        trailRenderer.emitting = false;
        isDashing = false;
        abilityCooldown = abilityTime;
    }
}