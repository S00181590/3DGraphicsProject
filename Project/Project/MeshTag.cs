using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    class MeshTag
    {
        public Vector3 color { get; set; }
        public Texture2D Texture { get; set; }
        public float SpecularPower { get; set; }
        public Effect CachedEffect { get; set; }

        public MeshTag() { }

        public MeshTag(Vector3 Color, Texture2D texture, float specularpower)
        {
            color = Color;
        }
    }
}
