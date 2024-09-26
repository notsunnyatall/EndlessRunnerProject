using UnityEngine;

namespace EndlessRunner.Abilities
{
    public class AbilityData
    {
        GameObject user;
        GameObject target;

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

        public void SetTarget(GameObject target)
        {
            this.target = target;
        }
    }
}