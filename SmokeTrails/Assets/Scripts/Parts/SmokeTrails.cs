namespace Assets.Scripts.Parts
{
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using Jundroo.SimplePlanes.ModTools.Parts.Attributes;
   using UnityEngine;

   /// <summary>
   /// A part modifier for SimplePlanes.
   /// A part modifier is responsible for attaching a part modifier behavior script to a game object within a part's hierarchy.
   /// </summary>
   [Serializable]
   public class SmokeTrails : Jundroo.SimplePlanes.ModTools.Parts.PartModifier
   {
      /// <summary>
      /// The activation group used to activate the smoke trail.
      /// </summary>
      [SerializeField]
      [DesignerPropertyToggleButton("1", "2", "3", "4", "5", "6", "7", "8", Label = "Activation Group", Order = 10)]
      private int _activationGroup = 1;

      /// <summary>
      /// The maximum alpha value for the smoke particle's color.
      /// </summary>
      [SerializeField]
      [DesignerPropertySlider(0, 1, 21, Label = "Alpha", Order = 70)]
      private float _alpha = 0.8f;

      /// <summary>
      /// The density setting for the smoke trails (controls emission rate).
      /// </summary>
      [SerializeField]
      [DesignerPropertySlider(1, 5, 5, Label = "Density", Order = 20)]
      private float _density = 2f;

      /// <summary>
      /// A value indicating whether the smoke particles ignore the color and intensity settings of the directional light.
      /// </summary>
      [SerializeField]
      [DesignerPropertyToggleButton(Label = "Ignore Lighting", Order = 80)]
      private bool _ignoreLighting = false;

      /// <summary>
      /// The initial speed of the particles.
      /// </summary>
      [SerializeField]
      [DesignerPropertySlider(0, 20, 21, Label = "Initial Speed", Order = 60)]
      private int _initialSpeed = 0;

      /// <summary>
      /// The lifetime setting for the smoke trails (how long they last in seconds).
      /// </summary>
      [SerializeField]
      [DesignerPropertySlider(1, 20, 20, Label = "Lifetime", Order = 30)]
      private int _lifetime = 10;

      /// <summary>
      /// The maximum size of the smoke particles.
      /// </summary>
      [SerializeField]
      [DesignerPropertySlider(1, 20, 20, Label = "Max Size", Order = 50)]
      private int _maxSize = 10;

      /// <summary>
      /// The minimum size of the smoke particles.
      /// </summary>
      [SerializeField]
      [DesignerPropertySlider(1, 20, 20, Label = "Min Size", Order = 40)]
      private int _minSize = 2;

      /// <summary>
      /// Gets the activation group used to activate the smoke trail.
      /// </summary>
      /// <value>
      /// The activation group used to activate the smoke trail.
      /// </value>
      public int ActivationGroup
      {
         get
         {
            return this._activationGroup;
         }
      }

      /// <summary>
      /// Gets the maximum alpha value for the smoke particle's color.
      /// </summary>
      /// <value>
      /// The maximum alpha value for the smoke particle's color.
      /// </value>
      public float Alpha
      {
         get
         {
            return this._alpha;
         }
      }

      /// <summary>
      /// Gets the density setting for the smoke trails (controls emission rate).
      /// </summary>
      /// <value>
      /// The density setting for the smoke trails (controls emission rate).
      /// </value>
      public float Density
      {
         get
         {
            return this._density;
         }
      }

      /// <summary>
      /// Gets a value indicating whether the smoke particles ignore the color and intensity settings of the directional light.
      /// </summary>
      /// <value>
      ///   <c>true</c> if the smoke particles ignore the color and intensity settings of the directional light; otherwise, <c>false</c>.
      /// </value>
      public bool IgnoreLighting
      {
         get
         {
            return this._ignoreLighting;
         }
      }

      /// <summary>
      /// Gets the initial speed of the particles.
      /// </summary>
      /// <value>
      /// The initial speed of the particles.
      /// </value>
      public int InitialSpeed
      {
         get
         {
            return this._initialSpeed;
         }
      }

      /// <summary>
      /// Gets the lifetime setting for the smoke trails (how long they last in seconds).
      /// </summary>
      /// <value>
      /// The lifetime setting for the smoke trails (how long they last in seconds).
      /// </value>
      public int Lifetime
      {
         get
         {
            return this._lifetime;
         }
      }

      /// <summary>
      /// Gets the maximum size of the smoke particles.
      /// </summary>
      /// <value>
      /// The maximum size of the smoke particles.
      /// </value>
      public int MaxSize
      {
         get
         {
            return this._maxSize;
         }
      }

      /// <summary>
      /// Gets the minimum size of the smoke particles.
      /// </summary>
      /// <value>
      /// The minimum size of the smoke particles.
      /// </value>
      public int MinSize
      {
         get
         {
            return this._minSize;
         }
      }

      /// <summary>
      /// Called when this part modifiers is being initialized as the part game object is being created.
      /// </summary>
      /// <param name="partRootObject">The root game object that has been created for the part.</param>
      /// <returns>The created part modifier behavior, or <c>null</c> if it was not created.</returns>
      public override Jundroo.SimplePlanes.ModTools.Parts.PartModifierBehaviour Initialize(UnityEngine.GameObject partRootObject)
      {
         // Attach the behavior to the part's root object.
         var behaviour = partRootObject.AddComponent<SmokeTrailsBehavior>();
         return behaviour;
      }
   }
}