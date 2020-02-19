using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sample;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphics
{
    public class ColorModel : CustomEffectModel
    {
        public ColorModel(string asset, Vector3 position) : base(asset, position) { }

        public override void LoadContent()
        {
            CustomEffect =
                GameUtilities.Content.Load<Effect>(
                    "Effects/EmptyEffect");

            Material = new ColorMaterial();

            base.LoadContent();
        }
    }
}
