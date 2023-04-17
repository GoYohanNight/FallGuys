using UnityEngine;
using UnityEngine.UI;

public class KeyGuideEffect : MonoBehaviour
{
    [Header("KeyGuides")]
    public Image _wKey;
    public Image _aKey;
    public Image _sKey;
    public Image _dKey;
    public Image _shiftKey;
    public Image _mouse01Key;
    public Image _spaceBar;

    private float minR = 0.3f;
    private float maxR = 0.8f;

    void Update()
    {
        // W 입력시 W 가이드 애니메이션
        if (Input.GetKeyDown(KeyCode.W))
            _wKey.color = new Color(Random.Range(minR, maxR), Random.Range(minR, maxR), Random.Range(minR, maxR));
        if (Input.GetKeyUp(KeyCode.W))
            _wKey.color = Color.white;

        // A 입력시 A 가이드 애니메이션
        if (Input.GetKeyDown(KeyCode.A))
            _aKey.color = new Color(Random.Range(minR, maxR), Random.Range(minR, maxR), Random.Range(minR, maxR));
        if (Input.GetKeyUp(KeyCode.A))
            _aKey.color = Color.white;

        // S 입력시 S 가이드 애니메이션
        if (Input.GetKeyDown(KeyCode.S))
            _sKey.color = new Color(Random.Range(minR, maxR), Random.Range(minR, maxR), Random.Range(minR, maxR));
        if (Input.GetKeyUp(KeyCode.S))
            _sKey.color = Color.white;

        // D 입력시 D 가이드 애니메이션
        if (Input.GetKeyDown(KeyCode.D))
            _dKey.color = new Color(Random.Range(minR, maxR), Random.Range(minR, maxR), Random.Range(minR, maxR));
        if (Input.GetKeyUp(KeyCode.D))
            _dKey.color = Color.white;

        // shift 입력시 shift 가이드 애니메이션
        if (Input.GetKeyDown(KeyCode.LeftShift))
            _shiftKey.color = new Color(Random.Range(minR, maxR), Random.Range(minR, maxR), Random.Range(minR, maxR));
        if (Input.GetKeyUp(KeyCode.LeftShift))
            _shiftKey.color = Color.white;

        // mouse1 입력시 mouse1 가이드 애니메이션
        if (Input.GetKeyDown(KeyCode.Mouse1))
            _mouse01Key.color = new Color(Random.Range(minR, maxR), Random.Range(minR, maxR), Random.Range(minR, maxR));
        if (Input.GetKeyUp(KeyCode.Mouse1))
            _mouse01Key.color = Color.white;

        // space 입력시 space 가이드 애니메이션
        if (Input.GetKeyDown(KeyCode.Space))
            _spaceBar.color = new Color(Random.Range(minR, maxR), Random.Range(minR, maxR), Random.Range(minR, maxR));
        if (Input.GetKeyUp(KeyCode.Space))
            _spaceBar.color = Color.white;
    }
}