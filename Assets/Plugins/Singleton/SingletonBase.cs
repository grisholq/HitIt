using UnityEngine;

public class SingletonBase<T> : MonoBehaviour where T : MonoBehaviour {

    private static T _instance;

    public static T Instance
    {
        get
        {
            if(_instance == null)
            {

                _instance = FindObjectOfType<T>();

                if (_instance == null)
                {

                    var singleton = new GameObject(typeof(T).ToString());
                    _instance = singleton.AddComponent<T>();

                }
            }
            return _instance;
        }
    }
}
