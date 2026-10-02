// Piece.cs
public enum PieceType 
{ 
    None, 
    Pawn, 
    Knight, 
    Bishop, 
    Rook, 
    Queen, 
    King 
}

public enum PieceColor 
{ 
    None, 
    White, 
    Black 
}

public struct Piece 
{
    public PieceType Type;
    public PieceColor Color;
    
    // Une petite propriété pratique pour savoir si la case est vide
    public bool IsEmpty => Type == PieceType.None;

    // Un constructeur pour créer facilement une pièce
    public Piece(PieceType type, PieceColor color)
    {
        Type = type;
        Color = color;
    }
}