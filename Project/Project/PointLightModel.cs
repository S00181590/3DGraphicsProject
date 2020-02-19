using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sample;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public class PointLightModel : CustomEffectModel
    {
        public PointLightModel(string asset, Vector3 position) : base(asset, position) {
            World *= Matrix.CreateScale(10);
        }

        public override void LoadContent()
        {
            CustomEffect =GameUtilities.Content.Load<Effect>(
                "Effects/NormalPointLight");

            Texture2D tex = 
                GameUtilities.Content.Load<Texture2D>(
                    "Textures/rocks");

            Texture2D normalTex = 
                GameUtilities.Content.Load<Texture2D>(
                    "Textures/rocks_normal");

            Material = new PointLightMaterial()
            {
                Texture = tex,
                NormalMap = normalTex
            };

            base.LoadContent();
        }

        public override void Update()
        {

            float radius = (Material as PointLightMaterial).Attenuation;
            Color color = (Material as PointLightMaterial).LightColor;
            Vector3 pos = (Material as PointLightMaterial).Position;

            DebugEngine.AddBoundingSphere(
                new BoundingSphere(pos, radius), color);

            if (InputEngine.IsKeyHeld(Keys.Add))
            {
                (Material as PointLightMaterial).Attenuation += 0.5f;
            }
            else if(InputEngine.IsKeyHeld(Keys.Subtract))
            {
                (Material as PointLightMaterial).Attenuation -= 0.5f;
            }

            if(InputEngine.IsKeyHeld(Keys.Left))
            {
                (Material as PointLightMaterial).Position -= new Vector3(0.25f, 0, 0);
            }
            else if(InputEngine.IsKeyHeld(Keys.Right))
            {
                (Material as PointLightMaterial).Position += new Vector3(0.25f, 0, 0);
            }

            if (InputEngine.IsKeyHeld(Keys.Up))
            {
                (Material as PointLightMaterial).Position += new Vector3(0, 0.25f, 0);
            }
            else if (InputEngine.IsKeyHeld(Keys.Down))
            {
                (Material as PointLightMaterial).Position -= new Vector3(0, 0.25f, 0);
            }

            if (InputEngine.IsKeyHeld(Keys.Z))
            {
                (Material as PointLightMaterial).Position += new Vector3(0, 0, 0.25f);
            }
            else if (InputEngine.IsKeyHeld(Keys.X))
            {
                (Material as PointLightMaterial).Position -= new Vector3(0, 0, 0.25f);
            }

            base.Update();
        }
    }
}
