using Microsoft.Xna.Framework;

namespace MinuteBattle.Graphics
{
    public interface IClip
    {
        void Draw(Vector2 parentPosition, float parentRotation, GameTime gameTime);
        ClipCategoryEnum GetCategory();
        bool SetText(string text);
        bool SetRotation(float rotation);
    }
}
