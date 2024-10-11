using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Caravan.Mono
{
  internal static class Globals
  {
    public static Texture2D horse, camel;

    private static SpriteBatch _spriteBatch;
    public static SpriteBatch SpriteBatch { get => _spriteBatch; set => _spriteBatch = value; }

    public static void Init(CaravanSimulator caravanSimulator)
    {
      camel = caravanSimulator.Content.Load<Texture2D>("textures/camel");
      horse = caravanSimulator.Content.Load<Texture2D>("textures/horse");
    }
  }
}
