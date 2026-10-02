public class Board 
{
    // Notre fameuse grille 8x8. [x, y] correspond à [colonne, ligne]
    public Piece[,] Grid; 
    
    // Pour savoir à qui c'est le tour de jouer
    public PieceColor CurrentTurn; 

    public Board()
    {
        Grid = new Piece[8, 8];
        CurrentTurn = PieceColor.White; // Les blancs commencent toujours
    }

    // Cette méthode place toutes les pièces à leur position initiale
    public void SetupStartingPosition()
    {
        // 1. On vide tout le plateau par sécurité
        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                Grid[x, y] = new Piece(PieceType.None, PieceColor.None);
            }
        }

        // 2. On place les Pions sur la ligne 1 (Blancs) et 6 (Noirs)
        for (int x = 0; x < 8; x++)
        {
            Grid[x, 1] = new Piece(PieceType.Pawn, PieceColor.White);
            Grid[x, 6] = new Piece(PieceType.Pawn, PieceColor.Black);
        }

        // 3. On place les pièces majeures Blanches (Ligne 0)
        Grid[0, 0] = new Piece(PieceType.Rook, PieceColor.White);
        Grid[1, 0] = new Piece(PieceType.Knight, PieceColor.White);
        Grid[2, 0] = new Piece(PieceType.Bishop, PieceColor.White);
        Grid[3, 0] = new Piece(PieceType.Queen, PieceColor.White);
        Grid[4, 0] = new Piece(PieceType.King, PieceColor.White);
        Grid[5, 0] = new Piece(PieceType.Bishop, PieceColor.White);
        Grid[6, 0] = new Piece(PieceType.Knight, PieceColor.White);
        Grid[7, 0] = new Piece(PieceType.Rook, PieceColor.White);

        // 4. On place les pièces majeures Noires (Ligne 7)
        Grid[0, 7] = new Piece(PieceType.Rook, PieceColor.Black);
        Grid[1, 7] = new Piece(PieceType.Knight, PieceColor.Black);
        Grid[2, 7] = new Piece(PieceType.Bishop, PieceColor.Black);
        Grid[3, 7] = new Piece(PieceType.Queen, PieceColor.Black);
        Grid[4, 7] = new Piece(PieceType.King, PieceColor.Black);
        Grid[5, 7] = new Piece(PieceType.Bishop, PieceColor.Black);
        Grid[6, 7] = new Piece(PieceType.Knight, PieceColor.Black);
        Grid[7, 7] = new Piece(PieceType.Rook, PieceColor.Black);
    }

    // Cette méthode applique un mouvement sur la grille en mémoire
    public void ExecuteMove(Move move)
    {
        // On place la pièce sur la case d'arrivée
        Grid[move.TargetX, move.TargetY] = move.PieceMoved;
        
        // On vide la case de départ
        Grid[move.StartX, move.StartY] = new Piece(PieceType.None, PieceColor.None);

        // On change de tour
        CurrentTurn = CurrentTurn == PieceColor.White ? PieceColor.Black : PieceColor.White;
    }
}