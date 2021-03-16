using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Compass : MonoBehaviour {

    public GameObject iconPrefab;
    List<CompassMarker> markers = new List<CompassMarker>();

    public GameObject player;

    float compassUnit;

    void Start() {
        compassUnit = GetComponent<RectTransform>().rect.width / 360f;
    }

    void Update() {
        foreach(CompassMarker marker in markers) {
            marker.image.rectTransform.anchoredPosition = GetPosOnCompass(marker);
        }
    }

    public void AddMarker(CompassMarker marker) {
        GameObject newMarker = Instantiate(iconPrefab, transform);

        marker.image = newMarker.GetComponent<Image>();
        marker.image.sprite = marker.icon;

        markers.Add(marker);
    }

    Vector2 GetPosOnCompass(CompassMarker marker) {
        Vector2 playerPos = new Vector2(player.transform.position.x, player.transform.position.z);
        Vector2 playerFwd = new Vector2(player.transform.forward.x, player.transform.forward.z);

        float angle = Vector2.SignedAngle(marker.position - playerPos, playerFwd);

        return new Vector2(compassUnit * angle, 0f);
    }
}
