using Microsoft.Xna.Framework;

namespace MinuteBattle.Graphics
{
    public interface IResolution
    {
        Vector2 ScreenToGameCoord(Vector2 screenCoord);
    }
}
