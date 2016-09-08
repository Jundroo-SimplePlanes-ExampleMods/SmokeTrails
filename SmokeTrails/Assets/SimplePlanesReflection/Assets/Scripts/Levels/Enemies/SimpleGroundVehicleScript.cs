namespace Assets.SimplePlanesReflection.Assets.Scripts.Levels.Enemies
{
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using UnityEngine;

   public partial class SimpleGroundVehicleScript : MonoBehaviourProxyType<SimpleGroundVehicleScript>
   {
      private static Field<float> __speed = CreateField<float>("_speed");

      private static Field<float> __targetVelocity = CreateField<float>("_targetVelocity");

      private static Property<MonoBehaviour> _damageableBody = CreateProperty<MonoBehaviour>("DamageableBody");

      private static Property<bool> _isDestroyed = CreateProperty<bool>("IsDestroyed");

      private static Property<bool> _isHostile = CreateProperty<bool>("IsHostile");

      protected SimpleGroundVehicleScript()
      {
      }

      public float _Speed
      {
         get
         {
            return this.Get(__speed);
         }

         set
         {
            this.Set(__speed, value);
         }
      }

      public float _TargetVelocity
      {
         get
         {
            return this.Get(__targetVelocity);
         }

         set
         {
            this.Set(__targetVelocity, value);
         }
      }

      public Damage.DamageableBody DamageableBody
      {
         get
         {
            return Damage.DamageableBody.Wrap(this.Get(_damageableBody));
         }
      }

      public bool IsDestroyed
      {
         get
         {
            return this.Get(_isDestroyed);
         }

         set
         {
            this.Set(_isDestroyed, value);
         }
      }

      public bool IsHostile
      {
         get
         {
            return this.Get(_isHostile);
         }

         set
         {
            this.Set(_isHostile, value);
         }
      }
   }
}