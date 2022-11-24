using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetector : MonoBehaviour, IHitDetector
{
    [SerializeField] BoxCollider _collider;
    [SerializeField] LayerMask hurtBoxLayer;

    float _thikness = .025f;
    IHitResponder _responder;

    public IHitResponder HitResponder { get => _responder; set => _responder = value; }

    public void CheckHit()
    {
        Vector3 _scaleSize = new Vector3(
            _collider.size.x * transform.lossyScale.x, 
            _collider.size.y * transform.lossyScale.y, 
            _collider.size.z * transform.lossyScale.z);

        float _distance = _scaleSize.y - _thikness;
        Vector3 _direction = (transform.up + transform.right).normalized;
        Vector3 _center = transform.TransformPoint(_collider.center);
        Vector3 _start = _center - _direction * (_distance / 2);
        Vector3 _halfExtents = new Vector3(_scaleSize.x, _thikness, _scaleSize.z) / 2;
        Quaternion _orientation = transform.rotation;

        RaycastHit[] hits = Physics.BoxCastAll(_center, _halfExtents, _direction, _orientation, _distance, hurtBoxLayer);

        HitData hitData = null;
        IHurtBox _hurtBox = null;

        foreach (RaycastHit hit in hits)
        {
            _hurtBox = hit.collider.GetComponent<IHurtBox>();

            if(_hurtBox != null && _hurtBox.Active)
            {
                hitData = new HitData
                {
                    hitPoint = hit.point == Vector3.zero ? _center : hit.point,
                    hitNormal = hit.normal,
                    hurtBox = _hurtBox,
                    hitDetector = this
                };

                if (hitData.Validate())
                {
                    hitData.hitDetector.HitResponder?.Response(hitData);
                    hitData.hurtBox.HurtResponder?.Response(hitData);
                }
            }
        }

    }

   
}
