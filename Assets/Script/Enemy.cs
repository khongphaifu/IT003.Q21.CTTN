using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Vector3 = UnityEngine.Vector3;
using Vector2 = UnityEngine.Vector2;
using Quaternion = UnityEngine.Quaternion;

public class Enemy : MonoBehaviour
{
    [Header("Location")]
    public int x;
    public int y;

    [Header("Stats")]
    public string type;
    public int curHealth;
    public int maxHealth;   

    Vector3 targetPos;
    private BoardManager boardManager;
    private GameManager gameManager;
    private Player player;
    // private GameObject enemy;
    float tileSizeX, tileSizeY;
    void Awake()
    {
        boardManager = FindAnyObjectByType<BoardManager>();
        gameManager = FindAnyObjectByType<GameManager>();
        player = FindAnyObjectByType<Player>();
    }
    void Start()
    {
        if(type == "King")
        {
            maxHealth = 10;
        }
        else if(type == "Knight")
        {
            maxHealth = 3;
        }
        else if(type == "Queen")
        {
            maxHealth = 9;
        }
        else if(type == "Rook")
        {
            maxHealth = 5;
        }
        else if(type == "Pawn")
        {
            maxHealth = 1;
        }
        else if(type == "Bishop")
        {
            maxHealth = 3;
        }
        curHealth = maxHealth;
        targetPos = transform.position;     
        tileSizeX = boardManager.lightTilePrefab.GetComponent<SpriteRenderer>().bounds.size.x;
        tileSizeY = boardManager.lightTilePrefab.GetComponent<SpriteRenderer>().bounds.size.y;
    }

    void Update() {
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * 10);   
    }

    public void SetPosition(int newx, int newy)
    {
        x = newx;
        y = newy;
        targetPos = new Vector3(x * tileSizeX - boardManager.offset, y * tileSizeY - boardManager.offset + boardManager.overlay, 0);
        // if(type == "Pawn" && y == 0) 
        // {
        //     type = "Queen";
        //     maxHealth = 9;
        //     curHealth = maxHealth;
        // }
    }

    public bool CanMove(int targetX, int targetY)
    {
        if(type == "Pawn")
        {
            int dx = Mathf.Abs(targetX - x);
            int dy = targetY - y; // chỉ được đi thẳng về phía trước
            if (targetX < 0 || targetX >= 8 || targetY < 0 || targetY >= 8) return false;
            if(gameManager.board[x, y - 1] != null || player.x == x && player.y == y - 1) return false; // không được đi vào ô có quân cờ khác
            if(targetX == x && targetY == 4 && dy == -2 && gameManager.board[x, y - 2] == null && !(player.x == x && player.y == y - 2)) return true; // nếu đang ở hàng xuất phát thì có thể đi hai ô
            if(dx != 0) return false; // không được đi chéo
            if(dy != -1) return false; // chỉ được đi một ô về phía trước
            return true;
        }
        else if(type == "Rook")
        {
            int dx = Mathf.Abs(targetX - x);
            int dy = Mathf.Abs(targetY - y);
            if (targetX < 0 || targetX >= 8 || targetY < 0 || targetY >= 8) return false;
            if(dx != 0 && dy != 0) return false; // phải đi thẳng
            if(dx == 0 && dy == 0) return false; // không di chuyển
            if(dx == 0) // đi dọc
            {
                int step = (targetY > y) ? 1 : -1;
                for(int i = y + step; i != targetY + step; i += step)
                {
                    if(gameManager.board[x, i] != null) return false; // không được đi qua ô có quân cờ khác
                }
            }
            else // đi ngang
            {
                int step = (targetX > x) ? 1 : -1;
                for(int i = x + step; i != targetX + step; i += step)
                {
                    if(gameManager.board[i, y] != null) return false; // không được đi qua ô có quân cờ khác
                }
            }
            return true;
        }
        else if(type == "Knight")
        {
            int dx = Mathf.Abs(targetX - x);
            int dy = Mathf.Abs(targetY - y);
            if (targetX < 0 || targetX >= 8 || targetY < 0 || targetY >= 8) return false;
            if((dx == 2 && dy == 1) || (dx == 1 && dy == 2))
            {
                if(gameManager.board[targetX, targetY] != null) return false; // không được đi vào ô có quân cờ khác
                return true; 
            }
            return false;
        }
        else if(type == "Bishop")
        {
            int dx = Mathf.Abs(targetX - x);
            int dy = Mathf.Abs(targetY - y);
            if (targetX < 0 || targetX >= 8 || targetY < 0 || targetY >= 8) return false;
            if(dx != dy) return false; // phải đi chéo
            if(dx == 0) return false; // không di chuyển
            int stepX = (targetX > x) ? 1 : -1;
            int stepY = (targetY > y) ? 1 : -1;
            
            int i = x + stepX;
            int j = y + stepY;

            while(i != targetX + stepX && j != targetY + stepY)
            {
                if(gameManager.board[i, j] != null) return false; // không được đi qua ô có quân cờ khác
                i += stepX;
                j += stepY;
            }

            return true;
        }
        else if(type == "Queen")
        {
            int dx = Mathf.Abs(targetX - x);
            int dy = Mathf.Abs(targetY - y);
            if (targetX < 0 || targetX >= 8 || targetY < 0 || targetY >= 8) return false;
            if(dx != 0 && dy != 0 && dx != dy) return false; // phải đi thẳng hoặc chéo
            if(dx == 0 && dy == 0) return false; // không di chuyển
            if(dx == 0) // đi dọc
            {
                int step = (targetY > y) ? 1 : -1;
                for(int i = y + step; i != targetY + step; i += step)
                {
                    if(gameManager.board[x, i] != null) return false; // không được đi qua ô có quân cờ khác
                }
            }
            else if(dy == 0) // đi ngang
            {
                int step = (targetX > x) ? 1 : -1;
                for(int i = x + step; i != targetX + step; i += step)
                {
                    if(gameManager.board[i, y] != null) return false; // không được đi qua ô có quân cờ khác
                }
            }
            else // đi chéo
            {
                int stepX = (targetX > x) ? 1 : -1;
                int stepY = (targetY > y) ? 1 : -1;
                
                int i = x + stepX;
                int j = y + stepY;

                while(i != targetX + stepX && j != targetY + stepY)
                {
                    if(gameManager.board[i, j] != null) return false; // không được đi qua ô có quân cờ khác
                    i += stepX;
                    j += stepY;
                }
            }
            return true;
        }
        else if(type == "King")
        {
            int dx = Mathf.Abs(targetX - x);
            int dy = Mathf.Abs(targetY - y);
            if (targetX < 0 || targetX >= 8 || targetY < 0 || targetY >= 8) return false;
            if(dx <= 1 && dy <= 1) return true; // đi như vua
            if(gameManager.board[targetX, targetY] != null) return false; // không được đi vào ô có quân cờ khác
            return false;
        }
        else return false; // loại quân cờ không hợp lệ
    }

    public void TakeDamage(int val)
    {
        curHealth -= val;
        if(curHealth <= 0)
        {
            Destroy(gameObject);
            gameManager.board[x, y] = null; // Xóa khỏi board
            gameManager.enemies.Remove(this); // Xóa khỏi danh sách kẻ thù
        }
    }
}
