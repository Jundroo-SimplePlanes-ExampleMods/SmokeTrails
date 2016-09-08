namespace Assets.Scripts.Parts
{
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using Jundroo.SimplePlanes.ModTools;
   using SimplePlanesReflection.Assets.Scripts.Parts;
   using UnityEngine;

   /// <summary>
   /// The part modifier behavior for the smoke trail emitter part.
   /// </summary>
   /// <seealso cref="Jundroo.SimplePlanes.ModTools.Parts.PartModifierBehaviour" />
   public class SmokeTrailsBehavior : Jundroo.SimplePlanes.ModTools.Parts.PartModifierBehaviour
   {
      /// <summary>
      /// The color of the smoke trail.
      /// </summary>
      private Color _color;

      /// <summary>
      /// The controls for the player's aircraft.
      /// </summary>
      private IPlayerAircraftControls _controls;

      /// <summary>
      /// The emission module for the smoke trail particle system.
      /// </summary>
      private ParticleSystem.EmissionModule _emission;

      /// <summary>
      /// A value indicating if the smoke trails are currently enabled.
      /// </summary>
      private bool _enabled = false;

      /// <summary>
      /// A value indicating if the behavior is running inside the designer.
      /// </summary>
      private bool _inDesigner;

      /// <summary>
      /// The directional light source for the level.
      /// </summary>
      private Light _light;

      /// <summary>
      /// The modifier for the part.
      /// </summary>
      private SmokeTrails _modifier;

      /// <summary>
      /// The particle system for the smoke trail.
      /// </summary>
      private ParticleSystem _particleSystem;

      /// <summary>
      /// Awake is called when the script instance is being loaded.
      /// </summary>
      protected virtual void Awake()
      {
         this._inDesigner = ServiceProvider.Instance.GameState.IsInDesigner;
      }

      /// <summary>
      /// Start is called on the frame when a script is enabled just before any of the Update methods is called the first time.
      /// </summary>
      protected virtual void Start()
      {
         if (this._inDesigner)
         {
            return;
         }

         // Grab a reference to the modifier and aircraft controls as we will need them every frame
         this._modifier = (SmokeTrails)this.PartModifier;
         this._controls = ServiceProvider.Instance.PlayerAircraft.Controls;

         // Determine the color of the smoke which is driven by the paint color for the part
         this._color = this.LookupColor();

         // Find our primary light source so we can adjust the particle color with the time of day
         this._light = FindDirectionalLight();
         if (this._light == null)
         {
            Debug.LogError("Could not find the light source for the level.");
         }

         // Grab a reference to the particle system that will be emitting the smoke
         this._particleSystem = this.GetComponentInChildren<ParticleSystem>(true);
         if (this._particleSystem == null)
         {
            Debug.LogError("Particle system not found for smoke trail emitter.");
            return;
         }

         // Initialize our particle system and fire it up.
         this.InitializeParticleSystem();
         this._particleSystem.Play();
      }

      /// <summary>
      /// Update is called every frame, if the MonoBehaviour is enabled.
      /// </summary>
      protected virtual void Update()
      {
         if (this._inDesigner)
         {
            return;
         }

         // Check if the part is above sea level. If not, we want to disable the smoke trail if currently emitting.
         var seaLevel = ServiceProvider.Instance.GameWorld.FloatingOriginSeaLevel;
         var aboveSeaLevel = !seaLevel.HasValue || this.transform.position.y > seaLevel.Value;

         // Look at the activation group and determine if the trail should be enabled
         var enabledPreviousFrame = this._enabled;
         this._enabled = this._controls.GetActivationGroupState(this._modifier.ActivationGroup) && aboveSeaLevel;

         // Update the particle system emission rate if the system was enabled or disabled this frame.
         if (this._enabled && !enabledPreviousFrame)
         {
            this._emission.rate = new ParticleSystem.MinMaxCurve(this._modifier.Density);
         }
         else if (!this._enabled && enabledPreviousFrame)
         {
            this._emission.rate = new ParticleSystem.MinMaxCurve(0);
         }

         if (this._enabled)
         {
            // If lighting should not be ignored, update the particle system color
            // based on the color and intensity of the directional light in the scene.
            // This allows the particles to appear darking at night rather than unrealistically bright.
            if (!this._modifier.IgnoreLighting)
            {
               var color = this._light.color * this._light.intensity;
               color.a = 1f;

               this._particleSystem.startColor = color;
            }
         }
      }

      /// <summary>
      /// Finds the directional light in the scene.
      /// </summary>
      /// <returns>The first active directional light in the scene or <c>null</c> if one could not be found.</returns>
      private static Light FindDirectionalLight()
      {
         foreach (var light in GameObject.FindObjectsOfType<Light>())
         {
            if (light.type == LightType.Directional && light.gameObject.activeInHierarchy)
            {
               return light;
            }
         }

         return null;
      }

      /// <summary>
      /// Creates the color gradient to be used to configure the particle system.
      /// This is used to fade out the alpha values at the end of the trail.
      /// </summary>
      /// <returns>The color gradient to be used to configure the particle system.</returns>
      private Gradient CreateColorGradient()
      {
         var g = new Gradient();
         g.colorKeys = new GradientColorKey[]
         {
            new GradientColorKey(this._color, 0),
            new GradientColorKey(this._color, 1),
         };
         g.alphaKeys = new GradientAlphaKey[]
         {
            new GradientAlphaKey(this._modifier.Alpha, 0),
            new GradientAlphaKey(this._modifier.Alpha, 0.6f),
            new GradientAlphaKey(0, 1),
         };

         return g;
      }

      /// <summary>
      /// Initializes the colors for the smoke trail particle system.
      /// </summary>
      private void InitializeParticleColors()
      {
         var colorLifetimeModule = this._particleSystem.colorOverLifetime;
         colorLifetimeModule.enabled = true;
         colorLifetimeModule.color = new ParticleSystem.MinMaxGradient(this.CreateColorGradient());
      }

      /// <summary>
      /// Initializes the particle sizes for the smoke trails particle system.
      /// </summary>
      private void InitializeParticleSizes()
      {
         float minSize = this._modifier.MinSize / 2f;
         float maxSize = this._modifier.MaxSize / 2f;

         this._particleSystem.startSize = maxSize;

         var sizeModule = this._particleSystem.sizeOverLifetime;
         sizeModule.enabled = true;
         sizeModule.size = new ParticleSystem.MinMaxCurve(1, new AnimationCurve(new Keyframe(0, minSize / maxSize), new Keyframe(1, 1)));
      }

      /// <summary>
      /// Initializes the smoke trail particle system.
      /// </summary>
      private void InitializeParticleSystem()
      {
         this._particleSystem.startLifetime = this._modifier.Lifetime;
         this._particleSystem.startSpeed = this._modifier.InitialSpeed;

         this.InitializeParticleColors();
         this.InitializeParticleSizes();

         this._emission = this._particleSystem.emission;
         this._emission.rate = new ParticleSystem.MinMaxCurve(0);
      }

      /// <summary>
      /// Finds the color to be used for the smoke trails based off the Trim 1 paint color for the part.
      /// </summary>
      /// <returns>The color to be used for the smoke trails.</returns>
      private Color LookupColor()
      {
         try
         {
            var part = PartScript.GetComponentInParent(this);
            var materialId = part.Part.MaterialIds[1];
            var themeMaterials = part.Aircraft.Theme.Theme.Materials;
            foreach (var material in themeMaterials)
            {
               if (material.Id == materialId)
               {
                  return material.Color;
               }
            }
         }
         catch (Exception ex)
         {
            Debug.LogError("Unable to look up color for the smoke trail particles");
            Debug.LogException(ex);
         }

         return Color.grey;
      }
   }
}