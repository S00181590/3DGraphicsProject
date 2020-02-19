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
    public class CustomEffectModel : SimpleModel
    {
        public Effect CustomEffect { get; set; }
        public Material Material { get; set; }

        public CustomEffectModel(string asset, Vector3 position) :
            base("", asset, position)
        { }

        public override void LoadContent()
        {
            //load the model as normal
            base.LoadContent();

            if(Model != null)
            {
                GeneratedMeshTag();

                foreach (ModelMesh mesh in Model.Meshes)
                {
                    foreach (ModelMeshPart part in mesh.MeshParts)
                    {
                        part.Effect = CustomEffect;
                    }
                }
            }

            
        }

        public override void Draw(Camera camera)
        {
            if (CustomEffect != null)
            {
                SetModelEffect(CustomEffect, false);


                foreach (ModelMesh mesh in Model.Meshes)
                {
                    foreach (ModelMeshPart part in mesh.MeshParts)
                    {
                        SetEffectParameters(part.Effect, "World", BoneTransforms[mesh.ParentBone.Index] * World);
                        SetEffectParameters(part.Effect, "View", BoneTransforms[mesh.ParentBone.Index] * World);
                        SetEffectParameters(part.Effect, "Projection", BoneTransforms[mesh.ParentBone.Index] * World);

                        if (Material != null)
                            Material.SetEffectParameters(part.Effect);

                        mesh.Draw();
                    }
                    
                }
            }
            else
            {
                base.Draw(camera);
            }
        }

        public void GeneratedMeshTag()
        {
            foreach(ModelMesh mesh in Model.Meshes)
            {
                foreach(ModelMeshPart part in mesh.MeshParts)
                {
                    if(part.Effect is BasicEffect)
                    {
                        MeshTag tag = new MeshTag();

                        tag.color = (part.Effect as BasicEffect).DiffuseColor;
                        tag.Texture = (part.Effect as BasicEffect).Texture;
                        tag.SpecularPower = (part.Effect as BasicEffect).SpecularPower;

                        part.Tag = tag;

                    }
                }
            }
        }

        public virtual void CacheEffect()
        {
            foreach(ModelMesh mesh in Model.Meshes)
            {
                foreach (ModelMeshPart part in mesh.MeshParts)
                {
                    (part.Tag as MeshTag).CachedEffect = part.Effect;
                }
            }
        }

        public virtual void RestoreEffect()
        {
            foreach (ModelMesh mesh in Model.Meshes)
            {
                foreach (ModelMeshPart part in mesh.MeshParts)
                {
                    if((part.Tag as MeshTag).CachedEffect != null)
                    {
                        part.Effect = (part.Tag as MeshTag).CachedEffect;
                    }
                }
            }
        }

        public virtual void SetEffectParameters(Effect effect, string parameterName, object value)
        {
            //if any parameter is not on the effect. return to Caller.
            if(effect.Parameters[parameterName] == null)
            {
                return;
            }

            if(value is Vector3)
            {
                effect.Parameters[parameterName].SetValue((Vector3)value);
            }
            else if (value is Texture2D)
            {
                effect.Parameters[parameterName].SetValue((Texture2D)value);
            }
            else if (value is bool)
            {
                effect.Parameters[parameterName].SetValue((bool)value);
            }
            else if (value is Matrix)
            {
                effect.Parameters[parameterName].SetValue((Matrix)value);
            }
            else if (value is int)
            {
                effect.Parameters[parameterName].SetValue((int)value);
            }
            else if (value is float)
            {
                effect.Parameters[parameterName].SetValue((float)value);
            }
        }

        public virtual void SetModelEffect(Effect effect, bool copyEffect)
        {
            CacheEffect();

            foreach (ModelMesh mesh in Model.Meshes)
            {
                foreach (ModelMeshPart part in mesh.MeshParts)
                {
                    Effect effectToBeSet = effect;

                    if(copyEffect)
                    {
                        effectToBeSet = effect.Clone();
                    }

                    MeshTag tag = (part.Tag as MeshTag);

                    if (tag.Texture != null)
                    {
                        SetEffectParameters(effectToBeSet, "Texture", tag.Texture);
                        SetEffectParameters(effectToBeSet, "TextureEnabled", true);

                    }
                    else
                    {
                        SetEffectParameters(effectToBeSet, "TextureEnabled", false);
                    }

                    SetEffectParameters(effectToBeSet, "DiffuseColor", tag.color);
                    SetEffectParameters(effectToBeSet, "SpecularPower", tag.color);

                    part.Effect = effectToBeSet;
                }
            }
        }
    }
}
