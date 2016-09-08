namespace Assets.SimplePlanesReflection.Assets.Scripts.Parts.Modifiers.Weapons.Events
{
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Reflection;
   using System.Text;
   using Explosions;
   using UnityEngine;

   public partial class RocketExplodedEventArgs : GenericProxyType<RocketExplodedEventArgs, EventArgs>
   {
      private static Property<Vector3> _blastDirection = CreateProperty<Vector3>("BlastDirection");

      private static Property<MonoBehaviour> _explosiveForce = CreateProperty<MonoBehaviour>("ExplosiveForce");

      private static Property<Vector3> _position = CreateProperty<Vector3>("Position");

      private static Property<MonoBehaviour> _rocket = CreateProperty<MonoBehaviour>("Rocket");

      protected RocketExplodedEventArgs()
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

      public Vector3 Position
      {
         get
         {
            return this.Get(_position);
         }
      }

      public RocketScript Rocket
      {
         get
         {
            return RocketScript.Wrap(this.Get(_rocket));
         }
      }
   }
}