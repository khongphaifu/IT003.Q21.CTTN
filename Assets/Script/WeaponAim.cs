using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;
using Vector2 = UnityEngine.Vector2;
using Quaternion = UnityEngine.Quaternion;

public class EnemyStats : MonoBehaviour
{
    [Header("Refs")]
    public Transform weaponPivot;
    public Transform muzzlePoint;
    public SpriteRenderer bodyRenderer;
    public SpriteRenderer weaponRenderer;

    [Header("Aim")]
    public float spriteAngleOffset = 0f;
    public Vector3 rightOffset = new Vector3(0.18f, 0.02f, 0f);
    public Vector3 leftOffset = new Vector3(-0.18f, 0.02f, 0f);
    
    Camera cam;

    private void Awake() {
        cam = Camera.main;
    }

    void Update()
    {
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f; // Đảm bảo z = 0 để tránh lỗi khi tính toán góc

        Vector2 direction = (mousePos - weaponPivot.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        //xoay vũ khí theo hướng chuột
        weaponPivot.rotation = Quaternion.Euler(0f, 0f, angle + spriteAngleOffset);

        //đổi hướng vũ khí và vị trí pivot nếu chuột ở bên trái hoặc phải
        if (angle > 90 || angle < -90)
        {
            weaponRenderer.flipY = true;
            weaponPivot.localPosition = leftOffset;
        }
        else
        {
            weaponRenderer.flipY = false;
            weaponPivot.localPosition = rightOffset;
        }
        // bool facingRight = direction.x >= 0f;
        // //đổi sorting order để vũ khí không bị che bởi thân khi quay sang trái
        // weaponRenderer.sortingOrder = facingRight ? 10 : -1;
    }
}
