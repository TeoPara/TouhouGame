using System;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;
using OpenTK.Mathematics;

namespace AnemiaEngine
{
    class MovementComponent : Component
    {
        Vector3 MovementTarget;
        bool Moving = false;

        Entity This;     
        public MovementComponent(Entity owner)
        {
            This = owner;
            MovementTarget = owner.Position;
        }
        
        public void MoveTo(Vector2 dir)
        {
            if (Moving) return;
            Moving = true;

            SpriteComponent sc = (SpriteComponent)This.Components.FirstOrDefault(c => c is SpriteComponent);

            if (dir == new Vector2(1, 0))
            {
                MovementTarget = This.Position + new Vector3(1, 0, 0);
                sc?.SetFace("right");
            }
            if (dir == new Vector2(-1, 0))
            {
                MovementTarget = This.Position + new Vector3(-1, 0, 0);
                sc?.SetFace("left");
            }
            if (dir == new Vector2(0, 1))
            {
                MovementTarget = This.Position + new Vector3(0, 1, 0);
                sc?.SetFace("up");
            }
            if (dir == new Vector2(0, -1))
            {
                MovementTarget = This.Position + new Vector3(0, -1, 0);
                sc?.SetFace("down");
            }
        }
        public void Update()
        {
            if (MovementTarget != This.Position)
            {
                float Z = This.Position.Z;

                if (Vector3.Distance(This.Position, MovementTarget) > (1f / 60f * 8))
                    This.Position += (MovementTarget - This.Position).Normalized() * (1f / 60f * 8);
                else
                    This.Position = MovementTarget;

                This.Position.Z = Z;
            }
            else
            {
                Moving = false;
            }
        }
    }
}
