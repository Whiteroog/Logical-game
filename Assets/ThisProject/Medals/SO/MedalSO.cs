using UnityEngine;

namespace ThisProject.Medals.SO
{
    [CreateAssetMenu(fileName = "Medal-", menuName = "Level/Medal")]
    public class MedalSO : ScriptableObject
    {
        public string placeText = "No medal";
        public Color colorPlace = new Color(0.26f, 0.26f, 0.26f, 0.9f);
    }
}