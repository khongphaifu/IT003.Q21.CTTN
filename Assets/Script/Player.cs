using System.Diagnostics;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;
using Debug = UnityEngine.Debug;

public class Player : MonoBehaviour
{
    [Header("Location")]
    public int x = 3;
    public int y = 0;
    private Vector3 targetPos;
    private BoardManager boardManager; 
    
    [Header("Stats")]
    public int ammo;
    public int maxAmmo;
    public int ammobag;
    public int maxammobag;
    public int pelletcount = 8;
    public float range = 10;
    public float reloadTime = 0.6f;
    public float spreadAngle = 18f;
    public int damageperpellet = 1;
    public float firerate = 0.7f;
    public float nextFireTime = 0f;

    void Awake()
    {
        maxAmmo = 2;
        ammo = maxAmmo;
        maxammobag = 10;
        ammobag = maxammobag;
        boardManager = FindAnyObjectByType<BoardManager>();
    }

    void Start()
    {
        boardManager.GenBoard(); // tạo bàn cờ trước khi lấy tọa độ của các ô
        GameObject st = GameObject.Find("Tile_3_0");
        Tile tile = st.GetComponent<Tile>();
        transform.position = new Vector3(tile.posx, tile.posy, 0);
        // Debug.Log($"Player initial position: {transform.position}");
        targetPos = new Vector3(tile.posx, tile.posy, 0);
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * 10);
    }

    public void SetPosition(int newX, int newY, float posX, float posY)
    {
        x = newX;
        y = newY;
        // GameObject st = GameObject.Find($"Tile_{x}_{y}");
        // Tile tile = st.GetComponent<Tile>();
        targetPos = new Vector3(posX, posY, 0);
    }

    public bool CanMove(int targetX, int targetY)
    {
        int dx = Mathf.Abs(targetX - x);
        int dy = Mathf.Abs(targetY - y);

        if (targetX < 0 || targetX >= 8 || targetY < 0 || targetY >= 8) return false;
        if(dx == 0 && dy == 0) return false; // không di chuyển
        return dx <= 1 && dy <= 1; // đi như vua
    }
}
