using UnityEngine;

namespace Clock
{
    public class UndestroyableContainer : MonoBehaviour
    {
        private void Awake() => DontDestroyOnLoad(gameObject);
    }
}