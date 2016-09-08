namespace Assets.SimplePlanesReflection.Assets.Game.AircraftIo
{
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using UnityEngine;

   public partial class PartMaterial : ProxyType<PartMaterial>
   {
      private static Property<Color> _color = CreateProperty<Color>("Color");

      private static Property<int> _id = CreateProperty<int>("Id");

      private static Property<float> _legacyReflectivity = CreateProperty<float>("LegacyReflectivity");

      private static Property<float> _metallic = CreateProperty<float>("Metallic");

      private static Property<float> _smoothness = CreateProperty<float>("Smoothness");

      private static Property<float> _smoothnessModifier = CreateProperty<float>("SmoothnessModifier");

      protected PartMaterial()
      {
      }

      public Color Color
      {
         get
         {
            return this.Get(_color);
         }
      }

      public int Id
      {
         get
         {
            return this.Get(_id);
         }
      }

      public float LegacyReflectivity
      {
         get
         {
            return this.Get(_legacyReflectivity);
         }
      }

      public float Metallic
      {
         get
         {
            return this.Get(_metallic);
         }
      }

      public float Smoothness
      {
         get
         {
            return this.Get(_smoothness);
         }
      }

      public float SmoothnessModifier
      {
         get
         {
            return this.Get(_smoothnessModifier);
         }
      }
   }
}