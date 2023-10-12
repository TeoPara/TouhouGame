using System;
using OpenTK.Mathematics;
using System.Collections.Generic;
using System.Text;

namespace AnemiaEngine
{
    class Entity
    {
        public static List<Entity> All = new List<Entity>();

        public string Name { get; private set; }

        public List<Component> Components = new List<Component>();

        public Vector3 Position;

        public Entity(string name, List<Component> components = null)
        {
            Name = name;
            if (components != null)
                Components = components;
            All.Add(this);
        }

        public void Destroy()
        {
            Name = null;
            Position = Vector3.Zero;
            Components.Clear();
            All.Remove(this);
        }
        public void Hide()
        {
            foreach(Component c in Components)
            {
                if (c is SpriteComponent sc)
                    sc.IsDisabled = true;
            }
        }
        public void Show()
        {
            foreach (Component c in Components)
            {
                if (c is SpriteComponent sc)
                    sc.IsDisabled = false;
            }
        }

    }
}