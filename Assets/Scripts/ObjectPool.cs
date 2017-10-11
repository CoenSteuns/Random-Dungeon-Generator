using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour {

    public static ObjectPool Instance = null;

    [System.Serializable]
    public class PoolItem
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private int _startAmount;

        public GameObject Prefab { get { return _prefab; } }
        public int StartAmount { get { return _startAmount; } }
    }

    [SerializeField] private PoolItem[] _startItems;

    private Dictionary<string, List<GameObject>> _pool = new Dictionary<string, List<GameObject>>();
    private Dictionary<string, GameObject> _prefabs = new Dictionary<string, GameObject>();


    void Awake()
    {
        if (Instance != null)
            Destroy(this);

        Instance = this;

        for (int i = 0; i < _startItems.Length; i++)
        {

            CreateNewPool(_startItems[i].Prefab.name, _startItems[i].Prefab, _startItems[i].StartAmount);
        }

    }

    public GameObject GetObject(string name, bool instantiateNewWhenEmpty = true)
    {
        if (_pool[name] == null)
            return null;

        if (_pool[name].Count == 0 && instantiateNewWhenEmpty)
            return CreateObject(name);

        var item = _pool[name][0];
        _pool[name].RemoveAt(0);

        item.SetActive(true);
        item.transform.parent = null;
        return item;
    }

    public void PoolObject(PoolableObject poolableObject)
    {
        if (!_pool.ContainsKey(poolableObject.Name))
            return;

        poolableObject.gameObject.transform.parent = transform;
        _pool[poolableObject.Name].Add(poolableObject.gameObject);
        poolableObject.gameObject.SetActive(false);
    }

    public void CreateNewPool(string name, GameObject prefab, int StartAmount = 0)
    {

        if (!_prefabs.ContainsKey(name))
            _prefabs.Add(name, prefab);

        if (!_pool.ContainsKey(name))
            _pool.Add(name, new List<GameObject>());

        _prefabs[name] = prefab;
        _pool[name] = new List<GameObject>();

        for (int i = 0; i < StartAmount; i++)
        {
            PoolObject(CreateObject(name).GetComponent<PoolableObject>());
        }
    }

    private GameObject CreateObject(string name)
    {
        if (_prefabs[name] == null)
            return null;

        var item = Instantiate(_prefabs[name]);
        item.GetComponent<PoolableObject>().SetName(name);
        return item;
    }
}
