using UnityEngine;

public class PlayerBow : MonoBehaviour
{
    public Transform launchPoint;
    public GameObject arrowPrefab;

    private Vector2 aimDirection = Vector2.right;

    private float shootCooldown = .5f;
    private float shootTimer;

    void Update()
    {
        shootTimer -= Time.deltaTime;
        HandleAiming();

        if (Input.GetButtonDown("Shoot") && shootTimer <= 0)
        {
            Shoot();
        }
    }

    private void HandleAiming()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        if(horizontal != 0 || vertical != 0)
        {
            aimDirection = new Vector2(horizontal, vertical).normalized;
        }
    }

    public void Shoot()
    {
        ArrowScript arrow = Instantiate(arrowPrefab, launchPoint.position, Quaternion.identity).GetComponent<ArrowScript>();
        arrow.direction = aimDirection;
        shootTimer = shootCooldown;
    }
}
