using UnityEngine;

namespace App.Scripts.Game.Configs.View
{
    [CreateAssetMenu(fileName = "ObjectOrdersConfig", menuName = "Configs/View/ObjectOrders")]

    public class ObjectOrdersConfig : ScriptableObject
    {
        public int bombOrder = -1;
        public int halfBlockOrder = -2;
    }
}