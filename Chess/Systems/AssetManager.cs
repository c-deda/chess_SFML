using Chess.GameLogic;
using SFML.Graphics;
using SFML.Audio;
using System.Collections.Generic;
using System;

namespace Chess.Systems
{
    class AssetManager
    {
        public Dictionary<TextureID, Texture> Textures { get; private set; }
        public Dictionary<FontID, Font> Fonts { get; private set; }
        public Dictionary<SoundID, SoundBuffer> SoundBuffers { get; private set; }

        public AssetManager()
        {
            Textures = new Dictionary<TextureID, Texture>();
            Fonts = new Dictionary<FontID, Font>();
            SoundBuffers = new Dictionary<SoundID, SoundBuffer>();

            // Load Textures
            Textures[TextureID.MenuBackground] = new Texture("Assets/Images/Main_Menu_Background.jpeg");
            Textures[TextureID.BoardBackground] = new Texture("Assets/Images/Board_Background.jpeg");
            Textures[TextureID.WhiteKing] = new Texture("Assets/Images/White_King.png");
            Textures[TextureID.WhiteQueen] = new Texture("Assets/Images/White_Queen.png");
            Textures[TextureID.WhiteRook] = new Texture("Assets/Images/White_Rook.png");
            Textures[TextureID.WhiteKnight] = new Texture("Assets/Images/White_Knight.png");
            Textures[TextureID.WhiteBishop] = new Texture("Assets/Images/White_Bishop.png");
            Textures[TextureID.WhitePawn] = new Texture("Assets/Images/White_Pawn.png");
            Textures[TextureID.BlackKing] = new Texture("Assets/Images/Black_King.png");
            Textures[TextureID.BlackQueen] = new Texture("Assets/Images/Black_Queen.png");
            Textures[TextureID.BlackRook] = new Texture("Assets/Images/Black_Rook.png");
            Textures[TextureID.BlackKnight] = new Texture("Assets/Images/Black_Knight.png");
            Textures[TextureID.BlackBishop] = new Texture("Assets/Images/Black_Bishop.png");
            Textures[TextureID.BlackPawn] = new Texture("Assets/Images/Black_Pawn.png");
            Textures[TextureID.NewGameIdle] = new Texture("Assets/Images/New_Game_Idle.png");
            Textures[TextureID.NewGameHover] = new Texture("Assets/Images/New_Game_Hover.png");
            Textures[TextureID.ReturnIdle] = new Texture("Assets/Images/Return_Idle.png");
            Textures[TextureID.ReturnHover] = new Texture("Assets/Images/Return_Hover.png");

            // Load Fonts
            Fonts[FontID.MenuFont] = new Font("Assets/Fonts/Menu_Font.ttf");
            Fonts[FontID.MoveHistoryFont] = new Font("Assets/Fonts/Move_History_Font.ttf");

            // Load SoundBuffers
            SoundBuffers[SoundID.MenuButtonClick] = new SoundBuffer("Assets/Sounds/Button_Click.wav");
            SoundBuffers[SoundID.MenuButtonClick] = new SoundBuffer("Assets/Sounds/Piece_Move.wav");
        }
        public Sprite GetPieceSprite(Piece piece)
        {
            // Empty Square
            if (piece == null)
            {
                return new Sprite();
            }
            // White Pieces
            else if (piece.Color == ChessColor.White)
            {
                switch (piece.Type)
                {
                    case PieceType.King:
                        return new Sprite(Textures[TextureID.WhiteKing]);
                    case PieceType.Queen:
                        return new Sprite(Textures[TextureID.WhiteQueen]);
                    case PieceType.Rook:
                        return new Sprite(Textures[TextureID.WhiteRook]);
                    case PieceType.Knight:
                        return new Sprite(Textures[TextureID.WhiteKnight]);
                    case PieceType.Bishop:
                        return new Sprite(Textures[TextureID.WhiteBishop]);
                    case PieceType.Pawn:
                        return new Sprite(Textures[TextureID.WhitePawn]);
                    default:
                        throw new ArgumentException("PieceType is not valid");

                }
            }
            // Black Pieces
            else
            {
                switch (piece.Type)
                {
                    case PieceType.King:
                        return new Sprite(Textures[TextureID.BlackKing]);
                    case PieceType.Queen:
                        return new Sprite(Textures[TextureID.BlackQueen]);
                    case PieceType.Rook:
                        return new Sprite(Textures[TextureID.BlackRook]);
                    case PieceType.Knight:
                        return new Sprite(Textures[TextureID.BlackKnight]);
                    case PieceType.Bishop:
                        return new Sprite(Textures[TextureID.BlackBishop]);
                    case PieceType.Pawn:
                        return new Sprite(Textures[TextureID.BlackPawn]);
                    default:
                        throw new ArgumentException("PieceType is not valid");

                }
            }
        }
    }

    public enum TextureID
    {
        MenuBackground,
        BoardBackground,
        WhiteKing,
        WhiteQueen,
        WhiteRook,
        WhiteKnight,
        WhiteBishop,
        WhitePawn,
        BlackKing,
        BlackQueen,
        BlackRook,
        BlackKnight,
        BlackBishop,
        BlackPawn,
        NewGameIdle,
        NewGameHover,
        ReturnIdle,
        ReturnHover
    }
    public enum FontID
    {
        MenuFont,
        MoveHistoryFont
    }
    public enum SoundID
    {
        MenuButtonClick,
        PieceMove
    }
}