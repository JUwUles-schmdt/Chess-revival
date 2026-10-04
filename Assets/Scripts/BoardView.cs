using UnityEngine;
using UnityEngine.InputSystem;
using Fusion; 

public class BoardView : NetworkBehaviour
{
    public PieceTheme theme; 
    public Vector2 boardOffset = new Vector2(-3.5f, -1.5f);
    public Vector2 tileSize = new Vector2(1f, 0.75f);
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
            // La sécurité réseau est testée uniquement lors du clic
            if (!Object || !Object.IsValid)
            {
                Debug.LogWarning("Clic ignoré : En attente de connexion Photon ou composant 'Network Object' manquant sur l'objet.");
                return;
            }

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

                RPC_SendMove(selectedSquare.x, selectedSquare.y, clickedSquare.x, clickedSquare.y);
                selectedSquare = new Vector2Int(-1, -1);
            }
        }
    }

    [Rpc(RpcSources.All, RpcTargets.All)]
    public void RPC_SendMove(int startX, int startY, int targetX, int targetY)
    {
        Piece pieceToMove = logicBoard.Grid[startX, startY];
        Piece pieceCaptured = logicBoard.Grid[targetX, targetY];
        
        Move move = new Move(startX, startY, targetX, targetY, pieceToMove, pieceCaptured);
        
        logicBoard.ExecuteMove(move);
        RefreshVisuals();
    }

    public Vector2Int GetGridPositionFromMouse()
    {
        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        
        float adjustedY = mouseWorldPos.y + clickOffset.y;
        float adjustedX = mouseWorldPos.x + clickOffset.x;

        int x = Mathf.FloorToInt((adjustedX - boardOffset.x) / tileSize.x + 0.5f);
        int y = Mathf.FloorToInt((adjustedY - boardOffset.y) / tileSize.y + 1);

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