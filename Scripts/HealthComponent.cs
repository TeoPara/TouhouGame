using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnemiaEngine
{
    class HealthComponent : Component
    {
        public int Health
        {
            get{ return _health; }
            set
            {
                _health = value;
                if (value <= -1)
                    This.Destroy();
                if (IsPlayer)
                {
                    foreach (Entity e in Entity.All.ToList())
                    {
                        if (e.Name == "bullet")
                            e.Destroy();
                    }


                    if (value == 0)
                    {
                        Game.Current.Life1.Hide();
                        Game.Current.Life2.Hide();
                        Game.Current.Life3.Hide();
                        Console.WriteLine(0);
                    }
                    if (value == 1)
                    {
                        Game.Current.Life1.Show();
                        Game.Current.Life2.Hide();
                        Game.Current.Life3.Hide();
                        Console.WriteLine(1);

                    }
                    if (value == 2)
                    {
                        Game.Current.Life1.Show();
                        Game.Current.Life2.Show();
                        Game.Current.Life3.Hide();
                        Console.WriteLine(2);

                    }
                    if (value == 3)
                    {
                        Game.Current.Life1.Show();
                        Game.Current.Life2.Show();
                        Game.Current.Life3.Show();
                        Console.WriteLine(3);

                    }
                }
            }
        }
        int _health;
        Entity This;
        public bool IsPlayer = false;
        public HealthComponent(Entity _This, int health = 1)
        {
            This = _This;
            Health = health;
        }
    }
}
