using UnityEngine;

public class HomingBarrage1 : IBarrage
{
    CircleEmitter _circleEmitter;
    HomingSteering _homingSteering;

    public HomingBarrage1()
    {
        _circleEmitter = new CircleEmitter(3, 3);
        _homingSteering = new HomingSteering();
        _homingSteering.Init(GameState.PlayerTransform, 90);
    }

    public override void Execute(Vector3 origin)
    {
        BulletContext circleBulletcontext = new BulletContext(
            position: origin,
            velocity: 2f,
            acceleration: 4f,
            accelStartSecond: 0.5f,
            angularVelocityInit: 0f,
            angularAcceleration: 0f,
            curveStartSecond: 0f,
            canReflect: false,
            maxReflectNum: 0,
            degree: 0,
            steering: _homingSteering
        );

        _circleEmitter.Emit(circleBulletcontext);
    }
}
