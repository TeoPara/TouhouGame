using System.Collections.Generic;
using System;
using System.Threading;
using System.Diagnostics;
using System.Linq;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace AnemiaEngine
{
    class Game : GameWindow
    {
        public static Game Current;

        public Game()
            : base(new GameWindowSettings() { }, new NativeWindowSettings() { Title = "Touhou ??? The divine crackhead", StartFocused = true})
        {
            Current = this;
            this.CenterWindow(new Vector2i(750, 775));
            GL.Enable(EnableCap.Texture2D);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(0, BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
            
        }

        public Vector3 cameraPosition = new Vector3(0f, 0f, 5f);       
        public HashSet<Vector3> floorPositions = new HashSet<Vector3>();


        public Entity Life1;
        public Entity Life2;
        public Entity Life3;

        protected override void OnLoad()
        {
            sp.Start();

            List<Sprite> left = new List<Sprite>()
            {
                new Sprite(new Texture("Player/MarisaLeft1.png"), 3),
                new Sprite(new Texture("Player/MarisaLeft2.png"), 3),
                new Sprite(new Texture("Player/MarisaLeft3.png"), 3),
                new Sprite(new Texture("Player/MarisaLeft4.png"), 3),
                new Sprite(new Texture("Player/MarisaLeft5.png"), 3),
                new Sprite(new Texture("Player/MarisaLeft6.png"), 3),
                new Sprite(new Texture("Player/MarisaLeft7.png"), 3),
                new Sprite(new Texture("Player/MarisaLeft8.png"), 3),
            };
            List<Sprite> right = new List<Sprite>()
            {
                new Sprite(new Texture("Player/MarisaRight1.png"), 3),
                new Sprite(new Texture("Player/MarisaRight2.png"), 3),
                new Sprite(new Texture("Player/MarisaRight3.png"), 3),
                new Sprite(new Texture("Player/MarisaRight4.png"), 3),
                new Sprite(new Texture("Player/MarisaRight5.png"), 3),
                new Sprite(new Texture("Player/MarisaRight6.png"), 3),
                new Sprite(new Texture("Player/MarisaRight7.png"), 3),
                new Sprite(new Texture("Player/MarisaRight8.png"), 3),
            };
            List<Sprite> neutral = new List<Sprite>()
            {
                new Sprite(new Texture("Player/MarisaNeutral1.png"), 3),
                new Sprite(new Texture("Player/MarisaNeutral2.png"), 3),
                new Sprite(new Texture("Player/MarisaNeutral3.png"), 3),
                new Sprite(new Texture("Player/MarisaNeutral4.png"), 3),
                new Sprite(new Texture("Player/MarisaNeutral5.png"), 3),
                new Sprite(new Texture("Player/MarisaNeutral6.png"), 3),
                new Sprite(new Texture("Player/MarisaNeutral7.png"), 3),
                new Sprite(new Texture("Player/MarisaNeutral8.png"), 3),
            };


            Entity Player = new Entity("player", new List<Component> { new SpriteComponent(null, right, left, null, neutral) });
            Player.Components.Add(new HealthComponent(Player, 3) { IsPlayer = true });
            Player.Position = new Vector3(0, 0, 0.1f);
            Controls.Player = Player;


            
            Sprite uitestsprite = new Sprite(new Texture("LifePickup.png"), 0.1f);
            uitestsprite.isUI = true;
            {
                Life1 = new Entity("life1");
                Life1.Components.Add(new SpriteComponent(uitestsprite));
                Life1.Position = new Vector3(0.94f, 0.94f, 0);
            }
            {
                Life2 = new Entity("life2");
                Life2.Components.Add(new SpriteComponent(uitestsprite));
                Life2.Position = new Vector3(0.94f, 0.83f, 0);
            }
            {
                Life3 = new Entity("life3");
                Life3.Components.Add(new SpriteComponent(uitestsprite));
                Life3.Position = new Vector3(0.94f, 0.72f, 0);
            }

            
            {
                Entity Enemy = new Entity("enemy");
                Enemy.Components.Add(new SpriteComponent(new Sprite(new Texture("Enemy/enemy1.png"), 2f)));
                Enemy.Components.Add(new EnemyComponent(Enemy));
                Enemy.Components.Add(new HealthComponent(Enemy, 1));
                Enemy.Position = new Vector3(0, 10, 0);
            }
            
            //
            base.OnLoad();
        }
        protected override void OnRenderFrame(FrameEventArgs args)
        {
            // clear buffers
            GL.Clear(ClearBufferMask.DepthBufferBit | ClearBufferMask.ColorBufferBit);
            GL.ClearColor(0.1f, 0.1f, 0.1f, 0);

            // render stuff
            foreach (Entity e in Entity.All.OrderBy(e2 => e2.Position.Z))
            {
                foreach(Component c in e.Components)
                {
                    if (c is SpriteComponent sc)
                    {
                        if (!sc.IsDisabled)
                        {
                            if (!sc.ActiveSprite.isUI)
                                sc.ActiveSprite.Draw(e.Position);
                            else
                                sc.ActiveSprite.DrawUI(e.Position);
                        }
                    }
                }
            }

            // end
            Context.SwapBuffers();
            base.OnRenderFrame(args);
        }
        protected override void OnUnload()
        {
            // unload buffer
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            base.OnUnload();
        }

        public Stopwatch sp = new Stopwatch();
        public long last = 0;

        public long LastWaveTime = 0;
        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            /*
            if (sp.ElapsedMilliseconds - LastWaveTime > 1000)
            {
                {
                    Random rng = new Random();
                    float randomx = (float)rng.NextDouble() * 20f;
                    if (rng.NextDouble() > 0.5f)
                        randomx -= randomx * 2;
                    float randomy = (float)rng.NextDouble() * 10f;
                    if (rng.NextDouble() > 0.5f)
                        randomy -= randomy * 2;


                    Entity Enemy = new Entity("enemy");
                    Enemy.Components.Add(new SpriteComponent(new Sprite(new Texture("Enemy/enemy1.png"), 2f)));
                    Enemy.Components.Add(new EnemyComponent(Enemy));
                    Enemy.Components.Add(new HealthComponent(Enemy, 1));
                    Enemy.Position = new Vector3(randomx, 10 + randomy, 0);
                }
                LastWaveTime = sp.ElapsedMilliseconds;
            }
            */

            // update components

            foreach (Entity e in Entity.All.ToList())
            {
                foreach(Component c in e.Components.ToList())
                {
                    if (c is MovementComponent mc)
                        mc.Update();
                    if (c is BulletComponent bc)
                        bc.Update();
                    if (c is SpriteComponent sc)
                        sc.Update();
                    if (c is EnemyComponent ec)
                        ec.Update();
                }
            }

            
            // destroy bullets far
            foreach (Entity e in Entity.All.Where(e => e.Name == "bullet" && (e.Position.Y > 25 || e.Position.Y < -25)).ToList())
                e.Destroy();

            // controls
            Controls.Update();

            base.OnUpdateFrame(args);
        }
        protected override void OnResize(ResizeEventArgs e)
        {
            GL.Viewport(0, 0, e.Width, e.Height);
            base.OnResize(e);
        }
    }
}