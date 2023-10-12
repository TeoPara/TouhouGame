using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace AnemiaEngine
{
    class SpriteComponent : Component
    {
        public Sprite ActiveSprite;

        public List<Sprite> NeutralSprites = new List<Sprite>();
        public List<Sprite> DownSprites = new List<Sprite>();
        public List<Sprite> RightSprites = new List<Sprite>();
        public List<Sprite> LeftSprites = new List<Sprite>();
        public List<Sprite> UpSprites = new List<Sprite>();

        public SpriteComponent(Sprite c)
        {
            ActiveSprite = c;
        }
        public SpriteComponent(List<Sprite> down, List<Sprite> right, List<Sprite> left, List<Sprite> up, List<Sprite> neutral)
        {
            DownSprites = down;
            RightSprites = right;
            LeftSprites = left;
            UpSprites = up;
            NeutralSprites = neutral;

            ActiveSprite = neutral[0];
        }

        public void SetFace(string face)
        {
            switch (face)
            {
                case "left":
                    // currently neutral
                    if (NeutralSprites.Any(c => c == ActiveSprite))
                    {
                        // switch to left
                        // check if not already left
                        if (!LeftSprites.Any(c => c == ActiveSprite))
                        {
                            ReverseLeft = false;
                            ActiveSprite = LeftSprites[0];
                        }
                    }
                    // currently right
                    else if (RightSprites.Any(c => c == ActiveSprite))
                    {
                        // reverse left
                        ReverseRight = true;
                    }
                    break;
                case "right":
                    // currently neutral
                    if (NeutralSprites.Any(c => c == ActiveSprite))
                    {
                        // switch to right
                        // check if not already right
                        if (!RightSprites.Any(c => c == ActiveSprite))
                        {
                            ReverseRight = false;
                            ActiveSprite = RightSprites[0];
                        }
                    }
                    // currently left
                    else if (LeftSprites.Any(c => c == ActiveSprite))
                    {
                        // reverse left
                        ReverseLeft = true;
                    }
                    break;
                default:
                    // currently left
                    if (LeftSprites.Any(c => c == ActiveSprite))
                    {
                        ReverseLeft = true;
                    }
                    // currently right
                    else if (RightSprites.Any(c => c == ActiveSprite))
                    {
                        ReverseRight = true;
                    }
                    else
                    {
                        if (!NeutralSprites.Any(c => c == ActiveSprite))
                        {
                            ActiveSprite = NeutralSprites[0];
                        }
                    }
                    break;
            }
        }

        bool ReverseLeft = false;
        bool ReverseRight = false;

        float LastTime = 0;


        public bool IsDisabled = false;
        public void Update()
        {
            if (IsDisabled)
                return;

            LastTime++;

            int time = 5;
            if (LeftSprites.Any(c => c == ActiveSprite) && ReverseLeft)
                time = 2;
            if (RightSprites.Any(c => c == ActiveSprite) && ReverseRight)
                time = 2;
            if (LastTime >= time)
            {
                LastTime = 0;
                
                // neutral
                if (NeutralSprites.Any(c => c == ActiveSprite))
                {
                    if (NeutralSprites.IndexOf(ActiveSprite) == NeutralSprites.Count - 1)
                        ActiveSprite = NeutralSprites[0];
                    else
                        ActiveSprite = NeutralSprites[NeutralSprites.IndexOf(ActiveSprite) + 1];
                }
                // left
                if (LeftSprites.Any(c => c == ActiveSprite))
                {
                    // normal
                    if (!ReverseLeft)
                    {
                        if (LeftSprites.IndexOf(ActiveSprite) == LeftSprites.Count - 1)
                            ActiveSprite = LeftSprites[LeftSprites.Count - 2];
                        else
                            ActiveSprite = LeftSprites[LeftSprites.IndexOf(ActiveSprite) + 1];
                    }
                    // reversing
                    else
                    {
                        if (LeftSprites.IndexOf(ActiveSprite) == 0)
                            ActiveSprite = NeutralSprites[0];
                        else
                            ActiveSprite = LeftSprites[LeftSprites.IndexOf(ActiveSprite) - 1];
                    }
                }
                // right
                if (RightSprites.Any(c => c == ActiveSprite))
                {
                    // normal
                    if (!ReverseRight)
                    {
                        if (RightSprites.IndexOf(ActiveSprite) == RightSprites.Count - 1)
                            ActiveSprite = RightSprites[RightSprites.Count - 2];
                        else
                            ActiveSprite = RightSprites[RightSprites.IndexOf(ActiveSprite) + 1];
                    }
                    // reversing
                    else
                    {
                        if (RightSprites.IndexOf(ActiveSprite) == 0)
                            ActiveSprite = NeutralSprites[0];
                        else
                            ActiveSprite = RightSprites[RightSprites.IndexOf(ActiveSprite) - 1];
                    }
                }
            }
        }
    }
}
