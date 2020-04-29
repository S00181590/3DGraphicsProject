using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Graphics
{
    public class MeshTag
    {
        public Vector3 Color;
        public Texture2D Texture;
        public float SpecularPower;
        public Vector3 SpecularColor;

        public Effect CachedEffect;

        public MeshTag() { }

        public MeshTag(Vector3 color, Texture2D texture, float specularPower, Vector3 specularColor)
        {
            Color = color;//diffuse
            Texture = texture;
            SpecularPower = specularPower;
            SpecularColor = specularColor;
        }
    }
}
