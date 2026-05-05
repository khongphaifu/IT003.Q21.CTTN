using UnityEngine;

// [ExecuteAlways] // dùng để hiện bàn cờ ngay trong Editor mà không cần chạy game
public class BoardManager : MonoBehaviour
{
    public GameObject lightTilePrefab; // truyền vào ô đen của bàn cờ
    public GameObject darkTilePrefab; // truyền vào ô trắng của bàn cờ
    private int boardSize = 8; // kích thước của bàn cờ (8x8)
    // private float tileSize = 1f; // kích thước của mỗi ô
    public float offset = 3.5f; // để căn giữa bàn cờ   
    public float overlay = 0.3f; 
    // private Tile tileScript; // tham chiếu đến script Tile để gán tọa độ
    
    private void Start() {
        // tileScript = FindAnyObjectByType<Tile>();
        // GenBoard();
    }

    public void GenBoard() { // tạo bàn cờ
        // Xóa board cũ để không bị tạo chồng
        // for (int i = transform.childCount - 1; i >= 0; i--)
        // {
        //     DestroyImmediate(transform.GetChild(i).gameObject);
        // }

        for (int y = 0; y < boardSize; y++)
        {
            for (int x = 0; x < boardSize; x++)
            {
                float tileSizeX = lightTilePrefab.GetComponent<SpriteRenderer>().bounds.size.x;
                float tileSizeY = lightTilePrefab.GetComponent<SpriteRenderer>().bounds.size.y;
                GameObject prefab = ((x + y) % 2 == 0) ? lightTilePrefab : darkTilePrefab;

                Vector3 position = new Vector3(
                    x * tileSizeX - offset,
                    y * tileSizeY - offset,
                    0f
                );

                GameObject tile = Instantiate(prefab, position, Quaternion.identity, transform);
                Tile tileScript = tile.GetComponent<Tile>();
                tile.name = $"Tile_{x}_{y}";
                // GÁN TỌA ĐỘ Ở ĐÂY
                tileScript.posx = x * tileSizeX - offset;
                tileScript.posy = y * tileSizeY - offset + overlay;
                tileScript.x = x;
                tileScript.y = y;
            }
        }

    }

    // void OnValidate()
    // {
    //     if (!Application.isPlaying)
    //     {
    //         GenBoard();
    //     }
    // }
}
