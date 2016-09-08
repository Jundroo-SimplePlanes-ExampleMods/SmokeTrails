namespace Assets.SimplePlanesReflection.Assets.Scripts.Parts.Modifiers.Weapons.Events
{
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Reflection;
   using System.Text;
   using Explosions;
   using UnityEngine;

   public partial class MissileExplodedEventArgs : GenericProxyType<MissileExplodedEventArgs, EventArgs>
   {
      private static Property<Vector3> _blastDirection = CreateProperty<Vector3>("BlastDirection");

      private static Property<MonoBehaviour> _explosiveForce = CreateProperty<MonoBehaviour>("ExplosiveForce");

      private static Property<bool> _groundImpact = CreateProperty<bool>("GroundImpact");

      private static Property<MonoBehaviour> _missile = CreateProperty<MonoBehaviour>("Missile");

      private static Property<Vector3> _position = CreateProperty<Vector3>("Position");

      protected MissileExplodedEventArgs()
      {
      }

      public Vector3 BlastDirection
      {
         get
         {
            return this.Get(_blastDirection);
         }
      }

      public ExplosiveForceScript ExplosiveForce
      {
         get
         {
            return ExplosiveForceScript.Wrap(this.Get(_explosiveForce));
         }
      }

      public bool GroundImpact
      {
         get
         {
            return this.Get(_groundImpact);
         }
      }

      public MissileScript Missile
      {
         get
         {
            return MissileScript.Wrap(this.Get(_missile));
         }
      }

      public Vector3 Position
      {
         get
         {
            return this.Get(_position);
         }
      }
   }
}