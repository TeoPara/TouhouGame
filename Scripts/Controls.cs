using System.Collections.Generic;
using System;
using System.Diagnostics;
using System.Linq;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace AnemiaEngine
{
    static class Controls
    {
        public static Entity Player;
        public static void Update()
        {
            Movement();
            Shooting();
        }

        static void Movement()
        {
            float speed = 25f;
            if (Game.Current.KeyboardState.IsKeyDown(Keys.LeftShift))
                speed /= 3;

            // left
            if (Player.Position.X > -25f && (Game.Current.KeyboardState.IsKeyDown(Keys.A) || Game.Current.KeyboardState.IsKeyDown(Keys.Left)))
            {
                Player.Position += new Vector3(-(speed * (float)Game.Current.UpdateTime), 0, 0);
                foreach (Component c in Player.Components)
                {
                    if (c is SpriteComponent sc)
                        sc.SetFace("left");
                }
            }
            // right
            if (Player.Position.X < 25f && (Game.Current.KeyboardState.IsKeyDown(Keys.D) || Game.Current.KeyboardState.IsKeyDown(Keys.Right)))
            {
                Player.Position += new Vector3((speed * (float)Game.Current.UpdateTime), 0, 0);
                foreach (Component c in Player.Components)
                {
                    if (c is SpriteComponent sc)
                        sc.SetFace("right");
                }
            }
            // up
            if (Player.Position.Y < 25f && (Game.Current.KeyboardState.IsKeyDown(Keys.W) || Game.Current.KeyboardState.IsKeyDown(Keys.Up)))
                Player.Position += new Vector3(0, (speed * (float)Game.Current.UpdateTime), 0);
            // down
            if (Player.Position.Y > -25f && (Game.Current.KeyboardState.IsKeyDown(Keys.S) || Game.Current.KeyboardState.IsKeyDown(Keys.Down)))
                Player.Position += new Vector3(0, -(speed * (float)Game.Current.UpdateTime), 0);

            // no move
            if (!Game.Current.KeyboardState.IsKeyDown(Keys.A) && !Game.Current.KeyboardState.IsKeyDown(Keys.D)
                && !Game.Current.KeyboardState.IsKeyDown(Keys.W) && !Game.Current.KeyboardState.IsKeyDown(Keys.S)
                && !Game.Current.KeyboardState.IsKeyDown(Keys.Left) && !Game.Current.KeyboardState.IsKeyDown(Keys.Right)
                && !Game.Current.KeyboardState.IsKeyDown(Keys.Up) && !Game.Current.KeyboardState.IsKeyDown(Keys.Down))
            {
                foreach (Component c in Player.Components)
                {
                    if (c is SpriteComponent sc)
                        sc.SetFace("up");
                }
            }
        }
        static public Sprite PlayerBullet = new Sprite(new Texture("Bullets/tile026.png", 0.125f), 2);
        static public Sprite EnemyBullet = new Sprite(new Texture("Bullets/tile019.png", 0.5f), 2);
        static void Shooting()
        {
            
            if (Game.Current.KeyboardState.IsKeyDown(Keys.Z) || Game.Current.KeyboardState.IsKeyDown(Keys.Enter))
            {
                if (Game.Current.sp.ElapsedMilliseconds - Game.Current.last > 50)
                {
                    Game.Current.last = Game.Current.sp.ElapsedMilliseconds;
                    if (!Game.Current.KeyboardState.IsKeyDown(Keys.LeftShift))
                    {
                        {
                            Entity Bullet = new Entity("bullet", new List<Component> { new SpriteComponent(PlayerBullet) });
                            Bullet.Position = Player.Position;
                            Bullet.Position.Z = 0;
                            Bullet.Components.Add(new BulletComponent(Bullet, Player, new Vector3(0, 1, 0), 1));
                        }
                        {
                            Entity Bullet = new Entity("bullet", new List<Component> { new SpriteComponent(PlayerBullet) });
                            Bullet.Position = Player.Position;
                            Bullet.Position.Z = 0;
                            Bullet.Components.Add(new BulletComponent(Bullet, Player, new Vector3(0.3f, 1, 0), 1));
                        }
                        {
                            Entity Bullet = new Entity("bullet", new List<Component> { new SpriteComponent(PlayerBullet) });
                            Bullet.Position = Player.Position;
                            Bullet.Position.Z = 0;
                            Bullet.Components.Add(new BulletComponent(Bullet, Player, new Vector3(0.6f, 1, 0), 1));
                        }
                        {
                            Entity Bullet = new Entity("bullet", new List<Component> { new SpriteComponent(PlayerBullet) });
                            Bullet.Position = Player.Position;
                            Bullet.Position.Z = 0;
                            Bullet.Components.Add(new BulletComponent(Bullet, Player, new Vector3(-0.3f, 1, 0), 1));
                        }
                        {
                            Entity Bullet = new Entity("bullet", new List<Component> { new SpriteComponent(PlayerBullet) });
                            Bullet.Position = Player.Position;
                            Bullet.Position.Z = 0;
                            Bullet.Components.Add(new BulletComponent(Bullet, Player, new Vector3(-0.6f, 1, 0), 1));
                        }
                    }
                    else
                    {
                        {
                            Entity Bullet = new Entity("bullet", new List<Component> { new SpriteComponent(PlayerBullet) });
                            Bullet.Position = Player.Position;
                            Bullet.Position.Z = 0;
                            Bullet.Components.Add(new BulletComponent(Bullet, Player, new Vector3(0, 1, 0), 1));
                        }
                        {
                            Entity Bullet = new Entity("bullet", new List<Component> { new SpriteComponent(PlayerBullet) });
                            Bullet.Position = Player.Position;
                            Bullet.Position.Z = 0;
                            Bullet.Components.Add(new BulletComponent(Bullet, Player, new Vector3(0.05f, 1, 0), 1));
                        }
                        {
                            Entity Bullet = new Entity("bullet", new List<Component> { new SpriteComponent(PlayerBullet) });
                            Bullet.Position = Player.Position;
                            Bullet.Position.Z = 0;
                            Bullet.Components.Add(new BulletComponent(Bullet, Player, new Vector3(-0.05f, 1, 0), 1));
                        }
                    }
                }
            }
        }
    }
}
