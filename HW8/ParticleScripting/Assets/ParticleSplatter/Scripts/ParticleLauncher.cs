using UnityEngine;
using System.Collections.Generic;

public class ParticleLauncher : MonoBehaviour {
    public ParticleSystem particleLauncher;
    public ParticleSystem splatterParticles;
    public DecalPool decalPool;
    public Gradient particleColorGradient;

    private List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();

    private void Update() {
        if (Input.GetButton("Fire1")) {
            // set a random color for the bullet
            ParticleSystem.MainModule psMain = particleLauncher.main;
            psMain.startColor = particleColorGradient.Evaluate(Random.Range(0f, 1f));
            // emit the bullet
            particleLauncher.Emit(1);
        }
    }

    private void OnParticleCollision(GameObject other) {
        ParticlePhysicsExtensions.GetCollisionEvents(particleLauncher, other, collisionEvents);
        // display the decal and emit the splatter where the bullet collides with the wall or the floor
        foreach (ParticleCollisionEvent collisionEvent in collisionEvents) {
            decalPool.ParticleHit(collisionEvent, particleColorGradient);
            EmitSplatterAtLocation(collisionEvent);
        }
    }

    private void EmitSplatterAtLocation(ParticleCollisionEvent particleCollisionEvent) {
        // set the position of the particle system for   splatter
        splatterParticles.transform.position = particleCollisionEvent.intersection;
        splatterParticles.transform.rotation = Quaternion.LookRotation(particleCollisionEvent.normal);
        // set a random color for the splatter
        ParticleSystem.MainModule psMain = splatterParticles.main;
        psMain.startColor = particleColorGradient.Evaluate(Random.Range(0f, 1f));
        // emit the splatter
        splatterParticles.Emit(1);
    }
}
