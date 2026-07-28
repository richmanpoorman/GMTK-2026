using Godot;
using System;

[Tool]
public partial class SpriteSizer : Node
{

    [Export]
    public Sprite2D sprite
    {
        get => _sprite; 
        set
        {
            if (_sprite is not null && _sprite.Texture is not null) _sprite.Texture.Changed -= updateSpriteSize; 
            _sprite = value; 
            if (_sprite is not null && _sprite.Texture is not null) _sprite.Texture.Changed += updateSpriteSize; 
            updateSpriteSize(); 
        }
    }

    [Export(PropertyHint.Link)]
    public Vector2 size
    {
        get => _spriteDimensions; 
        set
        {
            _spriteDimensions = value; 
            updateSpriteSize();
        }
    }

    private Vector2 _spriteDimensions = Vector2.Zero; 
    private Sprite2D _sprite; 

    private void updateSpriteSize()
    {
        if (sprite is null || sprite.Texture is null || _spriteDimensions.IsZeroApprox()) return; 

        Vector2 textureSize = sprite.Texture.GetSize(); 
        if (textureSize.X == 0 || textureSize.Y == 0) return; // Only work if the scale is a valid size

        Vector2 scaleFactor = new Vector2(_spriteDimensions.X / textureSize.X, _spriteDimensions.Y / textureSize.Y); 
        sprite.Scale = scaleFactor; 

    }

    public override void _ExitTree()
    {
        if (_sprite is not null && _sprite.Texture is not null) _sprite.Texture.Changed -= updateSpriteSize; 
    }

}
