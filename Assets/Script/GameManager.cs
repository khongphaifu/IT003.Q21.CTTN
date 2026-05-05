using System.Diagnostics;
using System.Numerics;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Vector3 = UnityEngine.Vector3;
using Vector2 = UnityEngine.Vector2;
using Quaternion = UnityEngine.Quaternion;
using System.Collections.Generic;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    public GameObject prefabPawn;
    public GameObject prefabRook;
    public GameObject prefabKnight;
    public GameObject prefabBishop; 
    public GameObject prefabQueen;
    public GameObject prefabKing;
    
    private Player player;
    private BoardManager boardManager;
    private ShotgunWeapon shotgunWeapon;

    [Header("Check turn")]
    public bool yourturn;

    [Header("Đổi màu ô đi được")]
    public GameObject redTilePrefab; // Prefab của ô đỏ để đánh dấu ô có thể di chuyển
    private GameObject currentTile;
    float tileSizeX, tileSizeY;

    [Header("Cam")]
    public Camera cam;

    [Header("Enemy")]
    public List<Enemy> enemies = new List<Enemy>();
    public Enemy[,] board = new Enemy[8,8];

    [Header("Game Stats")]
    public bool isGameOver = false;
    public bool isReloading = false;
    public bool isShootting = false;
    public int countplayerturn = 0;

    
    void Awake()
    {
        boardManager = FindAnyObjectByType<BoardManager>();
        player = FindAnyObjectByType<Player>();
        shotgunWeapon = FindAnyObjectByType<ShotgunWeapon>();
    }
    void Start()
    {
        yourturn = true;
        currentTile = Instantiate(redTilePrefab); // Đảm bảo redTilePrefab được khởi tạo đúng cách trong Inspector
        currentTile.SetActive(false); // Ẩn ô đỏ ban đầu
        tileSizeX = boardManager.lightTilePrefab.GetComponent<SpriteRenderer>().bounds.size.x;
        tileSizeY = boardManager.lightTilePrefab.GetComponent<SpriteRenderer>().bounds.size.y;
        SpawnEnemy(); 
    }

    void Update()
    {
        if (yourturn)
        {
            if (MoveInput() || shotgunWeapon.Shotgun())
            {
                countplayerturn++;
            }
            if(countplayerturn == 2)
            {
                yourturn = false;
                countplayerturn = 0;
            }
        }
        if (!yourturn && !isShootting && !isReloading)
        {
            // Xử lý logic của đối thủ (AI hoặc người chơi khác)
            EnemyTurn();
            yourturn = true; // Sau khi đối thủ hoàn thành lượt của mình, chuyển lại lượt cho bạn
        }

        if(isGameOver)
        {
            Debug.Log("Game Over!");
            StartCoroutine(GameOver());
        }
    }

    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(0.5f); // Đợi một chút để hiển thị kết quả trước khi tạm dừng 
        Time.timeScale = 0f; // Tạm dừng game
    }


    bool MoveInput()
    {
        Vector3 mousepos = cam.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousepos, Vector2.zero, 0.1f);
        
        Tile hoverTile = null;
        if(hit.collider != null)
        {
            hoverTile = hit.collider.GetComponent<Tile>();
        }

        if(hoverTile != null && player.CanMove(hoverTile.x, hoverTile.y))
        {
            Vector3 position = new Vector3(
                hoverTile.x * tileSizeX - boardManager.offset,
                hoverTile.y * tileSizeY - boardManager.offset,
                0f
            );
            currentTile.transform.position = position;
            currentTile.SetActive(true);
        }
        else
        {
            currentTile.SetActive(false);
        }

        if(hoverTile != null && Input.GetMouseButtonDown(0) && player.CanMove(hoverTile.x, hoverTile.y))
        {
            player.SetPosition(hoverTile.x, hoverTile.y, hoverTile.posx, hoverTile.posy);
            return true;
        }
        return false;
    }

    void SpawnEnemy()
    {
        CreateEnemy("Pawn", 0, 6);
        CreateEnemy("Pawn", 1, 6);
        CreateEnemy("Pawn", 2, 6);
        CreateEnemy("Pawn", 3, 6);
        CreateEnemy("Pawn", 4, 6);
        // CreateEnemy("Rook", 1, 6);
        // CreateEnemy("Knight", 2, 6);
        // CreateEnemy("Bishop", 3, 6);
        // CreateEnemy("Queen", 4, 6);
        // CreateEnemy("King", 5, 6);
    }

    void CreateEnemy(string type, int x, int y)
    {
        // Logic để tạo
        float posx = x * tileSizeX - boardManager.offset;
        float posy = y * tileSizeY - boardManager.offset + boardManager.overlay;

        GameObject prefab = prefabPawn; // Mặc định là Pawn
        if(type == "Rook") prefab = prefabRook;
        else if(type == "Knight") prefab = prefabKnight;
        else if(type == "Bishop") prefab = prefabBishop;
        else if(type == "Queen") prefab = prefabQueen;
        else if(type == "King") prefab = prefabKing;


        GameObject newEnemy = Instantiate(prefab, new Vector3(posx, posy, 0), Quaternion.identity);
        newEnemy.name = $"Enemy_{type}_{x}_{y}";
        Enemy e = newEnemy.GetComponent<Enemy>();
        e.x = x;
        e.y = y;
        e.type = type;
        enemies.Add(e);
        board[x, y] = e; // Đặt enemy vào mảng board
    }

    float EvaluateMove(Enemy e, int x, int y)
    {
        float score = 0;

        // ăn được player → điểm cực thấp (ưu tiên)
        if (x == player.x && y == player.y) return -1000;

        // càng gần player càng tốt
        score += Mathf.Abs(player.x - x) + Mathf.Abs(player.y - y);

        // tránh đứng sát player (nếu không ăn được)
        int dx = Mathf.Abs(player.x - x);
        int dy = Mathf.Abs(player.y - y);

        if (dx <= 1 && dy <= 1)
            score += 5;

        return score;
    }

    void MoveEnemy(Enemy e, int newx, int newy)
    {
        float posx = newx * tileSizeX - boardManager.offset;
        float posy = newy * tileSizeY - boardManager.offset + boardManager.overlay;

        board[e.x, e.y] = null; // Xóa vị trí cũ trên board

        e.SetPosition(newx, newy);
        board[newx, newy] = e; // Cập nhật vị trí mới trên board
    }
    void EnemyTurn()
    {
        Enemy bestEnemy = null;
        int bestX = 0, bestY = 0;
        float bestScore = float.MaxValue;
        // Logic di chuyển và hành động của đối thủ (AI hoặc người chơi khác)
        foreach (Enemy e in enemies)
        {
            for(int x = 0; x < 8; x++)
            {
                for(int y = 0; y < 8; y++)
                {
                    if(e.CanMove(x, y))
                    {
                        float score = EvaluateMove(e, x, y);
                        if(score < bestScore)
                        {
                            bestScore = score;
                            bestEnemy = e;
                            bestX = x;
                            bestY = y;
                        }
                    }
                    if(e.type == "Pawn" && x == player.x && y == player.y && y - e.y == -1 && Mathf.Abs(x - e.x) == 1) // nếu không di chuyển được nhưng có thể ăn player
                    {
                        float score = EvaluateMove(e, x, y);
                        if(score < bestScore)
                        {
                            bestScore = score;
                            bestEnemy = e;
                            bestX = x;
                            bestY = y;
                        }
                    }
                }
            }
        }
        if(bestEnemy != null)
        {
            MoveEnemy(bestEnemy, bestX, bestY);
            if(bestEnemy.type == "Pawn" && bestY == 0) // nếu quân cờ là Pawn và đã đến hàng cuối cùng thì biến thành Queen
            {
                bestEnemy.type = "Queen";
                bestEnemy.maxHealth = 9;
                bestEnemy.curHealth = bestEnemy.maxHealth;
                bestEnemy.GetComponent<SpriteRenderer>().sprite = prefabQueen.GetComponent<SpriteRenderer>().sprite; // đổi sprite thành Queen
            }
            if(bestX == player.x && bestY == player.y)
            {
                Debug.Log("Player bị ăn!");
                // Xử lý khi player bị ăn (game over hoặc giảm HP)
                Destroy(player.gameObject);
                isGameOver = true;
            }
        }
    }
}

