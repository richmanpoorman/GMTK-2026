using Godot;
using System;

[Tool]
public partial class ShapeDrawer : Node2D
{

    [Export(PropertyHint.Range, "0,1")]
    public float TRANSPARENCY
    {
        get => _transparency; 
        set => setTransparency(value);
    }

    [Export] 
	private bool IS_DEBUG = false; 
	[Export] 
	private Color DEBUG_SHAPE_COLOR
    {
        get => _color; 
        set => _color = new Color(value, TRANSPARENCY);
    }

    public Shape2D shape
    {
        get => _shape; 
        set => setShape(value);
    }

    private float _transparency = 0.5f;
    private Shape2D _shape; 
    private Color _color; 

    private void onShapeUpdate() => QueueRedraw(); 

    private void setTransparency(float value)
    {
        _transparency = value; 
        _color = new Color(_color, value); 
        QueueRedraw(); 
    }

    private void setShape(Shape2D shape)
    {
        if (_shape is not null) _shape.Changed -= onShapeUpdate; 
        _shape = shape; 
        if (_shape is not null) _shape.Changed += onShapeUpdate; 
        QueueRedraw(); 
    }

    public void drawCollider()
	{
		if (!IS_DEBUG && !Engine.IsEditorHint()) return; 
		shape.Draw(GetCanvasItem(), _color);
	}


    public override void _Draw()
    {
        drawCollider();
    }

    public override void _ExitTree()
    {
        if (_shape is not null) _shape.Changed -= onShapeUpdate; 
    }

}
