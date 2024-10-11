using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using Gum;
using RenderingLibrary;
using MonoGameGum.GueDeriving;

namespace Caravan.Mono
{
  public class CaravanSimulator : Game
  {
    private GraphicsDeviceManager _graphics;
    private Texture2D horse, camel, title, exit, load, start, background;

    private SpriteBatch _spriteBatch;
    public SpriteBatch SpriteBatch { get => _spriteBatch; set => _spriteBatch = value; }

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

      Globals.Init(this);
      Globals.SpriteBatch = new SpriteBatch(GraphicsDevice);
    }
    protected override void Initialize()
    {
      SystemManagers.Default = new SystemManagers();
      SystemManagers.Default.Initialize(_graphics.GraphicsDevice , fullInstantiation: true);

      _caravanManager = new();

      _caravanManager.Initialize();

      // TESTING GUM IMPLEMENTATION ✅
      /*
           var rectangle = new ColoredRectangleRuntime();
           rectangle.Width = 100;
           rectangle.Height = 100;
           rectangle.Color = Color.White;
           rectangle.AddToManagers(SystemManagers.Default , null);
           */
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
      SystemManagers.Default.Activity(gameTime.TotalGameTime.TotalSeconds);

      base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
      GraphicsDevice.Clear(Color.CornflowerBlue);

      _spriteBatch.Begin(samplerState: SamplerState.PointClamp);

      int thirdWidth = Window.ClientBounds.Width / 3;
      int fifthHeight = Window.ClientBounds.Height / 5;

      _spriteBatch.Draw(background , new Rectangle(0 , 0 , Window.ClientBounds.Width , Window.ClientBounds.Height) , Color.White);
      _spriteBatch.Draw(title , new Rectangle((Window.ClientBounds.Width / 2 - thirdWidth / 2) , 4 , thirdWidth , fifthHeight) , Color.White);
      _spriteBatch.Draw(start , new Rectangle((Window.ClientBounds.Width / 2 - thirdWidth / 2 + 50) , fifthHeight + 20 , thirdWidth - 100 , fifthHeight / 2) , Color.White);
      _spriteBatch.Draw(load , new Rectangle((Window.ClientBounds.Width / 2 - thirdWidth / 2 + 50) , fifthHeight + fifthHeight / 2 + 25 , thirdWidth - 100 , fifthHeight / 2) , Color.White);
      _spriteBatch.Draw(exit , new Rectangle((Window.ClientBounds.Width / 2 - thirdWidth / 2 + 50) , 2 * fifthHeight + 30 , thirdWidth - 100 , fifthHeight / 2) , Color.White);

      //PrintCaravan();

      _spriteBatch.End();

      _caravanManager.Draw();

      SystemManagers.Default.Draw();

      base.Draw(gameTime);
    }

    private void PrintCaravan()
    {
      int posX = _frameCounter;
      int number = 10;
      int fifthHeight = Window.ClientBounds.Height / 5;

      _spriteBatch.Draw(camel , new Rectangle(_frameCounter + fifthHeight + number , Window.ClientBounds.Height - 2 * fifthHeight , fifthHeight , fifthHeight) , Color.White);

      _spriteBatch.Draw(camel , new Rectangle(_frameCounter + 2 * fifthHeight + number , Window.ClientBounds.Height - 2 * fifthHeight , fifthHeight , fifthHeight) , Color.White);
      _spriteBatch.Draw(camel , new Rectangle(_frameCounter + 3 * fifthHeight + number , Window.ClientBounds.Height - 2 * fifthHeight , fifthHeight , fifthHeight) , Color.White);
      _spriteBatch.Draw(camel , new Rectangle(_frameCounter + 4 * fifthHeight + number , Window.ClientBounds.Height - 2 * fifthHeight , fifthHeight , fifthHeight) , Color.White);
      _spriteBatch.Draw(camel , new Rectangle(_frameCounter + 5 * fifthHeight + number , Window.ClientBounds.Height - 2 * fifthHeight , fifthHeight , fifthHeight) , Color.White);
      _spriteBatch.Draw(horse , new Rectangle(_frameCounter + 6 * fifthHeight + number , Window.ClientBounds.Height - 2 * fifthHeight , fifthHeight , fifthHeight) , Color.White);
      _spriteBatch.Draw(camel , new Rectangle(_frameCounter + 7 * fifthHeight + number , Window.ClientBounds.Height - 2 * fifthHeight , fifthHeight , fifthHeight) , Color.White);
      _spriteBatch.Draw(camel , new Rectangle(_frameCounter + 8 * fifthHeight + number , Window.ClientBounds.Height - 2 * fifthHeight , fifthHeight , fifthHeight) , Color.White);


      _frameCounter--;
      if (_frameCounter < -1000)
        _frameCounter = 1500;
    }

    private CaravanManager _caravanManager;
    private int _frameCounter = 1500;
  }
}
