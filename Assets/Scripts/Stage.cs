using UnityEngine;

public class Stage : MonoBehaviour
{
    [Header("Editor Objects")]
    public GameObject tilePrefab;
    public Transform gameBoard;
    public Transform setBlock;
    public Transform block;

    [Header("Game Settings")]
    [Range(4, 40)]
    public int boardWidth = 10;
    [Range(5, 20)]
    public int boardHeight = 20;
    public float fallCycle = 1.0f;

    private int _halfWidth;
    private int _halfHeight;

    private float _nextFallTime;

    private void Start()
    {
        _halfWidth = Mathf.RoundToInt(boardWidth * 0.5f);
        _halfHeight = Mathf.RoundToInt(boardHeight * 0.5f);

        _nextFallTime = Time.time + fallCycle;
        
        CreateBackGround();
        CreateBlock();
    }

    void Update()
    {
        Vector3 moveDir = Vector3.zero;
        bool isRotate = false;

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            moveDir.x = -1;

        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            moveDir.x = 1;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            isRotate = true;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            moveDir.y = -1;
        }

        if (moveDir != Vector3.zero || isRotate)
        {
            MoveTetromino(moveDir, isRotate);
        }
        
        if (Time.time > _nextFallTime)
        {
            _nextFallTime = Time.time + fallCycle;
            moveDir = Vector3.down;
            isRotate = false;
        }

        if (moveDir != Vector3.zero || isRotate)
        {
            MoveTetromino(moveDir, isRotate);
        }
    }
    
    bool MoveTetromino(Vector3 moveDir, bool isRotate)
    {
        block.transform.position += moveDir;
        if (isRotate)
        {
            block.transform.rotation *= Quaternion.Euler(0, 0, 90);
        }

        return true;
    }
    
    // 타일 생성
    Tile CreateTile(Transform parent, Vector2 position, Color color, int order = 1)
    {
        var go = Instantiate(tilePrefab);
        go.transform.parent = parent;
        go.transform.localPosition = position;

        var tile = go.GetComponent<Tile>();
        tile.color = color;
        tile.sortingOrder = order;

        return tile;
    }

    void CreateBackGround()
    {
        Color color = Color.gray;

        // 타일 보드
        color.a = 0.5f;
        for (int x = -_halfWidth; x < _halfWidth; ++x)
        {
            for (int y = _halfHeight; y > -_halfHeight; --y)
            {
                CreateTile(gameBoard, new Vector2(x, y), color, 0);
            }
        }

        // 좌우 테두리
        color.a = 1.0f;
        for (int y = _halfHeight; y > -_halfHeight; --y)
        {
            CreateTile(gameBoard, new Vector2(-_halfWidth - 1, y), color, 0);
            CreateTile(gameBoard, new Vector2(_halfWidth, y), color, 0);
        }

        // 아래 테두리
        for (int x = -_halfWidth - 1; x <= _halfWidth; ++x)
        {
            CreateTile(gameBoard, new Vector2(x, -_halfHeight), color, 0);
        }
    }

    void CreateBlock()
    {
        int index = Random.Range(0, 7);
        Color32 color = Color.white;

        block.rotation = Quaternion.identity;
        block.position = new Vector2(0, _halfHeight);

        switch (index)
        {
            // I : 하늘색
            case 0:
                color = new Color32(115, 251, 253, 255);
                CreateTile(block, new Vector2(1f, 0.0f), color);
                CreateTile(block, new Vector2(-2f, 0.0f), color);
                CreateTile(block, new Vector2(0f, 0.0f), color);
                CreateTile(block, new Vector2(-1f, 0.0f), color);
                break;

            // J : 파란색
            case 1:
                color = new Color32(0, 33, 245, 255);
                CreateTile(block, new Vector2(-1f, 0.0f), color);
                CreateTile(block, new Vector2(0f, 0.0f), color);
                CreateTile(block, new Vector2(1f, 0.0f), color);
                CreateTile(block, new Vector2(-1f, 1.0f), color);
                break;

            // L : 귤색
            case 2:
                color = new Color32(243, 168, 59, 255);
                CreateTile(block, new Vector2(-1f, 0.0f), color);
                CreateTile(block, new Vector2(0f, 0.0f), color);
                CreateTile(block, new Vector2(1f, 0.0f), color);
                CreateTile(block, new Vector2(1f, 1.0f), color);
                break;

            // O : 노란색
            case 3:
                color = new Color32(255, 253, 84, 255);
                CreateTile(block, new Vector2(0f, 0f), color);
                CreateTile(block, new Vector2(1f, 0f), color);
                CreateTile(block, new Vector2(0f, 1f), color);
                CreateTile(block, new Vector2(1f, 1f), color);
                break;

            // S : 녹색
            case 4:
                color = new Color32(117, 250, 76, 255);
                CreateTile(block, new Vector2(-1f, -1f), color);
                CreateTile(block, new Vector2(0f, -1f), color);
                CreateTile(block, new Vector2(0f, 0f), color);
                CreateTile(block, new Vector2(1f, 0f), color);
                break;

            // T : 자주색
            case 5:
                color = new Color32(155, 47, 246, 255);
                CreateTile(block, new Vector2(-1f, 0f), color);
                CreateTile(block, new Vector2(0f, 0f), color);
                CreateTile(block, new Vector2(1f, 0f), color);
                CreateTile(block, new Vector2(0f, 1f), color);
                break;

            // Z : 빨간색
            case 6:
                color = new Color32(235, 51, 35, 255);
                CreateTile(block, new Vector2(-1f, 1f), color);
                CreateTile(block, new Vector2(0f, 1f), color);
                CreateTile(block, new Vector2(0f, 0f), color);
                CreateTile(block, new Vector2(1f, 0f), color);
                break;
        }
    }
}