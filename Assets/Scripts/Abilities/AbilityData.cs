using UnityEngine;

namespace EndlessRunner.Abilities
{
    public class AbilityData
    {
        GameObject user;
        GameObject target;
        Vector3 targetPoint;

        public AbilityData(GameObject user)
        {
            this.user = user;
        }

        public GameObject GetUser()
        {
            return user;
        }

        public GameObject GetTarget()
        {
            return target;
        }

        public Vector3 GetTargetPoint()
        {
            return targetPoint;
        }

        public void SetTarget(GameObject target)
        {
            this.target = target;
        }

        public void SetTargetPoint(Vector3 targetPoint)
        {
            this.targetPoint = targetPoint;
        }
    }
}