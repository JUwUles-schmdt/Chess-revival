// Move.cs
public struct Move 
{
    public int StartX;
    public int StartY;
    public int TargetX;
    public int TargetY;

    // On mémorise quelle pièce a bougé et si une pièce a été mangée.
    // C'est indispensable pour pouvoir "annuler" un coup plus tard.
    public Piece PieceMoved;
    public Piece PieceCaptured;

    public Move(int startX, int startY, int targetX, int targetY, Piece pieceMoved, Piece pieceCaptured)
    {
        StartX = startX;
        StartY = startY;
        TargetX = targetX;
        TargetY = targetY;
        PieceMoved = pieceMoved;
        PieceCaptured = pieceCaptured;
    }
}