using UnityEngine;

public class Floating : MonoBehaviour
{
    Vector3 _origin;

    public float MoveSpeed = 1f;

    public float AmplitudeMin = -0.05f;
    public float AmplitudeMax = 0.05f;

    public float _amplitude;

    void Start()
    {
        //Saves the position at the start of the game.
        _origin = this.transform.localPosition;

        //Makes all tiles WITHOUT SET VALUE float at different values.
        if (_amplitude == 0)
        {
            _amplitude = Random.Range(AmplitudeMin, AmplitudeMax);

            if (_amplitude == 0)
            {
                _amplitude = AmplitudeMin;
            }
        }
    }

    void FixedUpdate()
    {
        this.transform.localPosition = _origin + new Vector3(0, _amplitude * Mathf.Sin(Time.time * MoveSpeed), 0);
    }
}
