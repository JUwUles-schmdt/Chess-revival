using UnityEngine;
using UnityEngine.InputSystem;

public class HoverHighlight : MonoBehaviour
{
    public BoardView boardView; 
    public Sprite outlineSprite; 
    
    public Vector2 visualOffset = new Vector2(0f, -0.03125f);
    
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = outlineSprite;
        spriteRenderer.sortingOrder = 100; 
    }

    private void Update()
    {
        // Si aucune souris ou aucune pièce sélectionnée, on cache le contour
        if (Mouse.current == null || boardView.selectedSquare.x == -1)
        {
            spriteRenderer.enabled = false;
            return;
        }

        // On interroge directement le BoardView pour avoir la case exacte !
        Vector2Int hoverSquare = boardView.GetGridPositionFromMouse();

        // Si la case survolée est bien sur le plateau
        if (hoverSquare.x != -1)
        {
            
            spriteRenderer.enabled = true; 
            
            // On calcule la position visuelle d'affichage
            float posX = (hoverSquare.x * boardView.tileSize.x) + boardView.boardOffset.x + visualOffset.x;
            float posY = (hoverSquare.y * boardView.tileSize.y) + boardView.boardOffset.y + visualOffset.y;
            
            transform.position = new Vector3(posX, posY, 0);
        }
        else
        {
            spriteRenderer.enabled = false;
        }
    }
}