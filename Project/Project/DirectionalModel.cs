using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sample;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public class DirectionalModel : CustomEffectModel
    {
        public DirectionalModel(string asset, Vector3 position) : base(asset, position) { }

        public override void LoadContent()
        {
            CustomEffect =
                GameUtilities.Content.Load<Effect>(
                    "Effects/DirectionalLight");

            Texture2D tex =
                GameUtilities.Content.Load<Texture2D>("Textures/cubecover");

            Material = new DirectionalLightMaterial()
            {
                Texture = tex
            };

            base.LoadContent();
        }
    }
}
