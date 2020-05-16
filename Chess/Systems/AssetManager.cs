using Chess.GameLogic.Pieces;
using Chess.GameLogic;
using SFML.Graphics;
using SFML.Audio;
using System.Collections.Generic;
using System;

namespace Chess.Systems
{
    class AssetManager
    {
        public Dictionary<TextureID, Texture> textures;
        public Dictionary<FontID, Font> fonts;
        public Dictionary<SoundID, SoundBuffer> soundBuffers;

        public AssetManager()
        {
            textures = new Dictionary<TextureID, Texture>();
            fonts = new Dictionary<FontID, Font>();
            soundBuffers = new Dictionary<SoundID, SoundBuffer>();

            // Load Textures
            textures[TextureID.MenuBackground] = new Texture("Assets/Images/Main_Menu_Background.jpeg");
            textures[TextureID.BoardBackground] = new Texture("Assets/Images/Board_Background.jpeg");
            textures[TextureID.WhiteKing] = new Texture("Assets/Images/White_King.png");
            textures[TextureID.WhiteQueen] = new Texture("Assets/Images/White_Queen.png");
            textures[TextureID.WhiteRook] = new Texture("Assets/Images/White_Rook.png");
            textures[TextureID.WhiteKnight] = new Texture("Assets/Images/White_Knight.png");
            textures[TextureID.WhiteBishop] = new Texture("Assets/Images/White_Bishop.png");
            textures[TextureID.WhitePawn] = new Texture("Assets/Images/White_Pawn.png");
            textures[TextureID.BlackKing] = new Texture("Assets/Images/Black_King.png");
            textures[TextureID.BlackQueen] = new Texture("Assets/Images/Black_Queen.png");
            textures[TextureID.BlackRook] = new Texture("Assets/Images/Black_Rook.png");
            textures[TextureID.BlackKnight] = new Texture("Assets/Images/Black_Knight.png");
            textures[TextureID.BlackBishop] = new Texture("Assets/Images/Black_Bishop.png");
            textures[TextureID.BlackPawn] = new Texture("Assets/Images/Black_Pawn.png");

            // Load Fonts
            fonts[FontID.MenuFont] = new Font("Assets/Fonts/Menu_Font.ttf");

            // Load SoundBuffers
            soundBuffers[SoundID.MenuButtonClick] = new SoundBuffer("Assets/Sounds/Button_Click.wav");
            soundBuffers[SoundID.MenuButtonClick] = new SoundBuffer("Assets/Sounds/Piece_Move.wav");
        }
        public Sprite GetPieceSprite(Piece piece)
        {
            // Empty Square
            if (piece == null)
            {
                return new Sprite();
            }
            // White Pieces
            else if (piece.color == ChessColor.White)
            {
                switch (piece.type)
                {
                    case PieceType.King:
                        return new Sprite(textures[TextureID.WhiteKing]);
                    case PieceType.Queen:
                        return new Sprite(textures[TextureID.WhiteQueen]);
                    case PieceType.Rook:
                        return new Sprite(textures[TextureID.WhiteRook]);
                    case PieceType.Knight:
                        return new Sprite(textures[TextureID.WhiteKnight]);
                    case PieceType.Bishop:
                        return new Sprite(textures[TextureID.WhiteBishop]);
                    case PieceType.Pawn:
                        return new Sprite(textures[TextureID.WhitePawn]);
                    default:
                        throw new ArgumentException("PieceType is not valid");

                }
            }
            // Black Pieces
            else
            {
                switch (piece.type)
                {
                    case PieceType.King:
                        return new Sprite(textures[TextureID.BlackKing]);
                    case PieceType.Queen:
                        return new Sprite(textures[TextureID.BlackQueen]);
                    case PieceType.Rook:
                        return new Sprite(textures[TextureID.BlackRook]);
                    case PieceType.Knight:
                        return new Sprite(textures[TextureID.BlackKnight]);
                    case PieceType.Bishop:
                        return new Sprite(textures[TextureID.BlackBishop]);
                    case PieceType.Pawn:
                        return new Sprite(textures[TextureID.BlackPawn]);
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
        BlackPawn
    }
    public enum FontID
    {
        MenuFont
    }
    public enum SoundID
    {
        MenuButtonClick,
        PieceMove
    }
}