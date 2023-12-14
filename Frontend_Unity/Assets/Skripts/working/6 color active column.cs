using UnityEngine;

public class colouractivecolumn : MonoBehaviour
{
    public float speed = 5f;
    public float minY = 1.5f;
    public float maxY = 13f;
    public float minX = -9f;
    public float maxX = 9f;

    // Koordinaten für die Snapping-Positionen auf der X-Achse
    private float[] xSnapPoints = { -7.5f, -4.5f, -1.5f, 1.5f, 4.5f, 7.5f };

    // Koordinaten für die Snapping-Positionen auf der Y-Achse
    private float[] ySnapPoints = { 1.5f, 3.5f, 5.5f, 7.5f, 9.5f, 11.5f };

    // Index für die aktuelle Position im xSnapPoints-Array
    private int xSnapIndex = 0;

    // Index für die aktuelle Position im ySnapPoints-Array
    private int ySnapIndex = 0;

    // Index für die ausgewählte Säule
    private int selectedColumnIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Setze den Start-Index auf den Index der ursprünglichen Position des Balls
        xSnapIndex = System.Array.IndexOf(xSnapPoints, transform.position.x);
        ySnapIndex = System.Array.IndexOf(ySnapPoints, transform.position.y);

        // Falls die ursprüngliche Position nicht in den definierten Snapping-Punkten liegt, setze den Index auf 0
        if (xSnapIndex == -1) xSnapIndex = 0;
        if (ySnapIndex == -1) ySnapIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // Überprüfe, ob Pfeiltaste nach links gedrückt wurde
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            // Ausgewählte Säule nach links verschieben (falls nicht bereits am Anfang)
            if (selectedColumnIndex > 0)
            {
                selectedColumnIndex--;
            }
        }

        // Überprüfe, ob Pfeiltaste nach rechts gedrückt wurde
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            // Ausgewählte Säule nach rechts verschieben (falls nicht bereits am Ende)
            if (selectedColumnIndex < xSnapPoints.Length - 1)
            {
                selectedColumnIndex++;
            }
        }

        // Färbe die Seitenwände der ausgewählten Säule
        ColorAdjacentColumnWalls(selectedColumnIndex);
    }

    // Funktion zum Abrufen des nächsten Werts aus dem xSnapPoints-Array
    float GetNextXSnapPoint()
    {
        return xSnapPoints[xSnapIndex];
    }

    // Funktion zum Abrufen des nächsten Werts aus dem ySnapPoints-Array
    float GetNextYSnapPoint()
    {
        return ySnapPoints[ySnapIndex];
    }

    // Überprüfe, ob dieser Ball an der obersten Position in seiner Spalte ist
    bool IsAtTopOfSelectedColumn()
    {
        for (int i = 0; i < ySnapPoints.Length; i++)
        {
            // Überprüfe, ob es einen Ball über diesem gibt
            if (i < ySnapIndex && Mathf.Approximately(transform.position.y, ySnapPoints[i]))
            {
                return false; // Nicht der oberste Ball in dieser Säule
            }
            // Überprüfe, ob es einen Ball unter diesem gibt
            else if (i > ySnapIndex && Mathf.Approximately(transform.position.y, ySnapPoints[i]))
            {
                return true; // Der oberste oder einzige Ball in dieser Säule
            }
        }
        return true; // Der oberste oder einzige Ball in dieser Säule
    }

    // Überprüfe, ob der Ball an der aktuellen X-Snap-Position ist
    bool IsAtCurrentXSnapPoint()
    {
        return Mathf.Approximately(transform.position.x, GetNextXSnapPoint());
    }

    // Überprüfe, ob der Ball an der aktuellen Y-Snap-Position ist
    bool IsAtCurrentYSnapPoint()
    {
        return Mathf.Approximately(transform.position.y, GetNextYSnapPoint());
    }

    // Funktion zum Springen zum vorherigen Snapping-Punkt auf der X-Achse
    void JumpToPreviousXSnapPoint()
    {
        // Zum vorherigen Index gehen (und Schleife wiederholen, wenn Anfang erreicht)
        xSnapIndex = (xSnapIndex - 1 + xSnapPoints.Length) % xSnapPoints.Length;
    }

    // Funktion zum Springen zum nächsten Snapping-Punkt auf der X-Achse
    void JumpToNextXSnapPoint()
    {
        // Zum nächsten Index gehen (und Schleife wiederholen, wenn Ende erreicht)
        xSnapIndex = (xSnapIndex + 1) % xSnapPoints.Length;
    }

    // Funktion zum Springen zum vorherigen Snapping-Punkt auf der Y-Achse
    void JumpToPreviousYSnapPoint()
    {
        // Zum vorherigen Index gehen (und Schleife wiederholen, wenn Anfang erreicht)
        ySnapIndex = (ySnapIndex - 1 + ySnapPoints.Length) % ySnapPoints.Length;
    }

    // Funktion zum Springen zum nächsten Snapping-Punkt auf der Y-Achse
    void JumpToNextYSnapPoint()
    {
        // Zum nächsten Index gehen (und Schleife wiederholen, wenn Ende erreicht)
        ySnapIndex = (ySnapIndex + 1) % ySnapPoints.Length;
    }

    // Funktion zum Färben der Seitenwände der ausgewählten Säule
    // Iteriere durch alle Wände und färbe die benachbarten Säulen
    void ColorAdjacentColumnWalls(int columnIndex)
    {
        for (int i = 1; i <= 7; i++)
        {
            GameObject sideWall = GameObject.Find("Sidewall " + i);
            if (sideWall != null)
            {
                if (i == columnIndex + 1 || i == columnIndex + 2)
                {
                    // Farbe für benachbarte Säulen
                    sideWall.GetComponent<Renderer>().material.color = Color.blue;
                }
                else
                {

                    // Farbe für nicht ausgewählte Säulen
                    sideWall.GetComponent<Renderer>().material.color = Color.white;
                }
            }
        }
    }
}

