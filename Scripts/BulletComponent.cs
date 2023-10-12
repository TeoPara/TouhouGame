using OpenTK.Mathematics;
using System.Linq;
using System.Collections.Generic;

namespace AnemiaEngine
{
    class BulletComponent : Component
    {
        Vector3 Direction;
        float Speed;

        Entity Owner;
        Entity This;

        public static List<Sprite> BlueSprites = new List<Sprite>()
        {
            new Sprite(new Texture("tile018")),
            new Sprite(new Texture("tile019")),
            new Sprite(new Texture("tile020")),
            new Sprite(new Texture("tile021")),
            new Sprite(new Texture("tile022")),
            new Sprite(new Texture("tile023")),
        };
        public static List<Sprite> RedSprites = new List<Sprite>()
        {
            new Sprite(new Texture("tile002")),
            new Sprite(new Texture("tile003")),
            new Sprite(new Texture("tile004")),
            new Sprite(new Texture("tile005")),
            new Sprite(new Texture("tile006")),
            new Sprite(new Texture("tile007")),
        };
        public static List<Sprite> YellowSprites = new List<Sprite>()
        {
            new Sprite(new Texture("tile026")),
            new Sprite(new Texture("tile027")),
            new Sprite(new Texture("tile028")),
            new Sprite(new Texture("tile029")),
            new Sprite(new Texture("tile030")),
            new Sprite(new Texture("tile031")),
        };
        public static List<Sprite> GreenSprites = new List<Sprite>()
        {
            new Sprite(new Texture("tile010")),
            new Sprite(new Texture("tile011")),
            new Sprite(new Texture("tile012")),
            new Sprite(new Texture("tile013")),
            new Sprite(new Texture("tile014")),
            new Sprite(new Texture("tile015")),
        };

        public enum BulletColors { Yellow, Blue, Red, Green};
        public BulletColors BulletColor;

        public BulletComponent(Entity entity, Entity owner, Vector3 direction, float speed)
        {
            This = entity;
            Owner = owner;
            Direction = direction;
            Speed = speed;
        }

        public void Update()
        {
            This.Position += Direction * Speed;

            foreach(Entity e in Entity.All.ToList())
            {
                if (e.Components.Any(c => c is BulletComponent))
                    continue;

                HealthComponent hc = (HealthComponent)e.Components.Find(c => c is HealthComponent);
                if (hc == null)
                    continue;

                if (Owner == e)
                    continue;
                if (e.Name == "enemy" && Owner.Name == "enemy")
                    continue;

                if (Vector3.Distance(This.Position, e.Position) < 0.5f)
                {
                    hc.Health--;
                    This.Destroy();
                }
            }
        }
    }
}
