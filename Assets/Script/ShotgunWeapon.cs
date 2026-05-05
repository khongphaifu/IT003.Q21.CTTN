using System.Diagnostics;
using System.Numerics;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Vector3 = UnityEngine.Vector3;
using Vector2 = UnityEngine.Vector2;
using Quaternion = UnityEngine.Quaternion;
using System.Collections.Generic;
using System.Collections;

public class ShotgunWeapon : MonoBehaviour
{
    public Transform muzzlePoint; 
    public Animator animator; 
    public GameObject muzzleFlashPrefab;
    public LayerMask enemyMask;
    public ParticleSystem muzzleFlash;
    public GameObject ShotLinePrefab;
    public GameObject hitEffectPrefab;

    private Player player;
    private GameManager gameManager;
    private Enemy enemy;
    private CameraShake camShake;

    Camera cam;

    void Awake()
    {
        cam = Camera.main;
        player = FindAnyObjectByType<Player>();
        gameManager = FindAnyObjectByType<GameManager>();
        enemy = FindAnyObjectByType<Enemy>();
        camShake = FindAnyObjectByType<CameraShake>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(Time.time >= player.nextFireTime && gameManager.isShootting)
        {
            gameManager.isShootting = false;
            animator.SetBool("isShootting", false);
        }
    }

    public bool Shotgun()
    {
        if(gameManager.isReloading || gameManager.isShootting) return false;
        if (Input.GetMouseButtonDown(0) && player.ammo > 0)
        {
            Debug.Log("Shotgun fired!");
            Shoot();
            return true;
        }
        else if (Input.GetKeyDown(KeyCode.R) && player.ammo < player.maxAmmo && player.ammobag > 0)
        {
            Debug.Log("Reloading...");
            StartCoroutine(Reload());
            return true;
        }
        return false;
    }

    void Shoot()
    {
        if(Time.time < player.nextFireTime) return;
        player.nextFireTime = Time.time + player.firerate;
        player.ammo--;
        Debug.Log($"Ammo left: {player.ammo}, Ammobag left: {player.ammobag}");

        gameManager.isShootting = true;
        animator.SetBool("isShootting", true);

        FireShotgun();
        if (camShake != null)
        {
            StartCoroutine(camShake.Shake(0.1f, 0.12f));
        }
        
    }

    public Vector2 GetAimDirection()
    {
        Vector3 mouseWorld = cam.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = 0f;

        Vector2 dir = (mouseWorld - muzzlePoint.position).normalized;
        if (dir == Vector2.zero) dir = Vector2.right;

        return dir;
    }

    void FireShotgun()
    {
        Vector2 origin = muzzlePoint.position;
        Vector2 baseDir = GetAimDirection();
        float halfSpread = player.spreadAngle * 0.5f;

        for(int i = 1; i <= player.pelletcount; i++)
        {
            float angle = Random.Range(-halfSpread, halfSpread);
            Vector2 pelletdir = Quaternion.Euler(0f, 0f, angle) * baseDir;

            RaycastHit2D hit = Physics2D.Raycast(origin, pelletdir, player.range, enemyMask);
            Debug.DrawRay(origin, pelletdir * player.range, Color.yellow, 0.2f);

            Vector2 endPoint = origin + pelletdir * player.range;
            if(hit.collider != null)
            {
                Enemy enemy = hit.collider.GetComponent<Enemy>();
                if(enemy != null)
                {
                    Debug.Log($"Pellet hit {enemy.name} for {player.damageperpellet} damage");
                    enemy.TakeDamage(player.damageperpellet);
                }

                if (hitEffectPrefab != null)
                {
                    GameObject hitFx = Instantiate(hitEffectPrefab, hit.point, Quaternion.identity);
                    Destroy(hitFx, 0.2f);
                }
            }

            if (ShotLinePrefab != null)
            {
                StartCoroutine(ShowShotLine(origin, endPoint));
            }
        }
    }


    IEnumerator Reload()
    {
        if (gameManager.isReloading) yield break;

        gameManager.isReloading = true;
        
        animator.SetBool("isReloading", true);
        
        // yield return new WaitForSeconds(player.reloadTime);
        while(player.ammo < player.maxAmmo && player.ammobag > 0)
        {
            yield return new WaitForSeconds(player.reloadTime);
            player.ammobag--;
            player.ammo++;
        }
        gameManager.isReloading = false;
        
        animator.SetBool("isReloading", false);
        
    }

    IEnumerator ShowShotLine(Vector2 start, Vector2 end)
    {
        GameObject lineObj = Instantiate(ShotLinePrefab);
        LineRenderer lr = lineObj.GetComponent<LineRenderer>();

        lr.SetPosition(0, start);
        lr.SetPosition(1, end);

        yield return new WaitForSeconds(0.03f);
        Destroy(lineObj);
    }
}
