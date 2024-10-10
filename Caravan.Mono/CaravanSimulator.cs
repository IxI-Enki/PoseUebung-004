using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Caravan.Mono
{
  public class CaravanSimulator : Game
  {
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Texture2D horse, camel, title, exit, load, start, background;

    public CaravanSimulator()
    {
      _graphics = new GraphicsDeviceManager(this);
      Content.RootDirectory = "Content";

      _graphics.PreferredBackBufferWidth = 1400;
      _graphics.PreferredBackBufferHeight = 700;
      _graphics.ApplyChanges();

      Window.AllowUserResizing = true;

      Window.Title = "Karawanen Simulator 9000";
      IsMouseVisible = true;
      //     Window.IsBorderless = true;
    }
    protected override void Initialize()
    {
      // TODO: Add your initialization logic here

      base.Initialize();
    }

    protected override void LoadContent()
    {
      _spriteBatch = new SpriteBatch(GraphicsDevice);
      background = Content.Load<Texture2D>("textures/background");
      camel = Content.Load<Texture2D>("textures/camel");
      horse = Content.Load<Texture2D>("textures/horse");
      title = Content.Load<Texture2D>("textures/title");
      start = Content.Load<Texture2D>("textures/start");
      load = Content.Load<Texture2D>("textures/load");
      exit = Content.Load<Texture2D>("textures/exit");
    }

    protected override void Update(GameTime gameTime)
    {
      if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
        || Keyboard.GetState().IsKeyDown(Keys.Escape))
        Exit();

      base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
      GraphicsDevice.Clear(Color.CornflowerBlue);

      _spriteBatch.Begin();
      _spriteBatch.Draw(background , new Rectangle(0 , 0 , Window.ClientBounds.Width , Window.ClientBounds.Height) , Color.White);
      _spriteBatch.Draw(title , new Rectangle((Window.ClientBounds.Width / 2 - 256) , 4 , 512 , 128) , Color.White);
      _spriteBatch.Draw(start , new Rectangle((Window.ClientBounds.Width / 2 - 120) , 142 , 240 , 54) , Color.White);
      _spriteBatch.Draw(load , new Rectangle((Window.ClientBounds.Width / 2 - 120) , 216 , 240 , 54) , Color.White);
      _spriteBatch.Draw(exit , new Rectangle((Window.ClientBounds.Width / 2 - 120) , 290 , 240 , 54) , Color.White);

      PrintCaravan();

      _spriteBatch.End();

      base.Draw(gameTime);
    }

    private void PrintCaravan()
    {
      int posX = _frameCounter;
      int number = 10;

      _spriteBatch.Draw(camel , new Rectangle(_frameCounter + 5 * number , Window.ClientBounds.Height - 256 , 128 , 128) , Color.White);

      _spriteBatch.Draw(camel , new Rectangle(_frameCounter + 15 * number , Window.ClientBounds.Height - 256 , 128 , 128) , Color.White);
      _spriteBatch.Draw(camel , new Rectangle(_frameCounter + 25 * number , Window.ClientBounds.Height - 256 , 128 , 128) , Color.White);
      _spriteBatch.Draw(camel , new Rectangle(_frameCounter + 35 * number , Window.ClientBounds.Height - 256 , 128 , 128) , Color.White);
      _spriteBatch.Draw(camel , new Rectangle(_frameCounter + 45 * number , Window.ClientBounds.Height - 256 , 128 , 128) , Color.White);
      _spriteBatch.Draw(horse , new Rectangle(_frameCounter + 55 * number , Window.ClientBounds.Height - 256 , 128 , 128) , Color.White);
      _spriteBatch.Draw(camel , new Rectangle(_frameCounter + 65 * number , Window.ClientBounds.Height - 256 , 128 , 128) , Color.White);
      _spriteBatch.Draw(camel , new Rectangle(_frameCounter + 75 * number , Window.ClientBounds.Height - 256 , 128 , 128) , Color.White);


      _frameCounter--;
      if (_frameCounter < -1000)
        _frameCounter = 1500;
    }

    private int _frameCounter = 1500;
  }
}
