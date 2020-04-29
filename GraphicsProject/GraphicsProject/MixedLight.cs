using Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsProject
{
    class MixedLight : CustomEffectModel
    {
        public class LambertMaterial : Material
        {
            public Color AmbientColor { get; set; }
            public Color DiffuseColor { get; set; }
            public Color LightColor { get; set; }

            public Vector3 Direction { get; set; }
            public Texture2D Texture { get; set; }

            public override void SetEffectParameters(Effect effect)
            {
                effect.Parameters["AmbientColor"].SetValue(AmbientColor.ToVector3());
                effect.Parameters["DiffuseColor"].SetValue(DiffuseColor.ToVector3());
                effect.Parameters["DColor"].SetValue(LightColor.ToVector3());
                effect.Parameters["LightDirection"].SetValue(Direction);
                effect.Parameters["Texture"].SetValue(Texture);

                base.SetEffectParameters(effect);
            }
        }

        public MixedLight(string id, string asset, Vector3 position) : base(id, asset, position)
        {

        }

        public override void LoadContent()
        {
            Material = new LambertMaterial()
            {
                Texture = GameUtilities.Content.Load<Texture2D>("Texture\\Stone"),
                LightColor = Color.White,
                Direction = new Vector3(1, -1, 1),
                AmbientColor = Color.DarkGray
            };

            base.LoadContent();
        }
    }
}
