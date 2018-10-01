using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Snake
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        KeyboardState _currentKeyboardState;
        KeyboardState _previousKeyboardState;
        Texture2D bg, head, node, rattle, donoteat, eat;
        List<Node> nodes;
        List<Eat> eats;
        List<DoNotEat> donoteats;
        bool moved = false;
        string direction = "left";
        float timeElapsed = 0.0f;
        float speed = 0.00f;
        int score = 0;

        public class Node
        {
            public int row, column;
            public string direction = "left";
        }

        public class Eat
        {
            public int row, column;
        }

        public class DoNotEat
        {
            public int row, column;
        }

        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = 500;
            graphics.PreferredBackBufferWidth = 1000;

            
            nodes = new List<Node>();
            eats = new List<Eat>();
            donoteats = new List<DoNotEat>();


            for (int i = 0; i < 8; i++)
            {
                Node nod = new Node();
                nod.row = 450;
                nod.column = 450 + ((i + 1) * 15);
                nodes.Add(nod);
            }


            this.Window.Title = "Score: " + this.score;

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        System.Random r = new System.Random();
        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            bg = Content.Load<Texture2D>("bg");
            head = Content.Load<Texture2D>("head");
            node = Content.Load<Texture2D>("node");
            rattle = Content.Load<Texture2D>("rattle");
            eat = Content.Load<Texture2D>("eat");
            donoteat = Content.Load<Texture2D>("donoteat");

            loadLevel();

        }

        private void loadLevel()
        {
            for (int i = 0; i < 20; i++)
            {
                DoNotEat e = new DoNotEat();
                int v = r.Next(30) + 1;
                int w = r.Next(60) + 1;
                e.row = v * 15;
                e.column = w * 15;
                donoteats.Add(e);
            }
            for (int i = 0; i < 80; i++)
            {
                Eat e = new Eat();
                int v = r.Next(30) + 1;
                int w = r.Next(60) + 1;
                e.row = v * 15;
                e.column = w * 15;
                eats.Add(e);
            }
            for (int i = 0; i < donoteats.Count; i++)
            {
                for (int j = 0; j < eats.Count; j++)
                {
                    if (donoteats[i].row == eats[j].row && donoteats[i].column == eats[j].column)
                    {
                        donoteats.Remove(donoteats[i]);
                    }
                }
            }

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Before handling input
            _currentKeyboardState = Keyboard.GetState();

            // TODO: Add your update logic here
            if (_currentKeyboardState.IsKeyDown(Keys.Left) && _previousKeyboardState.IsKeyUp(Keys.Left))
            {
                moved = true;
                if(!direction.Equals("right"))
                    direction = "left";
                nodes[0].direction = direction;
                for (int i = 0; i < nodes.Count; i++)
                {
                    if (nodes[0].direction.Equals("right") && nodes[1].direction.Equals("left"))
                    {
                        nodes[0].direction = "left";
                    }
                    if (nodes[0].direction.Equals("left") && nodes[1].direction.Equals("right"))
                    {
                        nodes[0].direction = "right";
                    }
                    if (nodes[i].direction.Equals("left"))
                        nodes[i].column -= 15;
                    if (nodes[i].direction.Equals("right"))
                        nodes[i].column += 15;
                    if (nodes[i].direction.Equals("up"))
                        nodes[i].row -= 15;
                    if (nodes[i].direction.Equals("down"))
                        nodes[i].row += 15;
                }
                for (int h = nodes.Count - 1; h > 0; --h)
                {
                    nodes[h].direction = nodes[h - 1].direction;
                }
            }
            if (_currentKeyboardState.IsKeyDown(Keys.Right) && _previousKeyboardState.IsKeyUp(Keys.Right))
            {
                moved = true;
                if (!direction.Equals("left"))
                    direction = "right";
                nodes[0].direction = direction;
                for (int i = 0; i < nodes.Count; i++)
                {
                    if (nodes[0].direction.Equals("right") && nodes[1].direction.Equals("left"))
                    {
                        nodes[0].direction = "left";
                    }
                    if (nodes[0].direction.Equals("left") && nodes[1].direction.Equals("right"))
                    {
                        nodes[0].direction = "right";
                    }
                    if (nodes[i].direction.Equals("left"))
                        nodes[i].column -= 15;
                    if (nodes[i].direction.Equals("right"))
                        nodes[i].column += 15;
                    if (nodes[i].direction.Equals("up"))
                        nodes[i].row -= 15;
                    if (nodes[i].direction.Equals("down"))
                        nodes[i].row += 15;
                }
                for (int h = nodes.Count - 1; h > 0; --h)
                {
                    nodes[h].direction = nodes[h - 1].direction;
                }
            }
            if (_currentKeyboardState.IsKeyDown(Keys.Up) && _previousKeyboardState.IsKeyUp(Keys.Up))
            {
                moved = true;
                if (!direction.Equals("down"))
                    direction = "up";
                nodes[0].direction = direction;
                for (int i = 0; i < nodes.Count; i++)
                {
                    if (nodes[0].direction.Equals("down") && nodes[1].direction.Equals("up"))
                    {
                        nodes[0].direction = "up";
                    }
                    if (nodes[0].direction.Equals("up") && nodes[1].direction.Equals("down"))
                    {
                        nodes[0].direction = "down";
                    }
                    if (nodes[i].direction.Equals("left"))
                        nodes[i].column -= 15;
                    if (nodes[i].direction.Equals("right"))
                        nodes[i].column += 15;
                    if (nodes[i].direction.Equals("up"))
                        nodes[i].row -= 15;
                    if (nodes[i].direction.Equals("down"))
                        nodes[i].row += 15;
                }
                for (int h = nodes.Count - 1; h > 0; --h)
                {
                    nodes[h].direction = nodes[h - 1].direction;
                }
            }
            if (_currentKeyboardState.IsKeyDown(Keys.Down) && _previousKeyboardState.IsKeyUp(Keys.Down))
            {
                moved = true;
                if (!direction.Equals("up"))
                    direction = "down";
                nodes[0].direction = direction;
                for (int i = 0; i < nodes.Count; i++)
                {
                    if (nodes[0].direction.Equals("down") && nodes[1].direction.Equals("up"))
                    {
                        nodes[0].direction = "up";
                    }
                    if (nodes[0].direction.Equals("up") && nodes[1].direction.Equals("down"))
                    {
                        nodes[0].direction = "down";
                    }
                    if (nodes[i].direction.Equals("left"))
                        nodes[i].column -= 15;
                    if (nodes[i].direction.Equals("right"))
                        nodes[i].column += 15;
                    if (nodes[i].direction.Equals("up"))
                        nodes[i].row -= 15;
                    if (nodes[i].direction.Equals("down"))
                        nodes[i].row += 15;
                }
                for (int h = nodes.Count - 1; h > 0; --h)
                {
                    nodes[h].direction = nodes[h - 1].direction;
                }
            }
            if (timeElapsed > 0.4f - this.speed)
            {
                if (!moved)
                {
                    for (int i = 0; i < nodes.Count; i++)
                    {
                        if (nodes[0].direction.Equals("down") && nodes[1].direction.Equals("up"))
                        {
                            nodes[0].direction = "up";
                        }
                        if (nodes[0].direction.Equals("up") && nodes[1].direction.Equals("down"))
                        {
                            nodes[0].direction = "down";
                        }
                        if (nodes[0].direction.Equals("right") && nodes[1].direction.Equals("left"))
                        {
                            nodes[0].direction = "left";
                        }
                        if (nodes[0].direction.Equals("left") && nodes[1].direction.Equals("right"))
                        {
                            nodes[0].direction = "right";
                        }
                        if (nodes[i].direction.Equals("left"))
                            nodes[i].column -= 15;
                        if (nodes[i].direction.Equals("right"))
                            nodes[i].column += 15;
                        if (nodes[i].direction.Equals("up"))
                            nodes[i].row -= 15;
                        if (nodes[i].direction.Equals("down"))
                            nodes[i].row += 15;
                    }
                    for (int h = nodes.Count - 1; h > 0; --h)
                    {
                        nodes[h].direction = nodes[h - 1].direction;
                    }
                }
                moved = false;
                timeElapsed = 0f;
            }

            bool died = false;
            for (int j = 0; j < nodes.Count; j++)
            {
                if (j != 0 && nodes[0].row == nodes[j].row && nodes[0].column == nodes[j].column)
                {
                    died = true;
                }
            }
            if(nodes[0].row < 0 ||
                nodes[0].row > 500 ||
                nodes[0].column < 0 ||
                nodes[0].column > 1000) {
                    died = true;
            }
            for (int i = 0; i < donoteats.Count; i++)
            {
                if (donoteats[i].row == nodes[0].row && donoteats[i].column == nodes[0].column)
                {
                    died = true;
                }
            }
            if(died)
            {
                Exit();
            }

            if(eats.Count == 0)
            {
                donoteats.Clear();
                eats.Clear();

                loadLevel();
            }

            for (int i = 0; i < eats.Count; i++)
            {
                if (eats[i].row == nodes[0].row && eats[i].column == nodes[0].column)
                {
                    score++;

                    if (this.speed < 0.16f)
                        this.speed += 0.01f;
                    else if(this.speed < 0.25f)
                        this.speed += 0.001f;

                    this.Window.Title = "Score: " + this.score;

                    Node newNode = new Node();
                    Node lastNode = nodes[nodes.Count - 1];
                    newNode.direction = lastNode.direction;
                    if(newNode.direction.Equals("up"))
                    {
                        newNode.row = lastNode.row + 15;
                        newNode.column = lastNode.column;
                    }
                    if (newNode.direction.Equals("down"))
                    {
                        newNode.row = lastNode.row - 15;
                        newNode.column = lastNode.column;
                    }
                    if (newNode.direction.Equals("left"))
                    {
                        newNode.row = lastNode.row;
                        newNode.column = lastNode.column + 15;
                    }
                    if (newNode.direction.Equals("right"))
                    {
                        newNode.row = lastNode.row;
                        newNode.column = lastNode.column - 15;
                    }
                    eats.Remove(eats[i]);
                    nodes.Add(newNode);
                }
            }

            // After handling input
            _previousKeyboardState = Keyboard.GetState();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Green);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            spriteBatch.Draw(bg, new Rectangle(0, 0, 1000, 500), Color.Green);
            for (int j = 0; j < eats.Count; j++)
            {
                spriteBatch.Draw(eat, new Rectangle(eats[j].column, eats[j].row, 15, 15), Color.White);
            }
            for (int j = 0; j < donoteats.Count; j++)
            {
                spriteBatch.Draw(donoteat, new Rectangle(donoteats[j].column, donoteats[j].row, 15, 15), Color.White);
            }
            for (int j = 0; j < nodes.Count; j++)
            {
                if(j == 0)
                    spriteBatch.Draw(head, new Rectangle(nodes[j].column, nodes[j].row, 15, 15), Color.White);
                else if(j == nodes.Count - 1)
                    spriteBatch.Draw(rattle, new Rectangle(nodes[j].column, nodes[j].row, 15, 15), Color.White);
                else
                    spriteBatch.Draw(node, new Rectangle(nodes[j].column, nodes[j].row, 15, 15), Color.White);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
