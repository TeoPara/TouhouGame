using System;
using System.Collections.Generic;
using System.Text;
using OpenTK.Mathematics;
using System.Threading;

namespace AnemiaEngine
{
    class EnemyComponent : Component
    {
        Entity This;
        public EnemyComponent(Entity _This)
        {
            This = _This;

            Random rng = new Random();
            directionx = rng.NextDouble() > 0.5f;
            directiony = rng.NextDouble() > 0.5f;
        }
        float Time;


        bool directionx;
        bool directiony;
        public void Update()
        {
            if (directionx)
                This.Position += new Vector3(0.1f, 0, 0);
            else
                This.Position += new Vector3(-0.1f, 0, 0);

            if (directiony)
                This.Position += new Vector3(0, 0.05f, 0);
            else
                This.Position += new Vector3(0, -0.05f, 0);

            if (This.Position.X >= 20)
                directionx = false;
            if (This.Position.X <= -20)
                directionx = true;

            if (This.Position.Y >= 25)
                directiony = false;
            if (This.Position.Y <= 10)
                directiony = true;

            Random rnd = new Random();

            Time++;
            if (Time >= 50)
            {
                Time = 0;

                for (int i = -90; i < 360; i += (360 / 10))
                {
                    bool flipped = false;
                    float o = i;
                    if (o >= 90)
                    {
                        o -= 180f;
                        flipped = true;
                    }
                    Console.WriteLine(i);

                    Vector2 result;

                    if (flipped)
                        result = new Vector2(-1, MathF.Tan((MathF.PI / 180f) * o)).Normalized();
                    else
                        result = new Vector2(1, MathF.Tan((MathF.PI / 180f) * o)).Normalized();

                    {
                        Entity Bullet = new Entity("bullet", new List<Component> { new SpriteComponent(Controls.EnemyBullet) });
                        Bullet.Position = This.Position;
                        Bullet.Position.Z = 0;
                        Bullet.Components.Add(new BulletComponent(Bullet, This, new Vector3(result.X,result.Y,0), 0.1f));
                    }
                }
            }
        }
    }
}