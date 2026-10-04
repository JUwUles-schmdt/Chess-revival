using UnityEngine;
using UnityEngine.InputSystem;

public class BoardView : MonoBehaviour
{
    public PieceTheme theme; 
    public Vector2 boardOffset = new Vector2(-3.5f, -1.5f);
    public Vector2 tileSize = new Vector2(1f, 0.75f);
    
    // Nouveau : Ajustement pour compenser la hauteur des pièces lors du clic
    public Vector2 clickOffset = new Vector2(0f, 0.4f); 

    private Board logicBoard;
    public Vector2Int selectedSquare = new Vector2Int(-1, -1);

    private void Start()
    {
        logicBoard = new Board();
        logicBoard.SetupStartingPosition();
        DrawPieces();
    }

    private void Update()
    {
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2Int clickedSquare = GetGridPositionFromMouse();

            if (clickedSquare.x == -1)
            {
                selectedSquare = new Vector2Int(-1, -1);
                return;
            }

            if (selectedSquare.x == -1)
            {
                Piece clickedPiece = logicBoard.Grid[clickedSquare.x, clickedSquare.y];
                if (!clickedPiece.IsEmpty)
                {
                    selectedSquare = clickedSquare;
                    Debug.Log($"Sélection : {clickedPiece.Color} {clickedPiece.Type} en {clickedSquare}");
                }
            }
            else
            {
                if (clickedSquare == selectedSquare)
                {
                    selectedSquare = new Vector2Int(-1, -1);
                    return;
                }

                Piece pieceToMove = logicBoard.Grid[selectedSquare.x, selectedSquare.y];
                Piece pieceCaptured = logicBoard.Grid[clickedSquare.x, clickedSquare.y];
                
                Move move = new Move(selectedSquare.x, selectedSquare.y, clickedSquare.x, clickedSquare.y, pieceToMove, pieceCaptured);
                logicBoard.ExecuteMove(move);
                RefreshVisuals();
                selectedSquare = new Vector2Int(-1, -1);
            }
        }
    }

    public Vector2Int GetGridPositionFromMouse()
    {
        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        
        // On triche en ajoutant le clickOffset à la position de la souris
        float adjustedY = mouseWorldPos.y + clickOffset.y;
        float adjustedX = mouseWorldPos.x + clickOffset.x;

        int x = Mathf.FloorToInt((adjustedX - boardOffset.x) / tileSize.x +0.5f);
        int y = Mathf.FloorToInt((adjustedY - boardOffset.y) / tileSize.y);

        if (x >= 0 && x < 8 && y >= 0 && y < 8)
            return new Vector2Int(x, y);

        return new Vector2Int(-1, -1); 
    }

    private void RefreshVisuals()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        DrawPieces();
    }

    private void DrawPieces()
    {
        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                Piece pieceLogic = logicBoard.Grid[x, y];
                
                if (!pieceLogic.IsEmpty)
                {
                    GameObject pieceObject = new GameObject($"Piece_{x}_{y}");
                    
                    float posX = (x * tileSize.x) + boardOffset.x;
                    float posY = (y * tileSize.y) + boardOffset.y;
                    
                    pieceObject.transform.position = new Vector3(posX, posY, 0);
                    
                    SpriteRenderer renderer = pieceObject.AddComponent<SpriteRenderer>();
                    renderer.sprite = theme.GetSprite(pieceLogic);
                    renderer.sortingOrder = 10 - y;
                    
                    pieceObject.transform.SetParent(this.transform);
                }
            }
        }
    }
}