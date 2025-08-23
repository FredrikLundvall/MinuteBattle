using Microsoft.Xna.Framework;

namespace MinuteBattle.Graphics
{
    public interface IClip
    {
        void Draw(Vector2 parentPosition, float parentRotation, GameTime gameTime);
        ClipCategoryEnum GetCategory();
        bool SetText(string text);
        bool SetRotation(float rotation);
        bool SetOrigin(Vector2 origin);
        bool SetColor(Color color);
        Rectangle GetBoundingRectangle(Vector2 parentPosition, float parentRotation);
        Vector2 getSize();
    }
}
