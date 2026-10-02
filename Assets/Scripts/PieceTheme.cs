using UnityEngine;

// Cette ligne permet de créer ce fichier depuis le menu clic droit de Unity
[CreateAssetMenu(fileName = "NewPieceTheme", menuName = "Chess/Piece Theme")]
public class PieceTheme : ScriptableObject
{
    [Header("White Pieces")]
    public Sprite whitePawn, whiteKnight, whiteBishop, whiteRook, whiteQueen, whiteKing;

    [Header("Black Pieces")]
    public Sprite blackPawn, blackKnight, blackBishop, blackRook, blackQueen, blackKing;

    // Cette fonction renvoie le bon sprite selon la logique pure
    public Sprite GetSprite(Piece piece)
    {
        if (piece.Color == PieceColor.White)
        {
            return piece.Type switch
            {
                PieceType.Pawn => whitePawn,
                PieceType.Knight => whiteKnight,
                PieceType.Bishop => whiteBishop,
                PieceType.Rook => whiteRook,
                PieceType.Queen => whiteQueen,
                PieceType.King => whiteKing,
                _ => null
            };
        }
        else if (piece.Color == PieceColor.Black)
        {
            return piece.Type switch
            {
                PieceType.Pawn => blackPawn,
                PieceType.Knight => blackKnight,
                PieceType.Bishop => blackBishop,
                PieceType.Rook => blackRook,
                PieceType.Queen => blackQueen,
                PieceType.King => blackKing,
                _ => null
            };
        }
        return null;
    }
}